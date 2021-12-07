using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoreManagementWinform.Model;
using StoreManagementWinform.DAO;

namespace StoreManagementWinform
{
    public partial class FrmChangePassword : Form
    {
        public User User { get; set; }
        UserRepository userRepo;
        public FrmChangePassword()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            StartPosition = FormStartPosition.CenterParent;
            
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (tb_old.Text.Trim() != User.Password) MessageBox.Show("Invalid old password");
            else if (tb_new.Text.Trim() == "") MessageBox.Show("New password can't not be empty");
            else
            {
                try
                {
                    userRepo.ChangePassword(User, tb_new.Text);
                    DialogResult = DialogResult.Yes;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Unknown Error!");
                    DialogResult = DialogResult.No;
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
