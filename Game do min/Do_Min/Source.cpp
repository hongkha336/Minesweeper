/*
NHOM 11
Dang Nguyen Hong Kha
16110112

Do Min

Yeu cau:
1. Phat sinh ngau nhien vi tri so min.
2. Tinh toan cac gia tri cua cac o con lai

*/
#include<iostream>
#include <time.h>
using namespace std;

int random(int n)
{
	int a = rand() % n;
	return a;
}

void makeThemZero(int A[][100], int n)
{
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n; j++)
			A[i][j] = 0;
}

void gotoLeft(int A[][100], int dong, int a, int b)
{
	for (int i = a; i <= b; i++)
		if (A[dong][i] == -1)
			return;
		else
		{
			A[dong][i] = -2;
		}
}

void gotoRight(int A[][100], int dong, int a, int b)
{
	for (int i = b; i >= a; i--)
		if (A[dong][i] == -1)
			return;
		else
		{
			A[dong][i] = -2;
		}
}

void xuatMang(int A[][100], int n)
{
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
			cout << A[i][j] << " ";
		cout << endl;
	}
}


bool isExistBoomInThatLine(int A[][100], int n, int a, int b, int LINE)
{
	for (int i = a; i <= b; i++)
	{
		if (A[LINE][i] == -1)
			return true;
	}
	return false;
}

int putflag(int A[][100], int n, int dong, int cot, int &count, int &right)
{
	int flag = cot;
	while (flag < n && A[dong][flag + 1] == -1)
	{
		count++;
		flag++;
	}
	A[dong][flag + 1] = -2;
	right = flag + 1;
	flag = cot;
	while (flag >= 0 && A[dong][flag - 1] == -1)
	{
		count++;
		flag--;
	}
	A[dong][flag - 1] = -2;
	/*if (flag == 0)
	return 0;*/
	return flag - 1;
}

void spand(int A[][100], int n, int dong, int cot)
{
	int count = 1;
	int d_up = dong;
	int d_down = dong;
	int right;
	int left = putflag(A, n, dong, cot, count, right);
	//cout << endl;
	//xuatMang(A, n);
	while (isExistBoomInThatLine(A, n, left, right, d_up - 1) && d_up >= 1)
	{
		d_up--;
		if (A[d_up][left] != -1)
		{
			gotoLeft(A, d_up, left, right);
			//cout << endl;
			//xuatMang(A, n);
		}
		if (A[d_up][right] != -1)
		{
			gotoRight(A, d_up, left, right);
			//cout << endl;
			//xuatMang(A, n);
		}
		if (A[d_up][left] == -1)
		{
			left = putflag(A, n, d_up, left, count, right);
			//cout << endl;
			//xuatMang(A, n);
		}
		if (A[d_up][right] == -1)
		{
			left = putflag(A, n, d_up, right, count, right);
			/*	cout << endl;
			xuatMang(A, n);*/
		}
	}
	if (!isExistBoomInThatLine(A, n, left, right, d_up - 1) && d_up >= 1)
	{
		for (int i = left + 1; i <= right - 1; i++)
		{
			A[d_up - 1][i] = -2;
		}
		/*	cout << endl;
		xuatMang(A, n);*/
	}
	while (isExistBoomInThatLine(A, n, left, right, d_down + 1) && d_down<n - 1)
	{
		d_down++;
		if (A[d_down][left] != -1)
		{
			gotoLeft(A, d_down, left, right);
			/*cout << endl;
			xuatMang(A, n);*/
		}
		if (A[d_down][right] != -1)//
		{
			gotoRight(A, d_down, left, right);
			/*cout << endl;
			xuatMang(A, n);*/
		}
		if (A[d_down][left] == -1)
		{
			left = putflag(A, n, d_down, left, count, right);
			/*cout << endl;
			xuatMang(A, n);*/
		}
		if (A[d_down][right] == -1)
		{
			left = putflag(A, n, d_down, right, count, right);
			/*cout << endl;
			xuatMang(A, n);*/
		}
	}
	if (!isExistBoomInThatLine(A, n, left, right, d_down + 1) && d_down < n - 1)
	{
		for (int i = left + 1; i <= right - 1; i++)
		{
			A[d_down + 1][i] = -2;
		}
		/*cout << endl;
		xuatMang(A, n);*/
	}

	for (int d = 0; d < n; d++)
		for (int c = 0; c < n; c++)
			if (A[d][c] == -2)
				A[d][c] = count;
}


void makeAllFlag(int A[][100], int n)
{
	for (int i = 0; i<n; i++)
		for (int j = 0; j < n; j++)
		{
			if (A[i][j] == -1)
			{
				spand(A, n, i, j);

			}
		}
}


void makeSomeBoom(int A[][100], int n, int count)
{
	int dong, cot;
	while (count > 0)
	{
		dong = random(n);
		cot = random(n);
		A[dong][cot] = -1;
		count--;
	}
}

void nhapMang(int A[][100], int n)
{
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n; j++)
			cin >> A[i][j];
}


int main()
{

	int A[100][100], n;
	cin >> n;
	int count = n*n / 4;
	makeThemZero(A, n);
	makeSomeBoom(A, n,count);
	cout << "Dat min: " << endl;
	xuatMang(A, n);


	makeAllFlag(A, n);
	cout << endl;
	cout << "Dat gia tri: " << endl;
	cout << endl;
	xuatMang(A, n);
	system("pause");
	return 0;
}