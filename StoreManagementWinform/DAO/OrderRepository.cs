using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreManagementWinform.Model;
using System.Data.SqlClient;

namespace StoreManagementWinform.DAO
{
    class OrderRepository : BaseRepository
    {
        public List<OrderMetadata> GetOrders()
        {
            return Context.ExecuteQuery<List<OrderMetadata>>(conn =>
            {
                List<OrderMetadata> result = new List<OrderMetadata>();
                SqlCommand command = conn.CreateCommand();

                command.CommandText =
                    "SELECT\n" +
                    "[Order].[ID],\n" +
                    "[User].[Name] as Staff,\n" +
                    "SUM(([Product].[Price] * [OrderProduct].[Quantity]) * (1 - ([OrderProduct].[Sale] / 100))) as Total,\n" +
                    "[Order].[CreatedAt]\n" +
                    "FROM [Order]\n" +
                    "INNER JOIN [OrderProduct] ON [Order].[ID] = [OrderProduct].[OrderID]\n" +
                    "INNER JOIN [Product] ON [Product].[ID] = [OrderProduct].[ProductID]\n" +
                    "INNER JOIN [User] ON [Order].[StaffID] = [User].[ID]\n" +
                    "GROUP BY [Order].[ID], [User].[Name], [Order].[CreatedAt]\n" +
                    "ORDER BY [Order].[CreatedAt] DESC";

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new OrderMetadata()
                        {
                            ID = reader.GetInt32(0),
                            Staff = reader.GetString(1),
                            Total = reader.GetInt32(2),
                            CreatedAt = reader.GetDateTime(3)
                        });
                    }
                }

                return result;
            });
        }

        public List<OrderDetail> GetOrderDetail(int ID)
        {
            return Context.ExecuteQuery(conn =>
            {
                List<OrderDetail> result = new List<OrderDetail>();
                SqlCommand command = conn.CreateCommand();

                command.CommandText =
                    "SELECT\n" +
                    "[OrderProduct].[ID],\n" +
                    "[Product].[ID] as ProductID,\n" +
                    "[Product].[Name] as ProductName,\n" +
                    "[Product].[Price] as OriginalPrice,\n" +
                    "[OrderProduct].[Quantity] as Quantity,\n" +
                    "[OrderProduct].[Sale] as Sale,\n" +
                    "([Product].[Price] * [OrderProduct].[Quantity]) * (1 - ([OrderProduct].[Sale] / 100)) as Amount\n" +
                    "FROM [OrderProduct]\n" +
                    "INNER JOIN [Order] ON [OrderProduct].[OrderID] = [Order].[ID]\n" +
                    "INNER JOIN [Product] ON [OrderProduct].[ProductID] = [Product].[ID]\n" +
                    "WHERE [Order].[ID] = @orderID";

                command.Parameters.Add(new SqlParameter("@orderID", System.Data.SqlDbType.Int) { Value = ID });

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new OrderDetail()
                        {
                            ID = reader.GetInt32(0),
                            ProductID = reader.GetInt32(1),
                            ProductName = reader.GetString(2),
                            OriginalPrice = reader.GetInt32(3),
                            Quantity = reader.GetInt32(4),
                            Sale = reader.GetInt32(5),
                            Amount = reader.GetInt32(6)
                        });
                    }
                }

                return result;
            });
        }
    }
}
