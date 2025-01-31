using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int startingPoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startingPoint += 1;
            progressBar2.Value = startingPoint;
            if (progressBar2.Value == 100)
            {
                progressBar2.Value = 0;
                timer1.Stop();
                Register register = new Register();
                register.Show();
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
