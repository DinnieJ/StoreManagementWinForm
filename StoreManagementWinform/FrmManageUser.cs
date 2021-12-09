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
    public partial class FrmManageUser : Form
    {
        public List<User> ListUser { get; set; }
        UserRepository userRepo;
        ProductRepository productRepo;
        public FrmManageUser()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            productRepo = new ProductRepository();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            this.Load += FrmManageUser_Load;
            this.dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            this.dataGridView1.AllowUserToAddRows = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.button2.Enabled = true;
            this.button3.Enabled = true;
        }

        private void FrmManageUser_Load(object sender, EventArgs e)
        {
            this.userBindingSource.DataSource = this.ListUser;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ID = ((User)this.dataGridView1.CurrentRow.DataBoundItem).ID;
            if (MessageBox.Show($"Are you sure you want to delete user {ID}", "Notification", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    var index = this.dataGridView1.CurrentRow.Index;
                    userRepo.DeleteUser(ID);
                    this.dataGridView1.Rows.RemoveAt(index);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var frmCreateUser = new FrmCreateUser() { User = new User() })
            {
                if (frmCreateUser.ShowDialog() == DialogResult.OK)
                {
                    this.userBindingSource.Add(frmCreateUser.User);
                }
                else Console.WriteLine(frmCreateUser.DialogResult);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
