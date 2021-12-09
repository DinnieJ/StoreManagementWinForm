using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace StoreManagementWinform.Utils
{
    class DBContext
    {
        private readonly string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        private SqlConnection connection;

        public DBContext()
        {
            connection = new SqlConnection(CONNECTION_STRING);
        }

        public delegate T QueryCallbackFn<T>(SqlConnection connection);
        public delegate void UpdateCallbackFn(SqlConnection connection);

        public T ExecuteQuery<T>(QueryCallbackFn<T> fn)
        {
            try
            {
                if(connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                T result = fn(this.connection);
                
                return result;
            } catch (Exception e)
            {
                if(connection.State == System.Data.ConnectionState.Open) 
                    connection.Close();
                Console.WriteLine(e.Message);
                throw new Exception("Context Error", e);
            } finally
            {
                connection.Close();
            }
        } 

        public bool ExecuteUpdate(UpdateCallbackFn fn)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                fn(connection);
            } catch(Exception e)
            {
                if (connection.State == System.Data.ConnectionState.Open) 
                    connection.Close();
                Console.WriteLine(e.Message);
                throw new Exception("Context Error", e);
                
            } finally
            {
                connection.Close();
            }
            return true;
        }

        public SqlConnection Connection
        {
            get { return this.connection; }
        }

        ~DBContext()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
