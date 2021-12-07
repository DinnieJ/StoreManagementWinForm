using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    public class Order
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _StaffID;
        public int StaffID
        {
            get { return _StaffID; }
            set { _StaffID = value; }
        }

    }
}
