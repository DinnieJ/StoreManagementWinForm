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
using System.Globalization;

namespace StoreManagementWinform
{
    public partial class FrmCreateUser : Form
    {
        public User User { get; set; }

        UserRepository UserRepo;
        public FrmCreateUser()
        {
            InitializeComponent();
            UserRepo = new UserRepository();
            this.User = new User();
            cb_role.SelectedIndex = 0;
            this.dt_dob.MaxDate = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(tb_phonenum.Text);
            if (tb_name.Text.Trim() == "") MessageBox.Show("Name can't not be empty", "Notification");
            else if (tb_username.Text.Trim() == "") MessageBox.Show("Username can't not be empty", "Notification");
            else if (tb_password.Text.Trim() == "") MessageBox.Show("Password can't not be empty", "Notification");
            else if (tb_phonenum.Text.Trim() == "") MessageBox.Show("Phone num can;t be empty", "Notification");
            else if (!Regex.IsMatch(tb_phonenum.Text.Trim(), @"^[0-9]+$")) MessageBox.Show("Invalid phone num", "Notification");
            else
            {
                try
                {
                    this.User = UserRepo.AddUser(new User()
                    {
                        Name = tb_name.Text.Trim(),
                        Username = tb_username.Text.Trim(),
                        Password = tb_password.Text.Trim(),
                        PhoneNumber = tb_phonenum.Text.Trim(),
                        DateOfBirth = dt_dob.Value.Date,
                        Role = cb_role.SelectedItem.ToString()
                    });
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
