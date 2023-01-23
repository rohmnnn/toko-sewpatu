using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using TokoSepatuApp.Model.Context;
using TokoSepatuApp.Model.Entity;
using System.Security.Policy;
using System.Windows.Forms;

namespace TokoSepatuApp.Model.Repository
{
    public class ProductsRepository
    {
        private SQLiteConnection _conn;

        public ProductsRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public int Create(Products products)
        {
            int result = 0;
            string sql = @"insert into products (name, brand_id, price, photo) values (@name, @brand_id, @price, @photo)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", products.Name);
                cmd.Parameters.AddWithValue("@brand_id", products.BrandId);
                cmd.Parameters.AddWithValue("@price", products.Price);
                cmd.Parameters.AddWithValue("@photo", products.Photo);
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

        public int Update(Products products, string id)
        {
            int result = 0;
            string sql = @"update products set name = @name, brand_id = @brand_id, price = @price, photo = @photo where id == @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", products.Name);
                cmd.Parameters.AddWithValue("@brand_id", products.BrandId);
                cmd.Parameters.AddWithValue("@price", products.Price);
                cmd.Parameters.AddWithValue("@photo", products.Photo);
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

        public int Delete(string id)
        {
            int result = 0;
            string sql = @"delete from products where id == @id";
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

        public int DetailPrice(int id)
        {
            int result = 0;
            string sql = @"select price from product_sizes 
                        INNER JOIN products ON products.id = product_sizes.id
                        where product_sizes.id == @id";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            result = Convert.ToInt32(dtr["price"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Detail price error: {0}", ex.Message);
                }
            }
            return result;
        }

        public List<Products> ReadAll()
        {
            List<Products> list = new List<Products>();
            try
            {
                string sql = @"select products.id, products.name, products.brand_id, products.price, products.photo, brands.name as brand from products left join brands on products.brand_id = brands.id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Products products = new Products();
                            products.Id = Convert.ToInt32(dtr["id"]);
                            products.Name = dtr["name"].ToString();
                            products.Price = Convert.ToInt32(dtr["price"]);
                            products.BrandId = Convert.ToInt32(dtr["brand_id"]);
                            products.Photo = dtr["photo"].ToString();
                            products.Brand = dtr["brand"].ToString();

                            list.Add(products);
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

        public List<Products> ReadAllSizes()
        {
            List<Products> list = new List<Products>();
            try
            {
                string sql = @"select products.name, product_sizes.*
                            from product_sizes
                            inner join products on products.id = product_sizes.product_id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Products products = new Products();
                            products.Id = Convert.ToInt32(dtr["id"]);
                            products.Name = dtr["name"].ToString();
                            products.Size = dtr["size"].ToString();

                            list.Add(products);
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

        public List<Products> Search(string param)
        {
            List<Products> list = new List<Products>();
            try
            {
                string sql = @" select products.id, products.name, products.price, products.photo, brands.name as brand from products left join brands on products.brand_id = brands.id where products.name like @param";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@param", string.Format("%{0}%", param));
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Products products = new Products();

                            products.Id = Convert.ToInt32(dtr["id"]);
                            products.Name = dtr["name"].ToString();
                            products.Price = Convert.ToInt32(dtr["price"]);
                            products.BrandId = Convert.ToInt32(dtr["brand_id"]);
                            products.Photo = dtr["photo"].ToString();
                            products.Brand = dtr["brand"].ToString();

                            list.Add(products);
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
