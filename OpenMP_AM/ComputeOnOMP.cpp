
#include <math.h>

#include "ComputeOnOMP.h"


ComputeOnOMP::PU::PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y) {
	this->tau = tau;
	this->ro = ro;
	this->nuM = nuM;
	this->x0 = x0;
	this->len = len;
	this->h = h;
	this->X = X;
	this->Y = Y;

	Uxn = new double[X*Y];
	Uyn = new double[X*Y];
	divU = new double[X*Y];
	P = new double[X*Y];	
	//fopen_s(&f,"res2.txt", "w");

	//начальные условия
	for (int i = 0; i<X; i++)
		for (int j = 0; j<Y; j++){
			P[j*X + i] = 0.0;
		}
}



void ComputeOnOMP::PU::_WeakСompressibility() {
	//вычисление дивергенции скорости
	#pragma omp for schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			divU[j*X + i] = ((Ux[(j + 1)*X + i + 1] + Ux[(j - 1)*X + i + 1]) - (Ux[(j - 1)*X + i - 1] + Ux[(j + 1)*X + i - 1]) +
			(Uy[(j + 1)*X + i - 1] + Uy[(j + 1)*X + i + 1]) - (Uy[(j - 1)*X + i - 1] + Uy[(j - 1)*X + i + 1])) / (4.0*h);

	//вычисление давления внутри области
	#pragma omp for schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			P[j*X + i] = P[j*X + i] - tau*b*divU[j*X + i];

	//давление на границах
	#pragma omp for schedule(static)
	for (int i = 1; i<X - 1; i++){
		P[i] = P[X + i];
		P[(Y - 1)*X + i] = P[(Y - 2)*X + i];
	}

	#pragma omp for schedule(static)
	for (int j = 1; j<Y - 1; j++){
		P[j*X] = P[1 + j*X];
		P[j*X + (X - 1)] = P[j*X + (X - 2)];
	}

	#pragma omp for schedule(static)
	for (int j = 0; j<Y; j++){
		P[j*X] = 2 * P[1 + j*X] - P[2 + j*X];//скорость 1 потока
		P[j*X + (X - 1)] = 2 * P[j*X + (X - 2)] - P[j*X + (X - 3)];//скорость на выходе
	}

	#pragma omp for schedule(static)
	for (int i = 0; i<X; i++){
		if ((i >= x0) && (i <= x0 + len))
			P[(Y - 1)*X + i] = 2 * P[(Y - 2)*X + i] - P[(Y - 3)*X + i];//скорость 2 потока
	}
}

void ComputeOnOMP::PU::_Poisson() {
	
}


void ComputeOnOMP::PU::Speeds() {
	//скорости новые
	#pragma omp for schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			Uxn[j*X + i] = Ux[j*X + i] + tau*(-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Ux[j*X + i] - Ux[j*X + i - 1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Ux[j*X + i + 1] - Ux[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Ux[j*X + i] - Ux[(j - 1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Ux[(j + 1)*X + i] - Ux[j*X + i]) / h
			- (P[(j + 1)*X + i + 1] + P[(j - 1)*X + i + 1] - P[(j + 1)*X + i - 1] - P[(j - 1)*X + i - 1]) / (4.0 * h*ro)
			+ nuM*(Ux[j*X + i + 1] + Ux[j*X + i - 1] + Ux[(j - 1)*X + i] + Ux[(j + 1)*X + i] - 4 * Ux[j*X + i]) / (h*h));

	
	#pragma omp for schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			Uyn[j*X + i] = Uy[j*X + i] + tau*(-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Uy[j*X + i] - Uy[j*X + i - 1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Uy[j*X + i + 1] - Uy[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Uy[j*X + i] - Uy[(j - 1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Uy[(j + 1)*X + i] - Uy[j*X + i]) / h
			- (P[(j + 1)*X + i - 1] + P[(j + 1)*X + i + 1] - P[(j - 1)*X + i - 1] - P[(j - 1)*X + i + 1]) / (4.0 * h*ro)
			+ nuM*(Uy[j*X + i + 1] + Uy[j*X + i - 1] + Uy[(j - 1)*X + i] + Uy[(j + 1)*X + i] - 4 * Uy[j*X + i]) / (h*h));


	#pragma omp for schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			Ux[j*X + i] = Uxn[j*X + i];

	#pragma omp for schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			Uy[j*X + i] = Uyn[j*X + i];

}


void ComputeOnOMP::PU::Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double tmax) {
	
	this->Ux = Ux;
	this->Uy = Uy;

	double t = 0;
	#pragma omp parallel
	{
		do{
		
			switch (pressureMethod) {
			case Poisson:
				_Poisson();
				break;
			case WeakСompressibility:
				_WeakСompressibility();
				break;
			}

			Speeds();
			
			t += tau;
		} while (t <= tmax);
	}
	
	//for (int j = 0; j < Y; j++){
	//	for (int i = 0; i < X; i++)


	//		fprintf(f, "%8.3f ", Ux[j*X+i]);
	//	fprintf(f, "\n");
	//}

	//fprintf(f, "\n X=%d ,Y=%d ,tmax=%f ,h=%f ,x0=%d ,len=%d ,tau=%f ", X, Y, tmax, h, x0, len, tau);


}


ComputeOnOMP::PU::~PU() {
	delete[] Uxn;
	delete[] Uyn;
	delete[] divU;
	delete[] P;
	//fclose(f);

}
