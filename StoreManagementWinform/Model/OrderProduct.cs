using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    public class OrderProduct : Model
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _OrderID;
        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private int _ProductID;
        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private int _Sale;
        public int Sale
        {
            get { return _Sale; }
            set { _Sale = value; }
        }
    }
}
