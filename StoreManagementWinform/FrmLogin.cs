using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StoreManagementWinform
{
    public partial class FrmLogin : Form
    {
        public string username { get; set; }
        public FrmLogin()
        {
            InitializeComponent();
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
            username = tb_username.Text;
            this.Hide();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
