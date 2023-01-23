using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using TokoSepatuApp.Model.Context;
using TokoSepatuApp.Model.Entity;
using System.Security.Policy;

namespace TokoSepatuApp.Model.Repository
{
    public class UsersRepository
    {
        private SQLiteConnection _conn;

        public UsersRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public bool IsValidUser(string email, string password)
        {
            bool result = false;

            string sql = @"select count(*) as row_count
                           from users
                           where email = @email and password = @password";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                using (SQLiteDataReader dtr = cmd.ExecuteReader())
                {
                    if (dtr.Read())
                    {
                        result = Convert.ToInt32(dtr["row_count"]) > 0;
                    }
                }
            }

            return result;
        }

        public int Create(Users user)
        {
            int result = 0;
            string sql = @"insert into users (name, email, password) values (@name, @email, @password)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
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

        public int Update(Users user, string id)
        {
            int result = 0;
            string sql = @"update users set name = @name, email = @email where id == @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
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
            string sql = @"delete from users where id == @id";
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

        public List<Users> ReadAll()
        {
            List<Users> list = new List<Users>();
            try
            {
                string sql = @"select id, name, email from users order by name";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Users user = new Users();
                            user.Id = dtr["id"].ToString();
                            user.Name = dtr["name"].ToString();
                            user.Email = dtr["email"].ToString();
                            list.Add(user);
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

        public List<Users> Search(string param)
        {
            List<Users> list = new List<Users>();
            try
            {
                string sql = @"select id, name, email from users where name like @param or email like @param order by name";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@param", string.Format("%{0}%", param));
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Users user = new Users();
                            user.Id = dtr["id"].ToString();
                            user.Name = dtr["name"].ToString();
                            user.Email = dtr["email"].ToString();
                            list.Add(user);
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
