using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using StoreManagementWinform.DAO;
using StoreManagementWinform.Model;

namespace StoreManagementWinform
{
    public partial class FrmCreateProduct : Form
    {
        public Product product { get; set; }
        ProductRepository ProductRepo;
        public FrmCreateProduct()
        {
            InitializeComponent();
            ProductRepo = new ProductRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_name.Text.Trim() == "") MessageBox.Show("Name can't be empty", "Notification");
            else if (tb_price.Text.Trim() == "") MessageBox.Show("Name can't be empty", "Notification");
            else if (!Regex.IsMatch(tb_price.Text.Trim(), @"^[0-9]*$")) MessageBox.Show("Name can't be empty", "Notification");
            else
            {
                try
                {
                    this.product = ProductRepo.AddProduct(new Product()
                    {
                        Name = tb_name.Text.Trim(),
                        Price = Int32.Parse(tb_price.Text)
                    });
                    this.DialogResult = DialogResult.OK;
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
