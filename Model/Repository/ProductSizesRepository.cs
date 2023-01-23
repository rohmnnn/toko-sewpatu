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
    internal class ProductSizesRepository
    {
        private SQLiteConnection _conn;

        public ProductSizesRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public int Create(ProductSizes productSize)
        {
            int result = 0;
            string sql = @"insert into product_sizes (product_id, size, is_available, stock) values (@product_id, @size, @is_available, @stock)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@product_id", productSize.ProductId);
                cmd.Parameters.AddWithValue("@size", productSize.Size);
                cmd.Parameters.AddWithValue("@is_available", productSize.IsAvaliable);
                cmd.Parameters.AddWithValue("@stock", productSize.Stock);
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

        public int Update(ProductSizes productSize, int id)
        {
            int result = 0;
            string sql = @"update product_sizes set product_id = @product_id, size = @size, is_available = @is_available, stock = @stock where id == @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@product_id", productSize.ProductId);
                cmd.Parameters.AddWithValue("@size", productSize.Size);
                cmd.Parameters.AddWithValue("@is_available", productSize.IsAvaliable);
                cmd.Parameters.AddWithValue("@stock", productSize.Stock);
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
            string sql = @"delete from product_sizes where id == @id";
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

        public List<ProductSizes> ReadAll()
        {
            List<ProductSizes> list = new List<ProductSizes>();
            try
            {
                string sql = @"select products.name as product, product_sizes.* 
                            from product_sizes 
                            inner join products on products.id = product_sizes.product_id
                            order by product_id, size
                            ";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            ProductSizes productSize = new ProductSizes();
                            productSize.Id = Convert.ToInt32(dtr["id"]);
                            productSize.ProductId = Convert.ToInt32(dtr["product_id"]);
                            productSize.Product = dtr["product"].ToString();
                            productSize.Size = Convert.ToInt32(dtr["size"]);
                            productSize.IsAvaliable = Convert.ToInt32(dtr["is_available"]);
                            productSize.Stock = Convert.ToInt32(dtr["stock"]);
                            list.Add(productSize);
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

        public List<ProductSizes> Search(string param)
        {
            List<ProductSizes> list = new List<ProductSizes>();
            try
            {
                string sql = @"select product_sizes.*, products.name as product from product_sizes 
                            inner join products on products.id = product_sizes.product_id
                            where products.name like @param 
                            order by name, size
                            ";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@param", string.Format("%{0}%", param));
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            ProductSizes productSize = new ProductSizes();
                            productSize.Id = Convert.ToInt32(dtr["id"]);
                            productSize.ProductId = Convert.ToInt32(dtr["product_id"]);
                            productSize.Product = dtr["product"].ToString();
                            productSize.Size = Convert.ToInt32(dtr["size"]);
                            productSize.IsAvaliable = Convert.ToInt32(dtr["is_available"]);
                            productSize.Stock = Convert.ToInt32(dtr["stock"]);
                            list.Add(productSize);
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
