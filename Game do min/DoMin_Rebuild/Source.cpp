#include<iostream>
#include<string>
#include<time.h>
#define MAX 100
using namespace std;

int random(int n) // 1
{
	int a = rand() % n + 1;
	return a;
}

void makeSomeBoom(int A[][MAX], int n, int soluong)
{
	int x, y; // x,y la toa do can dat min
	
	while (soluong>0)
	{
		x = random(n); // randrom ra 1 số nguyên trong đoạn 1 - (n )
		y = random(n);
		// Liệu có đặt đại ở x , y được hay không?
		// Không thể đặt đại được.
		if (A[x][y] !=-1) // -1 là mìn. 0 là ô trống
		{
			// Đặt tìm
			A[x][y] = -1;
			// trừ số lượng lại
			soluong--;
		}
	}
}

void inMaTran(int A[][MAX], int n)
{
	for (int i = 1; i <= n; i++)
	{
		for (int j = 1; j <= n; j++)
			cout << A[i][j] << '\t';
		cout << endl;
	}
	cout << endl;
}

void P3X3(int A[][MAX], int n, int x, int y)
{
	//lùi x,y về
	int xnew = x - 1;
	int ynew = y - 1;
	int countBoom = 0;// hàm đếm số bom

	for (int i = xnew; i < xnew + 3; i++)
		for (int j = ynew; j < ynew + 3; j++)

			if (A[i][j] == -1)
				countBoom++;


	A[x][y] = countBoom;
}

// nếu dòng = 0 || n-1
//nếu cột bằng 0 || n -1;


void PutFlag3X3(int A[][MAX], int n)
{
	for(int i = 1; i<=n; i++)
		for (int j = 1; j <= n; j++)
		{
			if (A[i][j] == 0)
				P3X3(A, n, i, j);
		}
}


void inMenu(int &soluong, int n)
{
	// Quy ước: 
	// Cho người dùng chọn thêm số lượng mìn.
	// Số lượng mìn auto bằng 1/4 ma trận.
	cout << "1. So luong min tu chon" << endl;
	cout << "2. So luong min auto" << endl;
	cout << "3.Ban chon: ";
	int tl;
	cin >> tl;
	switch (tl)
	{
	case 1:
	{
		do 
		{
			cout << "So luong min: ";
			cin >> soluong;
		} while (soluong >= n*n);
		break;
	}
	default:
		soluong = n*n / 3;
		break;
	}
	system("cls");
}
int main()
{
	srand(time(NULL));
	int A[MAX][MAX] = {};
	int n;
	cout << "Nhap do lon cua bai min: ";
	cin >> n;

	int soluong;
	inMenu(soluong, n);

	makeSomeBoom(A, n, soluong);
	inMaTran(A, n);
	cout << endl;

	for (int i = 0; i <= n; i++)
	{
		for (int j = 0; j <= n; j++)
			cout << A[i][j] << '\t';
		cout << endl;
	}
	//PutFlag3X3(A, n);
	//inMaTran(A, n);





	system("pause");
	return 0;
}