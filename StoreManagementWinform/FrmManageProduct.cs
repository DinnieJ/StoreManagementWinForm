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
    public partial class FrmManageProduct : Form
    {
        public List<Product> list { get; set; }
        ProductRepository ProductRepo;
        public FrmManageProduct()
        {
            InitializeComponent();
            this.ProductRepo = new ProductRepository();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ReadOnly = true;
            this.Load += FrmManageProduct_Load;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            this.dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.button2.Enabled = true;
            this.button3.Enabled = true;
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        private void FrmManageProduct_Load(object sender, EventArgs e)
        {
            this.productBindingSource.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var frmCreateProduct = new FrmCreateProduct())
            {
                if(frmCreateProduct.ShowDialog() == DialogResult.OK)
                {
                    this.productBindingSource.Add(frmCreateProduct.product);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ID = ((Product)this.dataGridView1.CurrentRow.DataBoundItem).ID;
            if (MessageBox.Show($"Are you sure you want to delete product {ID}", "Notification", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    var index = this.dataGridView1.CurrentRow.Index;
                    ProductRepo.DeleteProduct(ID);
                    this.dataGridView1.Rows.RemoveAt(index);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
