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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Hide();
            using (var frmLogin = new FrmLogin() { username = "" })
            {
                if (frmLogin.ShowDialog() == DialogResult.Yes)
                {
                    this.lb_user.Text = $"Welcome, {frmLogin.username}";
                    this.Show();
                }
                else Application.Exit();
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }


}
