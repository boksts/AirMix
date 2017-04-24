#pragma once

#include "ComputeOnCUDA.h"
#include "MiniWrapForCuda.h"

ComputeOnCUDA::PU::PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y) {

	Constructor(tau, ro, nuM, x0, len, h, X, Y);
}


ComputeOnCUDA::PU::~PU() {
	Destructor();
}
void ComputeOnCUDA::PU::Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double* Ux, double* Uy, double tmax) {
	 Compute(pressureMethod, navierStokesMethod,Ux, Uy, tmax);
}



