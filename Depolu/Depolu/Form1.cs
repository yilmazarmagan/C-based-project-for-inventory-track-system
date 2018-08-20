using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depolu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="admin" && textBox2.Text =="password")
            {
                Form2 yeni = new Form2();
                this.Visible = false;
                yeni.Show();
            }
            else
            {
                DialogResult secim = MessageBox.Show("Kullanıcı girişi başarısız.Lütfen tekrar deneyiniz.", "Uyarı", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if(secim == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void form1_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
