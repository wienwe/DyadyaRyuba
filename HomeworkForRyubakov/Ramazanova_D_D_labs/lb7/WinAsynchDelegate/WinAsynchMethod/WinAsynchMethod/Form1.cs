using System;
using System.Windows.Forms;

namespace WinAsynchMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int Summ(int a, int b)
        {
            System.Threading.Thread.Sleep(9000);
            return a + b;
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            int a, b;
            try
            {
                a = Int32.Parse(txbA.Text);
                b = Int32.Parse(txbB.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("��� ���������� �������������� ����� �������� ������");
                txbA.Text = txbB.Text = "";
                return;
            }

            int result = await Task.Run(() => Summ(a, b));
            MessageBox.Show($"����� ��������� ����� ����� {result}", "��������� ��������");
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            MessageBox.Show("������ �����!!!");
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace);
        }
    }
}