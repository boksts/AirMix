#pragma once

#include <stdexcept>

using namespace std;

class ComputeOnCUDA {
public:
	class PU {
	public:

		enum PressureCalcMethod {
			Poisson,//��-� ��������
			Weak�ompressibility //����� ������ �����������
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//����� �����
			ImplicitScheme //������� �����
		};
		


		double Calculation(PressureCalcMethod pressureCalcMethod, NavierStokesCalcMethod navierStokesCalcMethod, double* Ux, double* Uy, double tmax);

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);
		~PU();
	};



};
