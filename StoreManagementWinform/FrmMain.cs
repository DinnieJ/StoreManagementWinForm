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
        OrderRepository OrderRepo;
        User AuthUser;
        public FrmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.UserRepo = new UserRepository();
            this.ProductRepo = new ProductRepository();
            this.OrderRepo = new OrderRepository();
            this.Load += FrmMain_Load;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            this.dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = ((OrderMetadata)this.dataGridView1.CurrentRow.DataBoundItem).ID;
            List<OrderDetail> result = OrderRepo.GetOrderDetail(ID);

            using (var frmOrderDetail = new FrmOrderDetail() { OrderDetails = result })
            {
                if (frmOrderDetail.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void DataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ID = ((OrderMetadata)this.dataGridView1.CurrentRow.DataBoundItem).ID;
            List<OrderDetail> orderDetails = OrderRepo.GetOrderDetail(ID);

            using (var frmOrderDetail = new FrmOrderDetail() { OrderDetails = orderDetails })
            {
                if(frmOrderDetail.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
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
                    this.orderMetadataBindingSource.DataSource = OrderRepo.GetOrders();
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (var frmCreateOrder = new FrmCreateOrder() { Staff = AuthUser })
            {
                if(frmCreateOrder.ShowDialog() == DialogResult.OK)
                {
                    this.orderMetadataBindingSource.DataSource = OrderRepo.GetOrders();
                }
            }
        }
    }


}
