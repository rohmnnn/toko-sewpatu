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
    internal class CustomersRepository
    {
        private SQLiteConnection _conn;

        public CustomersRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public List<Customers> ReadAll()
        {
            List<Customers> list = new List<Customers>();
            try
            {
                string sql = @"select id, name, email, phone, address from customers order by name";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Customers brand = new Customers();
                            brand.Id = Convert.ToInt32(dtr["id"]);
                            brand.Name = dtr["name"].ToString();
                            brand.Address = dtr["address"].ToString();
                            list.Add(brand);
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

        public List<Customers> Search(string param)
        {
            List<Customers> list = new List<Customers>();
            try
            {
                string sql = @"select id, name, email, phone, address from customers where name like @param order by name";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@param", string.Format("%{0}%", param));
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Customers brand = new Customers();
                            brand.Id = Convert.ToInt32(dtr["id"]);
                            brand.Name = dtr["name"].ToString();
                            brand.Address = dtr["address"].ToString();
                            list.Add(brand);
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
