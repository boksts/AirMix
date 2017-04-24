#pragma once
#include  <iostream>

class ComputeOnOMP {
public:
	class PU {
	private:
	
		int X, Y;
		double *Ux,*Uy, *Uxn, *Uyn;
		double tau;
		double nuM;
		double *divU;
		double *P;
		double ro;
		double h;
		int x0, len;
		const double b = 100;
		
		
	public:
		FILE *f;
		enum PressureCalcMethod {
			Poisson,//ур-е Пуассона
			WeakСompressibility //метод слабой сжимаемости
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};

		void Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double tmax);
		void _Poisson();
		void _WeakСompressibility();
		void Speeds();

		PU(double tau, double ro, double nuM,int x0, int len, double h, int X, int Y);
		~PU();
			
	};


};
