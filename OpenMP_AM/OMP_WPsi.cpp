
#include <math.h>

#include "ComputeOnOMP.h"


ComputeOnOMP::WPsi::WPsi(double tau, double nuM, int x0, int len, double h, int X, int Y, double* Ux, double* Uy) {
	this->tau = tau;
	this->nuM = nuM;
	this->x0 = x0;
	this->len = len;
	this->h = h;
	this->X = X;
	this->Y = Y;
	this->Ux = Ux;
	this->Uy = Uy;

	Uxn = new double[X*Y];
	Uyn = new double[X*Y];
	w = new double[X*Y];
	wn = new double[X*Y];
	psi = new double[X*Y];
	psin = new double[X*Y];
	//fopen_s(&f,"res2.txt", "w");

	Init();
}

void ComputeOnOMP::WPsi::Init() {
	//функци€ тока на границах
	
		//начальные услови€
		for (int i = 0; i<X; i++)
			for (int j = 0; j<Y; j++){
				psi[i*Y + j] = 0.0;
				w[i*Y + j] = 0.0;
			}
	
		for (int i = X - 2; i >= 0; i--){
			if (i > x0 + len)
				psi[i + (Y - 1)*X] = 0.0;
			if ((i >= x0) && (i <= x0 + len))
				psi[i + (Y - 1)*X] = psi[i + (Y - 1)*X + 1] + Uy[i + (Y - 1)*X] * h;
			if (i < x0)
				psi[i + (Y - 1)*X] = psi[i + (Y - 1)*X + 1];
		}

	
		for (int j = Y - 2; j >= 0; j--)
			psi[j*X] = psi[(j + 1)*X] + Ux[j*X] * h;

	
		for (int i = 1; i < X; i++)
			psi[i] = psi[i - 1];

	
		for (int j = Y - 2; j >= 0; j--)
			psi[j*X + (X - 1)] = psi[(j + 1)*X + (X - 1)] + Ux[j*X + (X - 1)] * h;
	
}

void ComputeOnOMP::WPsi::Vortex() {

	#pragma omp parallel
	{
		#pragma omp for schedule(static)
		for (int i = 0; i < X; i++){
			w[i] = -(psi[i + X] - psi[i]) / (h*h);
			w[i + (Y - 1)*X] = -(psi[i + (Y - 2)*X] - psi[i + (Y - 1)*X]) / (h*h);
		}

		#pragma omp for schedule(static)
		for (int j = 0; j < Y; j++){
			w[j*X] = 0;
			w[j*X + (X - 1)] = w[j*X + (X - 2)];
		}

		//уравнение гельмгольца(противоточные производные)
		#pragma omp for schedule(static)
		for (int j = 1; j < Y - 1; j++)
			for (int i = 1; i < X - 1; i++){
			wn[j*X + i] = w[j*X + i] + tau*(
				-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0*(w[j*X + i] - w[j*X + i - 1]) / h
				- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0*(w[j*X + i + 1] - w[j*X + i]) / h
				- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0*(w[j*X + i] - w[(j - 1)*X + i]) / h
				- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0*(w[(j + 1)*X + i] - w[j*X + i]) / h
				+ (nuM)*(w[j*X + i + 1] + w[j*X + i - 1] + w[(j + 1)*X + i] + w[(j - 1)*X + i] - 4 * w[j*X + i]) / (h * h) - g*betta*Temp[i, j]);
			}

		#pragma omp for schedule(static)
		for (int j = 1; j < Y - 1; j++)
			for (int i = 1; i < X - 1; i++)
				w[j*X + i] = wn[j*X + i];
	}

}

void ComputeOnOMP::WPsi::CurrentFunction() {
	int pr;
	do {
		pr = 1;
		#pragma omp parallel for schedule(static) reduction(*:pr)
		for (int j = 1; j<Y - 1; j++)
			for (int i = 1; i<X - 1; i++){
			psin[j*X + i] = 0.25*(psi[j*X + i + 1] + psi[j*X + i - 1] + psi[(j + 1)*X + i] + psi[(j - 1)*X + i] + h*h*w[j*X + i]);

			if (abs(psin[j*X + i] - psi[j*X + i]) >= epsPsi)
				pr = 0;
			}

		#pragma omp parallel for schedule(static) 
		for (int j = 1; j<Y - 1; j++)
			for (int i = 1; i<X - 1; i++)
				psi[j*X + i] = psin[j*X + i];

	} while (pr==0);
}

void ComputeOnOMP::WPsi::Speeds() {
	//вычисление составл€ющих скорости
	#pragma omp parallel for schedule(static)
	for (int j = 1; j<Y - 1; j++)
		for (int i = 1; i<X - 1; i++){
		Ux[j*X + i] = -(psi[(j + 1)*X + i + 1] + psi[(j + 1)*X + i - 1] - psi[(j - 1)*X + i + 1] - psi[(j - 1)*X + i - 1]) / (4 * h);
		Uy[j*X + i] = -(psi[(j + 1)*X + i + 1] - psi[(j + 1)*X + i - 1] + psi[(j - 1)*X + i + 1] - psi[(j - 1)*X + i - 1]) / (4 * h);
		}
}

double ComputeOnOMP::WPsi::Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, double *Ux, double *Uy, double *_Temp, double tmax) {
	this->Ux = Ux;
	this->Uy = Uy;
	this->Temp = _Temp;
	Time timeObj;
	double fulltime;
	Temperature* temp = new Temperature(tau, nuM, x0, len, h, X, Y);
	double t = 0;

	timeObj.tn();

	do {
		Temp = temp->CalcTemp(this->Ux, this->Uy, Temp);
		Vortex();
		CurrentFunction();
		Speeds();

		t += tau;
	}
	while (t <= tmax);

	fulltime = timeObj.tk();
	/*
	for (int j = 0; j < Y; j++){
		for (int i = 0; i < X; i++)


			fprintf(f, "%8.3f ", Ux[j*X+i]);
		fprintf(f, "\n");
	}

   fprintf(f, "\n X=%d ,Y=%d ,tmax=%f ,h=%f ,x0=%d ,len=%d ,tau=%f, tmax =%f ", X, Y, tmax, h, x0, len, tau,tmax);*/

	return fulltime;

}


ComputeOnOMP::WPsi::~WPsi() {
	delete[] Uxn;
	delete[] Uyn;
	delete[] w;
	delete[] wn;
	delete[] psi;
	delete[] psin;
	//fclose(f);

}