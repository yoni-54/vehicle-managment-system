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
    public partial class Register : Form
    {
        private UserList userlist = new UserList();

        public Register(UserList userlist)
        {
            this.userlist = userlist; 
            InitializeComponent();
        }
        public Register()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string Password = textBox2.Text;
            userlist.AddUser(userName,Password);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string Password = textBox2.Text;
            userlist.RemoveUser(userName,Password);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login login = new Login(userlist);
            login.Show();
            this.Hide();
        }
    }
}
