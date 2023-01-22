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
    internal class BrandsRepository
    {
        private SQLiteConnection _conn;

        public BrandsRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public int Create(Brands brand)
        {
            int result = 0;
            string sql = @"insert into brands (name) values (@name)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", brand.Name);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }

        public int Update(Brands brand, int id)
        {
            int result = 0;
            string sql = @"update brands set name = @name where id == @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", brand.Name);
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            string sql = @"delete from brands where id == @id";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }
            return result;
        }

        public List<Brands> ReadAll()
        {
            List<Brands> list = new List<Brands>();
            try
            {
                string sql = @"select id, name from brands order by name";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Brands brand = new Brands();
                            brand.Id = Convert.ToInt32(dtr["id"]);
                            brand.Name = dtr["name"].ToString();
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

        public List<Brands> Search(string param)
        {
            List<Brands> list = new List<Brands>();
            try
            {
                string sql = @"select id, name from brands where name like @param order by name";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@param", string.Format("%{0}%", param));
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Brands brand = new Brands();
                            brand.Id = Convert.ToInt32(dtr["id"]);
                            brand.Name = dtr["name"].ToString();
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
