using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    class OrderMetadata
    {
        public int ID { get; set; }
        public string Staff { get; set; }
        public int Total { get; set; }
        public DateTime CreatedAt { get; set; }

        public OrderMetadata()
        {

        }
    }
}
