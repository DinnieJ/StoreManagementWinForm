using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using StoreManagementWinform.Model;

namespace StoreManagementWinform
{
    public partial class FrmOrderDetail : Form
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public FrmOrderDetail()
        {
            InitializeComponent();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.Load += FrmOrderDetail_Load;

            this.dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        private void FrmOrderDetail_Load(object sender, EventArgs e)
        {
            this.orderDetailBindingSource.DataSource = this.OrderDetails;
            this.textBox1.Text = $"{OrderDetails.AsEnumerable().Aggregate(0, (prev, current) => prev + current.Amount)} VNĐ";
        }

        private void orderDetailBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
