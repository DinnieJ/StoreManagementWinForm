using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using StoreManagementWinform.Model;

namespace StoreManagementWinform.DAO
{
    class ProductRepository : BaseRepository
    {
        public List<Product> GetProducts()
        {
            return Context.ExecuteQuery<List<Product>>(con =>
            {
                SqlCommand command = new SqlCommand(null, con)
                {
                    CommandText =
                        "SELECT * FROM [Product]"
                };
                var reader = command.ExecuteReader();
                List<Product> result = new List<Product>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Product()
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetInt32(2),
                            CreatedAt = reader.GetDateTime(3),
                            UpdatedAt = reader.GetDateTime(4),
                        });
                    }
                }
                return result;
            });
        }

        public Product AddProduct(Product p)
        {
            return Context.ExecuteQuery<Product>((conn) =>
            {
                SqlCommand command = new SqlCommand(null, conn)
                {
                    CommandText =
                        "INSERT INTO [Product]([Name],[Price]) VALUES \n" +
                        "(@name,@price)\n" +
                        "SELECT * FROM [Product] WHERE ID = SCOPE_IDENTITY()"
                };

                command.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.NVarChar) { Value = p.Name });
                command.Parameters.Add(new SqlParameter("@price", System.Data.SqlDbType.NVarChar) { Value = p.Price });
                var reader = command.ExecuteReader();
                Product product = null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        product = new Product()
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetInt32(2),
                            CreatedAt = reader.GetDateTime(3),
                            UpdatedAt = reader.GetDateTime(4)
                        };
                    }
                }
                return product;
            });
        }

        public void DeleteProduct(int id)
        {
            Context.ExecuteUpdate(conn =>
            {
                SqlCommand command = new SqlCommand(null, conn)
                {
                    CommandText = "DELETE FROM [Product] WHERE [Product].[ID] = @id"
                };
                command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = id });

                command.ExecuteNonQuery();
            });
        }

    }
}
