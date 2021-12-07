using StoreManagementWinform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManagementWinform.DAO
{
    class BaseRepository
    {
        protected DBContext Context { get; }

        public BaseRepository()
        {
            this.Context = new DBContext();
        }
    }
}
