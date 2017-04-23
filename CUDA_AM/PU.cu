#pragma once

#include <math.h>
//#include <time.h>
#include  <iostream>
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include "MiniWrapForCuda.h"



#define  b 100.0f;

#define BLOCK_SIZE 32

using namespace std;
/*
//функции для вычисление времени
class Time{
	cudaEvent_t Tn, Tk;
	time_t start, end;
public:
	double time;

	Time(){
		cudaEventCreate(&Tn);
		cudaEventCreate(&Tk);
	}
	~Time(){
		cudaEventDestroy(Tn);
		cudaEventDestroy(Tk);
	}
	void tn(){
		cudaEventRecord(Tn, 0);
	}
	double tk(){
		cudaEventRecord(Tk, 0);
		cudaEventSynchronize(Tk);
		cudaEventElapsedTime(&time, Tn, Tk);
		return time;
	}
	void tstart(){
		start = clock();
	}
	double tend(){
		end = clock();
		time = end - start;
		return time;
	}
};

*/

//вычисление давления
__global__ void kernel_P(int X, int Y, int x0, int l, double *P, double *Ux, double *Uy,double tau,double h){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if (i<0 || j<0 || i>(X - 1) || j>(Y - 1))
		return;

	if ((i>0) && (j>0) && (i<(X - 1)) && (j<(Y - 1))){
		P[j*X + i] = P[j*X + i] - tau*100.0*(((Ux[(j + 1)*X + i + 1] + Ux[(j - 1)*X + i + 1]) - (Ux[(j - 1)*X + i - 1] + Ux[(j + 1)*X + i - 1]) +
			(Uy[(j + 1)*X + i - 1] + Uy[(j + 1)*X + i + 1]) - (Uy[(j - 1)*X + i - 1] + Uy[(j - 1)*X + i + 1])) / (4.0*h));


		P[i] = P[X + i];
		P[(Y - 1)*X + i] = P[(Y - 2)*X + i];
		P[j*X] = P[1 + j*X];
		P[j*X + (X - 1)] = P[j*X + (X - 2)];
	}

	P[j*X] = 2 * P[1 + j*X] - P[2 + j*X];
	P[j*X + (X - 1)] = 2 * P[j*X + (X - 2)] - P[j*X + (X - 3)];

	if ((i >= x0) && (i <= x0 + l))
		P[(Y - 1)*X + i] = 2 * P[(Y - 2)*X + i] - P[(Y - 3)*X + i];

}


//вычисление новых скоростей
__global__ void kernel_U(int X, int Y, double *Uxn, double *Uyn, double *P, double *Ux, double *Uy,double tau,double h,double nuM,double ro){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){

		Uxn[j*X + i] = Ux[j*X + i] + tau*(
			-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Ux[j*X + i] - Ux[j*X + i - 1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Ux[j*X + i + 1] - Ux[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Ux[j*X + i] - Ux[(j - 1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Ux[(j + 1)*X + i] - Ux[j*X + i]) / h
			- (P[(j + 1)*X + i + 1] + P[(j - 1)*X + i + 1] - P[(j + 1)*X + i - 1] - P[(j - 1)*X + i - 1]) / (4 * h*ro)
			+ nuM*(Ux[j*X + i + 1] + Ux[j*X + i - 1] + Ux[(j - 1)*X + i] + Ux[(j + 1)*X + i] - 4 * Ux[j*X + i]) / (h*h));

		Uyn[j*X + i] = Uy[j*X + i] + tau*(
			-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Uy[j*X + i] - Uy[j*X + i - 1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Uy[j*X + i + 1] - Uy[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Uy[j*X + i] - Uy[(j - 1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Uy[(j + 1)*X + i] - Uy[j*X + i]) / h
			- (P[(j + 1)*X + i - 1] + P[(j + 1)*X + i + 1] - P[(j - 1)*X + i - 1] - P[(j - 1)*X + i + 1]) / (4 * h*ro)
			+ nuM*(Uy[j*X + i + 1] + Uy[j*X + i - 1] + Uy[(j - 1)*X + i] + Uy[(j + 1)*X + i] - 4 * Uy[j*X + i]) / (h*h));
	}

}

//переприсваивание
__global__ void kernel_p(int X, int Y, double *Uxn, double *Uyn, double *Ux, double *Uy){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){
		Ux[j*X + i] = Uxn[j*X + i];
		Uy[j*X + i] = Uyn[j*X + i];
	}
}





void PU_CUDA(ComputeOnCUDA::PU::PressureCalcMethod pressureMethod, ComputeOnCUDA::PU::NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double tau, double ro, double nuM, int x0, int len, double h, int X, int Y, double tmax){
	
	//FILE *f = fopen("res.txt", "w");
	double *Uxn = new double[X*Y]; //скорости новые
	double *Uyn = new double[X*Y]; //скорости новые
	double *P = new double[X*Y];//давление
	double *divU = new double[X*Y];//дивергенция скорости

	//Time* time = new Time();
	double timer, t = 0;

	//начальные условия
	for (int i = 0; i < X; i++)
		for (int j = 0; j < Y; j++) {
			P[j * X + i] = 0.0;
		}

	double *UxDev = NULL, *UyDev = NULL, *UxnDev = NULL, *UynDev = NULL, *PDev = NULL;
	int sizef = X*Y*sizeof(double);

	//определение размера грида
	int gridSizeX = (X / BLOCK_SIZE) + ((X % BLOCK_SIZE) > 0 ? 1 : 0);
	int gridSizeY = (Y / BLOCK_SIZE) + ((Y % BLOCK_SIZE) > 0 ? 1 : 0);

	//определение числа блоков и потоков
	dim3 threads(BLOCK_SIZE, BLOCK_SIZE);
	dim3 blocks(gridSizeX, gridSizeY);

	//выделение памяти на устройстве
	cudaMalloc((void**)&UxDev, sizef);
	cudaMalloc((void**)&UxnDev, sizef);
	cudaMalloc((void**)&UyDev, sizef);
	cudaMalloc((void**)&UynDev, sizef);
	cudaMalloc((void**)&PDev, sizef);

	//старт замера времени вычислений
	//time->tn();

	//копирование значений с хоста в память устройства
	cudaMemcpy(UxDev, Ux, sizef, cudaMemcpyHostToDevice);
	cudaMemcpy(UyDev, Uy, sizef, cudaMemcpyHostToDevice);
	cudaMemcpy(PDev, P, sizef, cudaMemcpyHostToDevice);



	do{
		//запуск ядер устройства
		kernel_P <<<blocks, threads >>>(X, Y, x0, len, PDev, UxDev, UyDev,tau,h);
		kernel_U <<<blocks, threads >>>(X, Y, UxnDev, UynDev, PDev, UxDev, UyDev,tau,h,nuM,ro);
		kernel_p <<<blocks, threads >>>(X, Y, UxnDev, UynDev, UxDev, UyDev);
		t += tau;
	} while (t <= tmax);



	//синхронизация устройства и хоста
	cudaThreadSynchronize();

	//копирование значений с устройства в память хоста
	cudaMemcpy(Ux, UxDev, sizef, cudaMemcpyDeviceToHost);
	cudaMemcpy(Uy, UyDev, sizef, cudaMemcpyDeviceToHost);

	//конец замера времени вычислений
	//timer = time->tk();


	//Вывод результатов в файл
	//fprintf(f,"time=%f",timer/1000);


	/*for (i = 0; i<X; i++){
		for (j = 0; j<Y; j++)
			fprintf(f, "%7.3f ", Uy[j*X + i]);
		fprintf(f, "\n");
	}
	fprintf(f, "\n\n");*/


	/*for (int j = 0; j < Y; j++){
		for (int i = 0; i < X; i++)


			fprintf(f, "%8.3f ", Ux[j*X+i]);
		fprintf(f, "\n");
	}

	fprintf(f, "\n X=%d ,Y=%d ,tmax=%f ,h=%f ,x0=%d ,len=%d ,tau=%f ", X, Y, tmax, h, x0, len, tau);*/

	cudaFree(UxDev);
	cudaFree(UxnDev);
	cudaFree(UyDev);
	cudaFree(UynDev);
	cudaFree(PDev);

	delete[]  Uxn, Uyn, P, divU;
	//delete time;

}

