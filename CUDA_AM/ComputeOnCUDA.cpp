#pragma once

#include "ComputeOnCUDA.h"
#include "MiniWrapForCuda.h"

ComputeOnCUDA::PU::PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y, double tmax) {
	this->tau = tau;
	this->ro = ro;
	this->nuM = nuM;
	this->x0 = x0;
	this->len = len;
	this->h = h;
	this->X = X;
	this->Y = Y;
	this->tmax = tmax;

}

void ComputeOnCUDA::PU::Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double* Ux, double* Uy) {
	 PU_CUDA(pressureMethod, navierStokesMethod,Ux, Uy, tau, ro, nuM, x0, len, h, X, Y, tmax);
}

