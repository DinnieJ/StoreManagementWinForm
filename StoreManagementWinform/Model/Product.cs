using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    public class Product
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private double _Price;
        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
    }
}
