#pragma once
#include  <iostream>
#include <omp.h>

class ComputeOnOMP {
public:

	enum TurbulenceModel {
		Secundova = 1,
		KE = 2
	};

	//������ ��� ���������� �������
	class Time{
		double Tn, Tk;
		time_t start, end;
	public:
		double time;

		void tn(){
			Tn = omp_get_wtime();
		}
		double tk(){
			Tk = omp_get_wtime();
			time = Tk - Tn;
			return time;
		}

	};

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
		
		FILE *f;
	public:
		
		enum PressureCalcMethod {
			Poisson,//��-� ��������
			Weak�ompressibility //����� ������ �����������
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//����� �����
			ImplicitScheme //������� �����
		};

		double Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double tmax);
		void _Poisson();
		void _Weak�ompressibility();
		void Speeds();

		PU(double tau, double ro, double nuM,int x0, int len, double h, int X, int Y);
		~PU();
			
	};

	class WPsi {
	private:
		FILE *f;
		int X, Y;
		double *Ux, *Uy, *Uxn, *Uyn;
		double *psi, *psin, *w, *wn;
		double tau;
		double nuM;
		double h;
		int x0, len;
		const double epsPsi = 0.001;


	public:
		enum HelmholtzCalcMethod {
			ExplicitScheme,
			ImplicitScheme
		};

		double Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, double *Ux, double *Uy, double tmax);
		void Init();
		void CurrentFunction();
		void Vortex();
		void Speeds();

		WPsi(double tau,  double nuM, int x0, int len, double h, int X, int Y, double *Ux, double *Uy);
		~WPsi();
		
	};


};
