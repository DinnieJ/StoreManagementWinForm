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
    public partial class FrmCreateOrder : Form
    {
        public DAO.ProductRepository ProductRepo;
        public DAO.OrderRepository OrderRepo;
        public Model.User Staff { get; set; }
        List<Model.Product> ListProduct;
        List<Model.AddedProduct> Cart = new List<Model.AddedProduct>();
        Model.Product SelectedProduct = new Model.Product();
        bool Selected = false;
        public FrmCreateOrder()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;

            this.ProductRepo = new DAO.ProductRepository();
            this.OrderRepo = new DAO.OrderRepository();

            this.ListProduct = ProductRepo.GetProducts();

            tb_product_name.AutoCompleteMode = AutoCompleteMode.Suggest;
            tb_product_name.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tb_product_name.DataSource = ListProduct.AsEnumerable().Select(item => item.Name).ToArray();

            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            coll.AddRange(ListProduct.AsEnumerable().Select(item => item.Name).ToArray());
            tb_product_name.AutoCompleteCustomSource = coll;
            tb_product_name.SelectedIndex = -1;
            tb_product_name.SelectedValueChanged += Tb_product_name_SelectedValueChanged;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
        }

        private void Tb_product_name_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.SelectedProduct = ListProduct.AsEnumerable().Where(item => item.Name.ToLower().Equals(tb_product_name.SelectedValue.ToString().ToLower())).FirstOrDefault();
                this.textBox1.Text = SelectedProduct.ID.ToString();
                this.textBox2.Text = SelectedProduct.Price.ToString();
                Selected = true;
            } catch (NullReferenceException ex)
            {
                this.SelectedProduct = new Model.Product();
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Selected)
            {
                Model.AddedProduct ap = new Model.AddedProduct()
                {
                    ID = SelectedProduct.ID,
                    Name = SelectedProduct.Name,
                    Price = SelectedProduct.Price,
                    Sale = (int)numericUpDown1.Value,
                    Quantity = (int)numericUpDown2.Value
                };
                Selected = false;
                this.addedProductBindingSource.Add(ap);
                this.Cart.Add(ap);
                this.SelectedProduct = new Model.Product();
                this.textBox1.Text = String.Empty;
                this.textBox2.Text = String.Empty;
                this.tb_product_name.SelectedIndex = -1;
                this.numericUpDown1.Value = this.numericUpDown1.Minimum;
                this.numericUpDown2.Value = this.numericUpDown2.Minimum;
                this.label6.Text = $"Total: {GetTotal()} VNĐ";
            }
        }

        private double GetTotal()
        {
            return Cart.AsEnumerable().Aggregate((double)0 , (prev, curr) => prev + curr.Amount);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(this, "Cancel current Order ?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OrderRepo.CreateOrder(Cart, Staff);
                this.DialogResult = DialogResult.OK;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
    }
}
