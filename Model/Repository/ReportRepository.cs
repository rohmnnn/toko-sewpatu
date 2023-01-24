using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.Model.Context;
using TokoSepatuApp.Model.Entity;

namespace TokoSepatuApp.Model.Repository
{
    internal class ReportRepository
    {
        private SQLiteConnection _conn;

        public ReportRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public List<Report> Report()
        {
            List<Report> list = new List<Report>();
            try
            {
                string sql = @"select brands.name, products.name as product_name, products.price, orders.order_no, product_sizes.size, orders.date, order_details.amount, order_details.total FROM brands inner join products on products.brand_id == brands.id inner join product_sizes on product_sizes.product_id == products.id inner join order_details on order_details.product_size_id == product_sizes.id inner join orders on order_details.order_id == orders.id";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {

                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Report report = new Report();
                            report.Date = Convert.ToDateTime(dtr["date"]);
                            report.Name = dtr["name"].ToString();
                            report.ProductName = dtr["product_name"].ToString();
                            report.Size = Convert.ToInt32(dtr["size"]);
                            report.Price = Convert.ToInt32(dtr["price"]);
                            report.OrderNo = dtr["order_no"].ToString();
                            report.Amount = Convert.ToInt32(dtr["amount"]);
                            report.Total = Convert.ToInt32(dtr["total"]);

                            list.Add(report);
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

        public List<Report> ReportByBrandId(string id) {
            List<Report> list = new List<Report>();
            try
            {
                string sql = @"select brands.name, products.name as product_name, products.price, orders.order_no, product_sizes.size, orders.date, order_details.amount, order_details.total FROM brands inner join products on products.brand_id == brands.id inner join product_sizes on product_sizes.product_id == products.id inner join order_details on order_details.product_size_id == product_sizes.id inner join orders on order_details.order_id == orders.id where brands.id == @id";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Report report = new Report();
                            report.Date = Convert.ToDateTime(dtr["date"]);
                            report.Name = dtr["name"].ToString();
                            report.ProductName = dtr["product_name"].ToString();
                            report.Size = Convert.ToInt32(dtr["size"]);
                            report.Price = Convert.ToInt32(dtr["price"]);
                            report.OrderNo = dtr["order_no"].ToString();
                            report.Amount = Convert.ToInt32(dtr["amount"]);
                            report.Total = Convert.ToInt32(dtr["total"]);

                            list.Add(report);
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

        public List<Report> ReportByDateOrder(string from, string to) {
            List<Report> list = new List<Report>();
            try
            {

                string sql = @"select brands.name, products.name as product_name, products.price, orders.order_no, product_sizes.size, orders.date, order_details.amount, order_details.total FROM brands inner join products on products.brand_id == brands.id inner join product_sizes on product_sizes.product_id == products.id inner join order_details on order_details.product_size_id == product_sizes.id inner join orders on order_details.order_id == orders.id where orders.date between @from and @to";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                    {
                        cmd.Parameters.AddWithValue("@from", from);
                        cmd.Parameters.AddWithValue("@to", to);
                    }

                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Report report = new Report();
                            report.Date = Convert.ToDateTime(dtr["date"]);
                            report.Name = dtr["name"].ToString();
                            report.ProductName = dtr["product_name"].ToString();
                            report.Size = Convert.ToInt32(dtr["size"]);
                            report.Price = Convert.ToInt32(dtr["price"]);
                            report.OrderNo = dtr["order_no"].ToString();
                            report.Amount = Convert.ToInt32(dtr["amount"]);
                            report.Total = Convert.ToInt32(dtr["total"]);

                            list.Add(report);
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
    }
}
