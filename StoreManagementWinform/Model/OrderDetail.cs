using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int OriginalPrice { get; set; }
        public int Quantity { get; set; }
        public int Sale { get; set; }
        public int Amount { get; set; }

        public OrderDetail()
        {
        }
    }
}
