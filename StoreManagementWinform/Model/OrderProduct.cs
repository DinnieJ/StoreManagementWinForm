using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.model
{
    class OrderProduct
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



    }
}
