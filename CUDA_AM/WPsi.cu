#pragma once

#include <math.h>
#include  <iostream>
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include "MiniWrapForCuda.h"
#include <ctime>

#define  epsPsi 0.001f

#define _BLOCK_SIZE 32

using namespace std;


// методы дл€ вычислени€ времени
class _Time{
	cudaEvent_t Tn, Tk;
	float time;
public:


	_Time(){
		cudaEventCreate(&Tn);
		cudaEventCreate(&Tk);
	}
	~_Time(){
		cudaEventDestroy(Tn);
		cudaEventDestroy(Tk);
	}
	void tn(){
		cudaEventRecord(Tn, 0);
	}
	float tk(){
		cudaEventRecord(Tk, 0);
		cudaEventSynchronize(Tk);
		cudaEventElapsedTime(&time, Tn, Tk);
		return time;
	}
};

//уравнение √ельмгольца (противоточные производные)
__global__ void kernel_gelmgolca(int X, int Y, double *w, double *wn, double *psi, double *ux, double *uy, double h, double tau, double nuM){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<X) && (j<Y)){
		w[i] = -(psi[i + X] - psi[i]) / (h*h);
		w[i + (Y - 1)*X] = -(psi[i + (Y - 2)*X] - psi[i + (Y - 1)*X]) / (h*h);
		w[j*X] = 0;
		w[j*X + (X - 1)] = w[j*X + (X - 2)];
	}

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){
		float dux, duy;

		if (ux[j*X + i] < 0)
			dux = (w[j*X + i + 1] - w[j*X + i]) / h;
		else
			dux = (w[j*X + i] - w[j*X + i - 1]) / h;
		if (uy[j*X + i] < 0)
			duy = (w[(j + 1)*X + i] - w[j*X + i]) / h;
		else
			duy = (w[j*X + i] - w[(j - 1)*X + i]) / h;


		wn[j*X + i] = w[j*X + i] + tau*(-ux[j*X + i] * dux - uy[j*X + i] * duy + nuM*
			(w[j*X + i + 1] + w[j*X + i - 1] + w[(j + 1)*X + i] + w[(j - 1)*X + i] - 4 * w[j*X + i]) / (h*h));

	}
}


//уравнение ѕуассона (метод якоби)
__global__ void kernel_puasson(int X, int Y, double *psi, double *w, double *psin, int *pr, double h){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){
		pr[j*X + i] = 0;
		psin[j*X + i] = 0.25*(psi[j*X + i + 1] + psi[j*X + i - 1] +
			psi[(j + 1)*X + i] + psi[(j - 1)*X + i] + h*h*w[j*X + i]);

		if (fabs(psin[j*X + i] - psi[j*X + i]) >= epsPsi)
			pr[j*X + i] = 1;
	}
}


//вычисление скоростей
__global__ void kernel_skorosti(int X, int Y, double *psi, double *ux, double *uy, double h){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i>0) && (j>0) && (i<(X - 1)) && (j<(Y - 1))){
		ux[j*X + i] = -(psi[(j + 1)*X + i + 1] + psi[(j + 1)*X + i - 1]
			- psi[(j - 1)*X + i + 1] - psi[(j - 1)*X + i - 1]) / (4 * h);

		uy[j*X + i] = -(psi[(j + 1)*X + i + 1] - psi[(j + 1)*X + i - 1]
			+ psi[(j - 1)*X + i + 1] - psi[(j - 1)*X + i - 1]) / (4 * h);
	}

}


//переприсваивание
__global__ void kernel_p(int X, int Y, double *psi, double *psin){
	int i = blockIdx.x*blockDim.x + threadIdx.x + 1;
	int j = blockIdx.y*blockDim.y + threadIdx.y + 1;
	if ((i<(X - 1)) && (j<(Y - 1)))
		psi[j*X + i] = psin[j*X + i];
}


double *_UxDev = NULL, *_UyDev = NULL, *_UxnDev = NULL, *_UynDev = NULL, *wDev = NULL, *wnDev = NULL, *psiDev = NULL, *psinDev = NULL;
int *prDev = NULL;
int _X, _Y;
int _x0, _len;
double _tau, _h;
double _nuM, _ro;
int _sizef, sizei;
double _fulltime;
int _gridSizeX, _gridSizeY;
_Time* _timer;


double ComputeWPsi(ComputeOnCUDA::WPsi::HelmholtzCalcMethod hcm, ComputeOnCUDA::TurbulenceModel tm,  double *Ux, double *Uy, double tmax) {
	double t = 0;
	
	//определение числа блоков и потоков
	dim3 threads(_BLOCK_SIZE, _BLOCK_SIZE);
	dim3 blocks(_gridSizeX, _gridSizeY);

	//копирование значений с хоста в пам€ть устройства
	cudaMemcpy(_UxDev, Ux, _sizef, cudaMemcpyHostToDevice);
	cudaMemcpy(_UyDev, Uy, _sizef, cudaMemcpyHostToDevice);

	bool flag = false;
	int *pr = NULL;
	pr = new int[_X*_Y];
	
	do{
		//запуск €дер устройства
		kernel_gelmgolca << <blocks, threads >> >(_X, _Y, wDev, wnDev, psiDev,
			_UxDev, _UyDev, _h, _tau, _nuM);
		kernel_p << <blocks, threads >> >(_X, _Y, wDev, wnDev);
		
		//решение уравнени€ ѕуассона до достижени€ точности
		do {
			flag = false;
			//запуск €дер устройства
			kernel_puasson << <blocks, threads >> >(_X, _Y, psiDev, wDev, psinDev, prDev, _h);
			kernel_p << <blocks, threads >> >(_X, _Y, psiDev, psinDev);
			//синхронизаци€ устройства и хоста
			cudaDeviceSynchronize();
			//копирование значений с устройства в пам€ть хоста
			cudaMemcpy(pr, prDev, sizei, cudaMemcpyDeviceToHost);
			for (int j = 1; j<_Y - 1; j++)
				for (int i = 1; i<_X - 1; i++)
					if (pr[j*_X + i] == 1){
						flag = true;
						j = _Y; i = _X;
					}

		} while (flag);

		//запуск €дер устройства
		kernel_skorosti << <blocks, threads >> >(_X, _Y, psiDev, _UxDev, _UyDev, _h);
		t += _tau;

	} while (t <= tmax);

	//синхронизаци€ устройства и хоста
	cudaThreadSynchronize();

	//копирование значений с устройства в пам€ть хоста
	cudaMemcpy(Ux, _UxDev, _sizef, cudaMemcpyDeviceToHost);
	cudaMemcpy(Uy, _UyDev, _sizef, cudaMemcpyDeviceToHost);

	 _fulltime = _timer->tk();
	 return _fulltime / 1000.0;
	
	
}

void ConstructorWPsi(double tau,  double nuM, int x0, int len, double h, int X, int Y, double *Ux, double *Uy){
	_tau = tau;
	_nuM = nuM;
	_x0 = x0;
	_len = len;
	_h = h;
	_X = X;
	_Y = Y;

	double *psi = new double[X*Y];//функци€ тока
	double *w = new double[X*Y];//функци€ тока
	_sizef = X*Y*sizeof(double);
	sizei = X*Y*sizeof(int);
	_timer = new _Time();

	//начальные услови€
	for (int i = 0; i < X; i++)
		for (int j = 0; j < Y; j++) {
			psi[j * X + i] = 0.0;
			w[j * X + i] = 0.0;
		}
	//функци€ тока на границах
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

	for (int i = 1; i<X; i++)
		psi[i] = psi[i - 1];


	for (int j = Y - 2; j >= 0; j--)
		psi[j*X + (X - 1)] = psi[(j + 1)*X + (X - 1)] + Ux[j*X + (X - 1)] * h;
	

	//определение размера грида
	_gridSizeX = (X / _BLOCK_SIZE) + ((X % _BLOCK_SIZE) > 0 ? 1 : 0);
	_gridSizeY = (Y / _BLOCK_SIZE) + ((Y % _BLOCK_SIZE) > 0 ? 1 : 0);

	//выделение пам€ти на устройстве
	cudaMalloc((void**)&_UxDev, _sizef);
	cudaMalloc((void**)&_UxnDev, _sizef);
	cudaMalloc((void**)&_UyDev, _sizef);
	cudaMalloc((void**)&_UynDev, _sizef);
	cudaMalloc((void**)&psiDev, _sizef);
	cudaMalloc((void**)&psinDev, _sizef);
	cudaMalloc((void**)&wDev, _sizef);
	cudaMalloc((void**)&wnDev, _sizef);
	cudaMalloc((void**)&prDev, sizei);

	//старт замера времени вычислений
	_timer->tn();

	cudaMemcpy(wDev, w, _sizef, cudaMemcpyHostToDevice);
	cudaMemcpy(psiDev, psi, _sizef, cudaMemcpyHostToDevice);
	
}

void DestructorWPsi() {
	cudaFree(_UxDev);
	cudaFree(_UxnDev);
	cudaFree(_UyDev);
	cudaFree(_UynDev);
	cudaFree(wDev);
	cudaFree(wnDev);
	cudaFree(psiDev);
	cudaFree(psinDev);
	cudaFree(prDev);

}