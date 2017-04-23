#pragma once
#include "ComputeOnCUDA.h"

void PU_CUDA(ComputeOnCUDA::PU::PressureCalcMethod pressureMethod, ComputeOnCUDA::PU::NavierStokesCalcMethod navierStokesMethod,double *Ux, double *Uy, double tau, double ro, double nuM, int x0, int len, double h, int X, int Y, double tmax);
