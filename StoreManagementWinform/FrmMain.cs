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
    public partial class FrmMain : Form
    {
        UserRepository UserRepo;
        ProductRepository ProductRepo;
        User AuthUser;
        public FrmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.UserRepo = new UserRepository();
            this.ProductRepo = new ProductRepository();
            this.Load += FrmMain_Load;
            List<User> u = UserRepo.getAllUser();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Hide();
            using (var frmLogin = new FrmLogin() { AuthUser = new User() })
            {
                if (frmLogin.ShowDialog() == DialogResult.Yes)
                {
                    this.lb_user.Text = $"Welcome, {frmLogin.AuthUser.Name}";
                    this.AuthUser = frmLogin.AuthUser;
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

        private void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            using (var frmChangePassword = new FrmChangePassword() { User = AuthUser })
            {
                if (frmChangePassword.ShowDialog() == DialogResult.Yes)
                {

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(AuthUser.Role == "ADMIN")
            {
                List<User> list = UserRepo.getAllUser();
                using (var frmManageUser = new FrmManageUser() { ListUser =  list })
                {
                    if (frmManageUser.ShowDialog() == DialogResult.Yes)
                    {

                    }
                }
            } else
            {
                MessageBox.Show("You are not authenticated to use this feature", "Notification");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Product> ls = ProductRepo.GetProducts();
            using (var frmManageProduct = new FrmManageProduct() { list = ls })
            {
                if (frmManageProduct.ShowDialog() == DialogResult.Yes)
                {

                }
            }
        }
    }


}
