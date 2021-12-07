using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.Model
{
    public class Model
    {
        protected DateTime _CreatedAt;
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { _CreatedAt = value; }
        }

        protected DateTime _UpdatedAt;
        public DateTime UpdatedAt
        {
            get { return _UpdatedAt; }
            set { _UpdatedAt = value; }
        }

        public Model() { }
    }
}
