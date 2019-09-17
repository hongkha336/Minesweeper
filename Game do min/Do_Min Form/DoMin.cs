using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Do_Min_Form
{
    public partial class DoMin : Form
    {
        DateTime start = DateTime.Now;
        private int[,] A;
        int n;

        private int[,] B;
        int Conlai;
        int soMin;


        private int[,] C;
        public DoMin()
        {
            
            InitializeComponent();
            A = new int[100, 100];
            B = new int[100, 100];
            C = new int[100, 100];
        }

        private int random(int n)
        {
            Random Rd = new Random();
            return (Rd.Next(1, n));
          
        }

        private void initMatrix(int[,] A, int n)
        {
            for (int i = 0; i < n + 2; i++)
                for (int j = 0; j < n + 2; j++)
                {
                    A[i, j] = 0;
                    B[i, j] = 0;
                    C[i, j] = 0;
                }
        }

      private  void makeSomeBoom(int [,] A, int n, int soluong)
        {
	        int x, y; // x,y la toa do can dat min
	
	        while (soluong>0)
	            {
                 
                x = random(n); // randrom ra 1 số nguyên trong đoạn 1 - (n )
            Thread.Sleep(13);
		    y = random(n);
          
		    // Liệu có đặt đại ở x , y được hay không?
		    // Không thể đặt đại được.
		    if (A[x,y] !=-1) // -1 là mìn. 0 là ô trống
		        {
			// Đặt tìm
			    A[x,y] = -1;
			    // trừ số lượng lại
			        soluong--;
		        }
	            }
        }

        void P3X3(int [,] A, int n, int x, int y)
            {
	        //lùi x,y về
	        int xnew = x - 1;
	        int ynew = y - 1;
	        int countBoom = 0;// hàm đếm số bom

	            for (int i = xnew; i < xnew + 3; i++)
		            for (int j = ynew; j < ynew + 3; j++)
			            if (A[i,j] == -1)
				        countBoom++;
	                A[x,y] = countBoom;
            }
        // Số lượng ô Width
        private void CreateChessTable(int Width)
        {
    
            flpBanCo.Controls.Clear(); // xóa trống flp
            // 30 là chiều dài của một ô
            // tính toán chiều dài, chiều rộng của flp dựa trên 1 ô. Với ô btn có chiều dài x rộng = 30x30
            flpBanCo.Width = Width * 30 + Width + 2;
            flpBanCo.Height = Width * 30 + Width + 1;

            // Đặt vị trí flp ở giữa
            flpBanCo.Left = (this.Width - flpBanCo.Width) / 2;

            // Đặt trục tung
            int y = (this.Height - flpBanCo.Height) / 2;
            // Vị trí Top của flp chạm pannel btn
            if (y > pnButton.Bottom + 30)
                flpBanCo.Top = y;
            else
                flpBanCo.Top = pnButton.Bottom + 30;
            // Sinh btn
            for (int i = 0; i < Width * Width; i++)
            {
                Button btn = new Button() { Height = 30, Width = 30 };
                // Tên giống như tên của thứ tự
                btn.Name =  "btx"+(i+1).ToString();
                // Màu giống như mặc định
                btn.ForeColor = this.ForeColor;
             //   btn.BackColor = Color.Red;
             // Căn trên , dưới, trái, phải
                btn.Margin = new Padding(0, 0, 0, 0);
             // Thêm action Click cho btn
               // btn.Click += Btn_Click;
                btn.MouseDown += btn_MouseDown;
              // Thêm btn vừa tạo vào Control
                flpBanCo.Controls.Add(btn);
            }
        }

        
        void btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender; //lấy button đang được click
            int x = 0; int y = 0;
            string name = btn.Name.ToString();
            int no = Convert.ToInt16(takeNo(name));
            takePlace(no, ref x, ref y);

            int a = y, b = y;
//===========================================================================

            if (e.Button == MouseButtons.Right)
            {
                if (B[x, y] == 0)
                {
                    if (C[x, y] == 0)
                    {
                        C[x, y] = 1;
                    }
                    else
                        C[x, y] = 0;


                    bieudienMatran();
                }
            }
            else
            {
    
                if (A[x, y] == -1)
                {
                    // show Boom
                    B[x, y] = -1;


                    if (checkBox3.CheckState == CheckState.Unchecked)
                    {
                        Showbanco();
                        flpBanCo.Enabled = false;
                        timer2.Enabled = false;
                        MessageBox.Show("THUA RỒI!!!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        checkBox1.Enabled = true;
                        panel2.Enabled = true;
                        checkBox1.CheckState = CheckState.Unchecked;
                        checkBox2.CheckState = CheckState.Unchecked;
                        checkBox3.CheckState = CheckState.Unchecked;
                        return;
                    }
                    else
                    {
                        soMin--;
                        label2.Text = soMin.ToString();
                    }

                }

                // Ngang
                if (A[x, y] != 0)
                {

                    B[x, y] = -1;
                }
                else
                {

                    doLine(ref x, ref y, ref a, ref b);
                    int a2 = a;
                    int b2 = b;

                    int x2 = x - 1;
                    int x3 = x + 1;

                    int a3 = a;
                    int b3 = b;

                    int a4 = a;
                    int b4 = b;

                    if (x2 != 0)
                    {
                        int temp = isExist(a, b, x2);
                        int temp2 = isExist2(a2, b2, x2);
                        while ((temp != -1 && x2 != 0) || (temp2 != -1 && x2 != 0))
                        {

                            if (temp != -1)
                            {
                                doLine(ref x2, ref temp, ref a, ref b);
                            }
                            if (temp2 != -1)
                            {
                                doLine(ref x2, ref temp2, ref a2, ref b2);
                            }
                            x2--;
                            temp = isExist(a, b, x2);
                            temp2 = isExist2(a2, b2, x2);
                        }
                    }
                    if (x3 != n + 1)
                    {
                        int temp = isExist(a3, b3, x3);
                        int temp2 = isExist2(a4, b4, x3);
                        while ((temp != -1 && x3 != n + 1) || (temp2 != -1 && x3 != n + 1))
                        {

                            if (temp != -1)
                            {
                                doLine(ref x3, ref temp, ref a3, ref b3);
                            }
                            if (temp2 != -1)
                            {
                                doLine(ref x3, ref temp2, ref a4, ref b4);
                            }
                            x3++;
                            temp = isExist(a3, b3, x3);
                            temp2 = isExist2(a4, b4, x3);
                        }
                    }
                }
                bieudienMatran();
            }
        }


        private void Showbanco()
        {
            for(int i = 1; i<=n; i++)
                for(int j = 1; j<=n; j++)
                    if(A[i,j] == -1)
                    {
                        B[i, j] = -1;
                    }
            bieudienMatran();
        }

        void conlai()
        {
            int count = 0;
            for(int i = 1; i <=n; i++)
                for(int j = 1; j<=n; j++)
                    if(B[i,j] !=0 )
                    {
                        count++;
                    }
            Conlai = n*n- count ;
            label5.Text = Conlai.ToString();

            if (Conlai == soMin && checkBox3.CheckState == CheckState.Unchecked)
            {
                timer2.Enabled = false;
                MessageBox.Show("CHÚC MỪNG CHIẾN THẮNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkBox1.Enabled = true;
                panel2.Enabled = true;
                checkBox1.CheckState = CheckState.Unchecked;
                checkBox2.CheckState = CheckState.Unchecked;
                checkBox3.CheckState = CheckState.Unchecked;
                return;
            }
            if(Conlai == 0 && checkBox3.CheckState == CheckState.Checked)
            {
                timer2.Enabled = false;
                checkBox1.Enabled = true;
                panel2.Enabled = true;
                checkBox1.CheckState = CheckState.Unchecked;
                checkBox2.CheckState = CheckState.Unchecked;
                checkBox3.CheckState = CheckState.Unchecked;
                return;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
          
        }


        void doLine(ref int x, ref int y, ref int a, ref int b)
        {
            while (y >= 1 && A[x, y] == 0)
            {
                B[x, y] = 1;
          
                y--;
            }

            B[x, y] = -1;
        

            a = y;
            y++;

            while (y <= n && A[x, y] == 0)
            {
                B[x, y] = 1;
    
                y++;
            }
            b = y;
            B[x, y] = -1;
  
            a++;
            b--;

            for(int i = a; i <=b; i++)
            {
                if (A[x - 1, i] != 0 && A[x - 1, i] != -1)
                {
                    B[x - 1, i] = -1;
              
                }
                if (A[x + 1, i] != 0 && A[x + 1, i] != -1)
                {
                    B[x + 1, i] = -1;
                    
                }
            }

        }

        int isExist(int a, int b, int x)
        {
            for(int i = a; i<=b; i++)
            {
                if (A[x, i] == 0)
                    return i;
            }
            return -1;
        }
        int isExist2(int a, int b, int x)
        {
            for (int i = b; i >= a; i--)
            {
                if (A[x, i] == 0)
                    return i;
            }
            return -1;
        }

        private void Toxy(ref int x, ref int y, int k, int n)
        {
            int i, j;
            int count = 0;
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    count++;
                    if (count == k)
                        break;
                }
                if (count == k)
                    break;
            }
        }

        private void PutFlag3X3(int [,] A, int n)
        {
	            for(int i = 1; i<=n; i++)
		            for (int j = 1; j <= n; j++)
		            {
			            if (A[i,j] == 0)
				            P3X3(A, n, i, j);
		            }
        }       


        private void DoMin_Load(object sender, EventArgs e)
        {
            txtKichThuoc4.Text = "10";
            panel2.Visible = false;
            panel2.Left = this.Width / 2 - panel2.Width / 2;
            pnButton.Left = this.Width / 2 - pnButton.Width / 2 - 3;
            panel3.Top = this.Height / 2 - panel3.Height/2 ;
            panel3.Left = this.Width / 2 - panel3.Width / 2;
           

        }

        void cheat()
        {
            for(int i = 1; i<=n; i++)
                for(int j = 1; j<=n; j++)
                {
                    if (A[i, j] == -1)
                        C[i, j] = 1;
                }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            n = Convert.ToInt16(txtKichThuoc4.Text);
            CreateChessTable(Convert.ToInt16(txtKichThuoc4.Text));
            initMatrix(A, n);
            soMin = n * n / 6;
            if (!txtSoMin.Equals("") && checkBox1.Checked)
                soMin = Convert.ToInt16(txtSoMin.Text.Trim());
            
           int k = soMin;
           label2.Text = (k).ToString();
            makeSomeBoom(A, n, k);
            PutFlag3X3(A, n);
            Conlai = n * n ;
           
            label5.Text = Conlai.ToString() ;

          //  doit();
            flpBanCo.Enabled = true;
            timer2.Enabled = true;
            start = DateTime.Now;

            checkBox1.Enabled = false;
            panel2.Enabled = false;

            if (checkBox2.Checked)
            {
                cheat();
                bieudienMatran();
            }
        }

       bool isNo(char a)
        {
            try
            {
                Convert.ToInt16(a.ToString());
                return true;
            }
            catch { return false; }
        }
       string takeNo(string a)
       {
           string kq = "";
            while(isNo(a[a.Length -1]))
            {

                kq = a.Substring(a.Length - 1, 1) + kq;
                a = a.Substring(0, a.Length - 1);
            }
            return kq;
       }
        
       void takePlace(int a, ref int x, ref int y)
       {
           int i = 1;
           int j = 1;
           int count = 0;
           for (i = 1; i <= n; i++)
           {
               for (j = 1; j <= n; j++)
               {
                   count++;
                   if (count == a)
                       break;
               }
               if (count == a)
                   break;
           }

           x = i;
           y = j;
           
       }

        void kiemtra()
       {
           string temp = "";
            for(int i = 1; i<=n; i++)
            {
                for (int j = 1; j <= n; j++)
                    temp = temp + A[i, j].ToString() + '\t';
                temp = temp + '\n';
            }
           // richTextBox1.Text = temp;
       }


        private void bieudienMatran()
        {
            

            conlai();
            foreach (Control item in flpBanCo.Controls)
            {
                if(item.GetType() == typeof(Button) && item.Name.Substring(0,3) == "btx")
                {
                 
                    int no = Convert.ToInt16(takeNo(item.Name.ToString()));
                    
                    int x = 0;
                    int y = 0;
                    takePlace(no, ref x, ref y);
                    if(C[x,y] !=0)
                    {
                        item.Text = "x";
                        item.ForeColor = Color.Red;
                    }
                    else
                    {
                        item.Text = "";
                        item.ForeColor = this.ForeColor;
                    }
                    if(B[x,y] == 1)
                    {
                        item.BackColor = Color.DarkGray;
                        item.Text = "";
                    }
                    if(B[x,y] == -1 )
                    { 
                   string text = A[x, y].ToString();
                   if (text.Equals("0"))
                       text = "";
                   item.Text = text;
                   if (text.Equals("1"))
                   {
                       item.ForeColor = Color.Green;
                       item.BackColor = Color.DarkGray;
                   }
                   else
                   {
                       if (text.Equals("2"))
                       {
                           item.ForeColor = Color.Blue;
                           item.BackColor = Color.DarkGray;
                       }
                       else
                       {
                           if (text.Equals("-1"))
                           {
                               item.Text = "*";
                               item.ForeColor = Color.Black;
                               item.BackColor = Color.Red;
                          
                           }
                           else
                           {
                               item.ForeColor = Color.Red;
                               item.BackColor = Color.DarkGray;
                           }
                       }
                   }
                   }
                }  
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
            
               
                panel2.Visible = true;
                txtSoMin.Text = "16";
              
            }
            else
            {
                label4.Visible = true;
                label5.Visible = true;
                panel2.Visible = false;
                
            }
        }

        private void txtKichThuoc4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }

        private void txtKichThuoc4_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string a = (DateTime.Now - start).ToString();
            DateTime passTime = DateTime.Parse(a);
            string b = passTime.ToLongTimeString().ToString();

            b = b.Substring(3, b.Length - 3);
            b = b.Substring(0, b.Length - 2);
            label6.Text = b;
        }

  
        private void DoMin_SizeChanged(object sender, EventArgs e)
        {
          
            panel2.Top = this.Height - 100;
            panel2.Left = this.Width/2 - panel2.Width/2;
            pnButton.Left = this.Width / 2 - pnButton.Width/2 - 3;

            panel3.Top = this.Height / 2  - panel3.Height/2;
            panel3.Left = this.Width / 2 - panel3.Width / 2;
        }

        private void DoMin_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Width < pnButton.Width+7)
                this.Width = pnButton.Width+7;
            if (this.Height < 493)
                this.Height = 493;
        }
     }
        
}
