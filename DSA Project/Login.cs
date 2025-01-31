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
    public partial class Login : Form
    {
        private UserList userList;

        public Login()
        {
            InitializeComponent();
        }
        public Login(UserList userList)
        {
            this.userList = userList;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string password = textBox2.Text;

            if (userName == "" || password == "")
            {
                MessageBox.Show("Enter username or password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (userList.ValidateUser(userName, password))
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 f2 = new Form2(userList);
                    f2.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Invalid username or password", "Invalid", MessageBoxButtons.OK);
                }
            }

        }
        private void label3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void label9_Click(object sender, EventArgs e)
        {
            Register f3 = new Register(userList);
            f3.Show();
            this.Hide();
        }
    }
}
