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

    public partial class FrmUpdateUser : Form
    {
        public User EditUser { get; set; }
        UserRepository UserRepo;
        public FrmUpdateUser()
        {
            InitializeComponent();
            this.UserRepo = new UserRepository();
        }
    }
}
