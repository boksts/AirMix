// Wrapper.h

#pragma once

#include "..\OpenMP_AM\ComputeOnOMP.h"
#include "..\OpenMP_AM\ComputeOnOMP.cpp"

#include "..\CUDA_AM\ComputeOnCUDA.h"
#include "..\CUDA_AM\ComputeOnCUDA.cpp"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace AirMixParallel {

	/// <summary>
	/// ������ ��������������
	/// </summary>
	 public enum class TurbulenceModel {
		///<summary>��������� ������</summary>
		Secundova = 1,
		///<summary>�-� ������</summary>
		KE = 2
	};

	/// <summary>
	/// ���������� ������������� ����������������
	/// </summary>
	 public enum class PPT{
		///<summary>OpenMP</summary>
		OpenMP,
		///<summary>CUDA</summary>
		CUDA
	};

	public ref class PU
	{

	private:
		array<double> ^Ux;
		array<double> ^Uy;
		int X;
		int Y;
		double *_Ux, *_Uy;

		ComputeOnOMP::PU *computeOnOMP;
		ComputeOnCUDA::PU *computeOnCUDA;
		PPT ppt;

	public:

		/// <summary>
		/// ����� ������� ���� ��������
		/// </summary>
		enum class PressureCalcMethod {
			///<summary>��-� ��������</summary>
			Poisson,
			///<summary>����� ������ �����������</summary>
			Weak�ompressibility
		};

		/// <summary>
		/// ����� ������� ��������� �����-������
		/// </summary>
		enum class NavierStokesCalcMethod{
			///<summary>����� �����</summary>
			ExplicitScheme,
			///<summary>������� �����</summary>
			ImplicitScheme
		};



		///<summary>��������� ���������� �������</summary>
		/// <param name="ppt">���������� ������������� ����������������</param>
		/// <param name="tau">��� �� �������</param>
		/// <param name="ro">���������</param>
		///  <param name="nuM">������������ ��������</param>
		///  <param name="x0">������������ �������� ����� �� ����� �0...</param>
		///  <param name="len">...������ len</param>
		/// <param name="h">��� �� �����</param>
		/// <param name="X">����� ����� �� ��� �</param>
		///  <param name="Y">����� ����� �� ��� �</param>
		PU(PPT ppt, double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);

		

		/// <summary> ���������� � ����������� ������������ ����������</summary>
		/// <param name="pcm">����� ������� ���� ��������</param>
		/// <param name="nscm">����� ������� ��������� �����-������</param>
		///  <param name="tm">������ �������������� (turbulenceModel = 0 ���� �������������� �� �������������)</param>
		///  <param name="Ux">�������� Ux</param>
		///  <param name="Uy">�������� Uy</param>
		/// <param name="tmax">����� �������</param>
		double Calculation(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, TurbulenceModel tm, array<double> ^Ux, array<double> ^Uy, double tmax);

		~PU();

	};


}
