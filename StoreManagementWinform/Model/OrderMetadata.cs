using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    public class OrderMetadata
    {
        public int ID { get; set; }
        public string Staff { get; set; }
        private double _Total;

        public double Total
        {
            get { return Math.Floor(_Total); }
            set { _Total = value; }
        }
        public DateTime CreatedAt { get; set; }

        public OrderMetadata()
        {

        }
    }
}
