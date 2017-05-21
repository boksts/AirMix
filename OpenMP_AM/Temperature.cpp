#include <math.h>

#include "ComputeOnOMP.h"

double* ComputeOnOMP::Temperature::CalcTemp(double* Ux, double* Uy, double*Temp) {

	#pragma omp for  schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			Tempn[j*X + i] = Temp[j*X + i] + tau * (-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Temp[j*X + i] - Temp[j*X + i-1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Temp[j*X + i+1] - Temp[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Temp[j*X + i] - Temp[(j-1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Temp[(j+1)*X + i] - Temp[j*X + i]) / h
			+ c*(nuM)* (Ux[j*X + i + 1] + Ux[j*X + i - 1] + Uy[(j + 1)*X + i] + Uy[(j-1)*X + i] - 2 * Ux[j*X + i] - 2 * Uy[j*X + i]) /
			(h * h));


	//температура в стенках
	for (int i = 1; i < X - 1; i++) {
		//на границе снизу
		if ((i < x0) || (i >= x0 + len))
			Tempn[(Y - 1)*X + i] = Temp[(Y - 1)*X + i] +
			tau*a*a / (h*h)*
			(Temp[(Y - 1)*X + i + 1] + Temp[(Y - 1)*X + i - 1] + Temp[(Y - 2)*X + i] - 4 * Temp[(Y - 1)*X + i]);

		//на границе сверху
		Tempn[i] = Temp[i] +tau*a*a / (h*h)*(Temp[i + 1] + Temp[i - 1] + Temp[X + i] - 4 * Temp[i]);

	}

	#pragma omp for  schedule(static)
	for (int i = 1; i < X - 1; i++)
		for (int j = 1; j < Y - 1; j++)
			Temp[j*X + i] = Tempn[j*X + i];


	for (int i = 1; i < X - 1; i++) {
		if ((i < x0) || (i >= x0 + len))
			Temp[(Y - 1)*X + i] = Tempn[(Y - 1)*X + i];

		Temp[i] = Tempn[i];
	}

	for (int j = 1; j < Y - 1; j++) {
		Temp[j*X + X - 1] = Temp[j*X + X - 2];
	}

	return Temp;

}


ComputeOnOMP::Temperature::Temperature(double tau, double nuM, int x0, int len, double h, int X, int Y) {
	this->tau = tau;
	this->nuM = nuM;
	this->h = h;
	this->X = X;
	this->Y = Y;
	this->x0 = x0;
	this->len = len;

	Tempn = new double[X*Y];
}
