using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StoreManagementWinform
{
    public partial class FrmUpdateProduct : Form
    {
        public Model.Product SelectedProduct { get; set; }
        public DAO.ProductRepository ProductRepo;
        public FrmUpdateProduct()
        {
            InitializeComponent();
            Load += FrmUpdateProduct_Load;
            ProductRepo = new DAO.ProductRepository();
        }

        private void FrmUpdateProduct_Load(object sender, EventArgs e)
        {
            this.tb_id.Text = SelectedProduct.ID.ToString();
            this.tb_name.Text = SelectedProduct.Name;
            this.tb_price.Text = SelectedProduct.Price.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_name.Text.Trim().Equals(String.Empty)) MessageBox.Show(this, "Name can't be empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (tb_price.Text.Trim().Equals(String.Empty)) MessageBox.Show(this, "Price can't be empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!Regex.IsMatch(tb_price.Text.Trim(), @"^[0-9]*$")) MessageBox.Show(this, "Name can't be empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    SelectedProduct.Name = tb_name.Text;
                    SelectedProduct.Price = Int32.Parse(tb_price.Text);
                    ProductRepo.UpdateProduct(SelectedProduct);
                    DialogResult = DialogResult.OK;
                } catch(Exception ex) 
                {
                    MessageBox.Show(this, ex.InnerException.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
