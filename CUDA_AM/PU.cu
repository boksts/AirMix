#pragma once

#include <math.h>
#include  <iostream>
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include "MiniWrapForCuda.h"
#include <ctime>

#define  b 100.0f
#define BLOCK_SIZE 32

#define a 0.1f
#define c 1.0f
#define g 9.8f

#define betta  0.003665f

using namespace std;

// ������ ��� ���������� �������
class Time{
	cudaEvent_t Tn, Tk;
	float time;
public:

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
	float tk(){
		cudaEventRecord(Tk, 0);
		cudaEventSynchronize(Tk);
		cudaEventElapsedTime(&time, Tn, Tk);
		return time;
	}
};

//���������� ��������
__global__ void kernel_P(int X, int Y, int x0, int l, double *P, double *Ux, double *Uy,double tau,double h){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if (i<0 || j<0 || i>(X - 1) || j>(Y - 1))
		return;

	if ((i>0) && (j>0) && (i<(X - 1)) && (j<(Y - 1))){
		P[j*X + i] = P[j*X + i] - tau*b*(((Ux[(j + 1)*X + i + 1] + Ux[(j - 1)*X + i + 1]) - (Ux[(j - 1)*X + i - 1] + Ux[(j + 1)*X + i - 1]) +
			(Uy[(j + 1)*X + i - 1] + Uy[(j + 1)*X + i + 1]) - (Uy[(j - 1)*X + i - 1] + Uy[(j - 1)*X + i + 1])) / (4.0*h));

		P[i] = P[X + i];
		P[(Y - 1)*X + i] = P[(Y - 2)*X + i];
	}

	P[j*X] = 2 * P[1 + j*X] - P[2 + j*X];
	P[j*X + (X - 1)] = 2 * P[j*X + (X - 2)] - P[j*X + (X - 3)];

	if ((i >= x0) && (i <= x0 + l))
		P[(Y - 1)*X + i] = 2 * P[(Y - 2)*X + i] - P[(Y - 3)*X + i];

}


//���������� ����� ���������
__global__ void kernel_U(int X, int Y, double *Uxn, double *Uyn, double *P, double *Ux, double *Uy, double *Temp, double tau,double h,double nuM,double ro){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){

		Uxn[j*X + i] = Ux[j*X + i] + tau*(
			-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Ux[j*X + i] - Ux[j*X + i - 1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Ux[j*X + i + 1] - Ux[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Ux[j*X + i] - Ux[(j - 1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Ux[(j + 1)*X + i] - Ux[j*X + i]) / h
			- (P[(j + 1)*X + i + 1] + P[(j - 1)*X + i + 1] - P[(j + 1)*X + i - 1] - P[(j - 1)*X + i - 1]) / (4 * h*ro)
			+ nuM*(Ux[j*X + i + 1] + Ux[j*X + i - 1] + Ux[(j - 1)*X + i] + Ux[(j + 1)*X + i] - 4 * Ux[j*X + i]) / (h*h)
			- g*betta*Temp[j*X + i]);

		Uyn[j*X + i] = Uy[j*X + i] + tau*(
			-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Uy[j*X + i] - Uy[j*X + i - 1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Uy[j*X + i + 1] - Uy[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Uy[j*X + i] - Uy[(j - 1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Uy[(j + 1)*X + i] - Uy[j*X + i]) / h
			- (P[(j + 1)*X + i - 1] + P[(j + 1)*X + i + 1] - P[(j - 1)*X + i - 1] - P[(j - 1)*X + i + 1]) / (4 * h*ro)
			+ nuM*(Uy[j*X + i + 1] + Uy[j*X + i - 1] + Uy[(j - 1)*X + i] + Uy[(j + 1)*X + i] - 4 * Uy[j*X + i]) / (h*h)
			- g*betta*Temp[j*X + i]);
	}

}

//����������������
__global__ void kernel_p(int X, int Y, double *Uxn, double *Uyn, double *Ux, double *Uy){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){
		Ux[j*X + i] = Uxn[j*X + i];
		Uy[j*X + i] = Uyn[j*X + i];
	}
}


//���������� �����������
__global__ void kernel_temp(int X, int Y, int x0, int len, double *Ux, double *Uy, double *Temp, double *Tempn,  double nuM, double h, double tau){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){
		Tempn[j*X + i] = Temp[j*X + i] + tau * (-(Ux[j*X + i] + abs(Ux[j*X + i])) / 2.0 * (Temp[j*X + i] - Temp[j*X + i - 1]) / h
			- (Ux[j*X + i] - abs(Ux[j*X + i])) / 2.0 * (Temp[j*X + i + 1] - Temp[j*X + i]) / h
			- (Uy[j*X + i] + abs(Uy[j*X + i])) / 2.0 * (Temp[j*X + i] - Temp[(j - 1)*X + i]) / h
			- (Uy[j*X + i] - abs(Uy[j*X + i])) / 2.0 * (Temp[(j + 1)*X + i] - Temp[j*X + i]) / h
			+ c*(nuM)* (Ux[j*X + i + 1] + Ux[j*X + i - 1] + Uy[(j + 1)*X + i] + Uy[(j - 1)*X + i] - 2 * Ux[j*X + i] - 2 * Uy[j*X + i]) /
			(h * h));
	}

	//����������� � �������
	if((i<(X - 1))  && (i>0)){
		//�� ������� �����
		if ((i < x0) || (i >= x0 + len))
			Tempn[(Y - 1)*X + i] = Temp[(Y - 1)*X + i] +
			tau*a*a / (h*h)*
			(Temp[(Y - 1)*X + i + 1] + Temp[(Y - 1)*X + i - 1] + Temp[(Y - 2)*X + i] - 4 * Temp[(Y - 1)*X + i]);

		//�� ������� ������
		Tempn[i] = Temp[i] + tau*a*a / (h*h)*(Temp[i + 1, 0] + Temp[i - 1] + Temp[X + i] - 4 * Temp[i]);

	}
}

//����������������
__global__ void kernel_pTemp(int X, int Y, int x0, int len, double *Temp, double *Tempn){
	int i = blockIdx.x*blockDim.x + threadIdx.x;
	int j = blockIdx.y*blockDim.y + threadIdx.y;

	if ((i<(X - 1)) && (j<(Y - 1)) && (i>0) && (j>0)){
		Temp[j*X + i] = Tempn[j*X + i];
	
		if ((i < x0) || (i >= x0 + len))
			Temp[(Y - 1)*X + i] = Tempn[(Y - 1)*X + i];

		Temp[i] = Tempn[i];	
		Temp[j*X + X - 1] = Temp[j*X + X - 2];		
	}
}

double *UxDev = NULL, *UyDev = NULL, *UxnDev = NULL, *UynDev = NULL,  *PDev = NULL, *TempDev = NULL, *TempnDev = NULL;
int X, Y;
int x0, len;
double tau, h;
double nuM, ro;
int sizef;
int gridSizeX, gridSizeY;
Time* timer;
FILE *f;
double ComputePU(ComputeOnCUDA::PU::PressureCalcMethod pressureMethod, ComputeOnCUDA::PU::NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double *Temp, double tmax) {
	double t = 0;
	double fulltime;
		//����������� ����� ������ � �������
	dim3 threads(BLOCK_SIZE, BLOCK_SIZE);
	dim3 blocks(gridSizeX, gridSizeY);

	//����������� �������� � ����� � ������ ����������
	cudaMemcpy(UxDev, Ux, sizef, cudaMemcpyHostToDevice);
	cudaMemcpy(UyDev, Uy, sizef, cudaMemcpyHostToDevice);
	cudaMemcpy(TempDev, Temp, sizef, cudaMemcpyHostToDevice);

	do{
		//������ ���� ����������
		kernel_P <<<blocks, threads >>>(X, Y, x0, len, PDev, UxDev, UyDev,tau,h);
		kernel_temp << <blocks, threads >> >(X, Y, x0, len, UxDev, UyDev, TempDev, TempnDev, nuM, h, tau);
		kernel_pTemp << <blocks, threads >> >(X, Y, x0, len, TempDev, TempnDev);
		kernel_U <<<blocks, threads >>>(X, Y, UxnDev, UynDev, PDev, UxDev, UyDev, TempDev,tau,h,nuM,ro);
		kernel_p <<<blocks, threads >>>(X, Y, UxnDev, UynDev, UxDev, UyDev);
		t += tau;

	} while (t <= tmax);

	//������������� ���������� � �����
	cudaThreadSynchronize();

	//����������� �������� � ���������� � ������ �����
	cudaMemcpy(Ux, UxDev, sizef, cudaMemcpyDeviceToHost);
	cudaMemcpy(Uy, UyDev, sizef, cudaMemcpyDeviceToHost);
	cudaMemcpy(Temp, TempDev, sizef, cudaMemcpyDeviceToHost);

	fulltime=timer->tk();

	return fulltime/1000.0;

	/*
	for (int j = 0; j < Y; j++){
		for (int i = 0; i < X; i++)
			fprintf(f, "%8.3f ", Ux[j*X + i]);
		fprintf(f, "\n");
	}
	fprintf(f, "\n X=%d ,Y=%d ,tmax=%f ,h=%f ,x0=%d ,len=%d ,tau=%f ", X, Y, tmax, h, x0, len, tau);*/
}

void ConstructorPU(double _tau, double _ro, double _nuM, int _x0, int _len, double _h, int _X, int _Y){
	tau = _tau;
	ro = _ro;
	nuM = _nuM;
	x0 = _x0;
	len = _len;
	h = _h;
	X = _X;
	Y = _Y;	
	//f = fopen("res.txt", "w");
	double *P = new double[X*Y];//��������

	timer = new Time();

	//��������� �������
	for (int i = 0; i < X; i++)
		for (int j = 0; j < Y; j++) {
			P[j * X + i] = 0.0;
		}

	sizef = X*Y*sizeof(double);

	//����������� ������� �����
	gridSizeX = (X / BLOCK_SIZE) + ((X % BLOCK_SIZE) > 0 ? 1 : 0);
	gridSizeY = (Y / BLOCK_SIZE) + ((Y % BLOCK_SIZE) > 0 ? 1 : 0);

	//��������� ������ �� ����������
	cudaMalloc((void**)&UxDev, sizef);
	cudaMalloc((void**)&UxnDev, sizef);
	cudaMalloc((void**)&UyDev, sizef);
	cudaMalloc((void**)&UynDev, sizef);
	cudaMalloc((void**)&PDev, sizef);
	cudaMalloc((void**)&TempDev, sizef);
	cudaMalloc((void**)&TempnDev, sizef);

	//����� ������ ������� ����������
	timer->tn();
	cudaMemcpy(PDev, P, sizef, cudaMemcpyHostToDevice);
	
	//fprintf(f, "������ ��������\n");
    //����� ������ ������� ����������
	//timer = time->tk();
 	//����� ����������� � ����
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
	//delete time;

}

void DestructorPU() {
	//fprintf(f, "������ �����������, ���� ������\n");
	//fclose(f);
	cudaFree(UxDev);
	cudaFree(UxnDev);
	cudaFree(UyDev);
	cudaFree(UynDev);
	cudaFree(PDev);
	cudaFree(TempDev);
	cudaFree(TempnDev);
}