using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokoSepatuApp.Model.Context;
using TokoSepatuApp.Model.Entity;

namespace TokoSepatuApp.Model.Repository
{
    internal class OrdersRepository
    {
        private SQLiteConnection _conn;

        public OrdersRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        // TODO : check stock + perhitungan harga

        public int Create(Orders orders, OrderDetails orderDetails)
        {
            int result = 0;
            string sql = @"insert into orders (order_no, date, customer_id, total) values ('INV'||strftime('%Y%m%d%H%M%s'), datetime(), @customer_id, @total)";
            string sqlDetail = @"insert into order_detail (order_id, product_size_id, amount, total) values (@order_id, @product_size_id, @amount, @total)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@customer_id", orders.CustomerId);
                cmd.Parameters.AddWithValue("@total", orders.Total);
                try
                {
                    result = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }


            string sqlGetId = @"select last_insert_rowid()";
            using (SQLiteCommand cmdId = new SQLiteCommand(sqlGetId, _conn))
            {
                long lastId = (long)cmdId.ExecuteScalar();

                using (SQLiteCommand cmd = new SQLiteCommand(sqlDetail, _conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", lastId);
                    cmd.Parameters.AddWithValue("@product_size_id", orderDetails.ProductIdSize);
                    cmd.Parameters.AddWithValue("@amount", orderDetails.Amount);
                    cmd.Parameters.AddWithValue("@total", orderDetails.Total);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                    }
                }
            }

            return result;
        }

        public List<Orders> ReadAll()
        {
            List<Orders> list = new List<Orders>();
            try
            {
                string sql = @"select id, order_no, date, customer_id, total from orders order by date";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Orders order = new Orders();
                            order.Id = Convert.ToInt32(dtr["id"]);
                            order.OrderNo = dtr["order_no"].ToString();
                            order.Date= DateTime.Parse(dtr["date"].ToString());
                            order.CustomerId = Convert.ToInt32(dtr["customer_id"]);
                            order.Total = Convert.ToInt32(dtr["total"]);
                            list.Add(order);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;
        }

        public List<Orders> Search(string param)
        {
            List<Orders> list = new List<Orders>();
            try
            {
                string sql = @"select * from orders where order_no like @param order by order_no";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@param", string.Format("%{0}%", param));
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Orders order = new Orders();
                            order.Id = Convert.ToInt32(dtr["id"]);
                            order.OrderNo = dtr["order_no"].ToString();
                            order.Date = DateTime.Parse(dtr["date"].ToString());
                            order.CustomerId = Convert.ToInt32(dtr["customer_id"]);
                            order.Total = Convert.ToInt32(dtr["total"]);
                            list.Add(order);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Search error: {0}", ex.Message);
            }
            return list;
        }
    }
}
