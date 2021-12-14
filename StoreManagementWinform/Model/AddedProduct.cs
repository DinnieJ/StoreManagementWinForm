using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    public class AddedProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Sale { get; set; }
        public int Price { get; set; }
        public double Amount
        {
            get { return (Price * Quantity) * (1 - ((double)Sale / 100)); }
        }

        public AddedProduct()
        {

        }
    }
}
