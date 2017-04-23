#pragma once

#include <stdexcept>

using namespace std;

class ComputeOnCUDA {
public:
	class PU {
	public:
		int X, Y;
		double tau;
		double ro;
		double nuM;
		double tmax;
		double h;
		int x0, len;

		enum PressureCalcMethod {
			Poisson,//ур-е Пуассона
			WeakСompressibility //метод слабой сжимаемости
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};
		

		void Calculation(PressureCalcMethod pressureCalcMethod, NavierStokesCalcMethod navierStokesCalcMethod, double* Ux, double* Uy);

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y, double tmax = 0.0);
	};



};
