using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreManagementWinform.Model;
using System.Data.SqlClient;

namespace StoreManagementWinform.DAO
{
    public class OrderRepository : BaseRepository
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
                    "CAST(SUM(([Product].[Price] * [OrderProduct].[Quantity]) * CAST((1 - (CAST([OrderProduct].[Sale] AS FLOAT) / 100)) AS FLOAT)) AS FLOAT) as Total,\n" +
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
                        Console.WriteLine(reader.GetValue(2).GetType().Name);
                        result.Add(new OrderMetadata()
                        {
                            ID = reader.GetInt32(0),
                            Staff = reader.GetString(1),
                            Total = (double)reader.GetValue(2),
                            CreatedAt = reader.GetDateTime(3)
                        });
                    }
                }

                return result;
            });
        }

        public void CreateOrder(List<AddedProduct> cart, User staff)
        {
            var newOrder = Context.ExecuteQuery<Order>(conn =>
            {
                var cmd = conn.CreateCommand();
                var o = new Order();
                cmd.CommandText =
                    "INSERT INTO [Order]([StaffID]) VALUES (@staffID)\n" +
                    "SELECT * FROM [Order] WHERE [ID] = SCOPE_IDENTITY()";

                cmd.Parameters.Add(new SqlParameter("@staffID", System.Data.SqlDbType.Int) { Value = staff.ID });
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        o.ID = reader.GetInt32(0);
                        o.StaffID = staff.ID;
                    }
                }

                return o;
            });

            Context.ExecuteUpdate(conn =>
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO [OrderProduct]([OrderID],[ProductID],[Sale],[Quantity]) VALUES \n";

                for (int i = 0; i < cart.Count; i++)
                {
                    if (i == cart.Count - 1)
                    {
                        cmd.CommandText += $"(@oID{i}, @pID{i}, @sale{i}, @quantity{i})";
                    }
                    else
                    {
                        cmd.CommandText += $"(@oID{i}, @pID{i}, @sale{i}, @quantity{i}),\n";
                    }

                    cmd.Parameters.Add(new SqlParameter($"@oID{i}", System.Data.SqlDbType.Int) { Value = newOrder.ID });
                    cmd.Parameters.Add(new SqlParameter($"@pID{i}", System.Data.SqlDbType.Int) { Value = cart.ElementAt(i).ID });
                    cmd.Parameters.Add(new SqlParameter($"@sale{i}", System.Data.SqlDbType.Int) { Value = cart.ElementAt(i).Sale });
                    cmd.Parameters.Add(new SqlParameter($"@quantity{i}", System.Data.SqlDbType.Int) { Value = cart.ElementAt(i).Quantity });
                }

                cmd.ExecuteNonQuery();
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
                    "CAST(([Product].[Price] * [OrderProduct].[Quantity] * CAST((1 - CAST([OrderProduct].[Sale] AS FLOAT) / 100) AS FLOAT)) AS FLOAT) AS Amount\n" +
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
                            Amount = (double)reader.GetValue(6)
                        });
                    }
                }

                return result;
            });
        }

    }
}
