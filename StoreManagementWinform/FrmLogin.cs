using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoreManagementWinform.DAO;
using StoreManagementWinform.Model;

namespace StoreManagementWinform
{
    public partial class FrmLogin : Form
    {
        UserRepository userRepo;
        public User AuthUser;

        public FrmLogin()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            StartPosition = FormStartPosition.CenterParent;
            this.FormClosed += FrmLogin_FormClosed;
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.AuthUser = userRepo.GetUserByCreds(tb_username.Text, tb_password.Text);
            if(AuthUser == null)
            {
                MessageBox.Show("Invalid username or password");
            } else
            {
                this.DialogResult = DialogResult.Yes;
                this.Hide();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
