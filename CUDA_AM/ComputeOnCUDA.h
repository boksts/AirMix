#pragma once

#include <stdexcept>

using namespace std;

class ComputeOnCUDA {
public:
	class PU {
	public:

		enum PressureCalcMethod {
			Poisson,//ур-е Пуассона
			WeakСompressibility //метод слабой сжимаемости
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};
		


		double Calculation(PressureCalcMethod pressureCalcMethod, NavierStokesCalcMethod navierStokesCalcMethod, double* Ux, double* Uy, double tmax);

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);
		~PU();
	};



};
