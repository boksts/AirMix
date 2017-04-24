#pragma once
#include "ComputeOnCUDA.h"

void Compute(ComputeOnCUDA::PU::PressureCalcMethod pressureMethod, ComputeOnCUDA::PU::NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double tmax);
void Constructor(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);
void Destructor();