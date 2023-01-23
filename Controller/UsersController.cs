using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.Model.Context;
using TokoSepatuApp.Model.Entity;
using TokoSepatuApp.Model.Repository;

namespace TokoSepatuApp.Controller
{
    internal class UsersController
    {
        private UsersRepository _repository;

        public bool IsValidUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            bool isValidUser = false;

            using (DbContext context = new DbContext())
            {
                _repository = new UsersRepository(context);

                isValidUser = _repository.IsValidUser(email, password);
            }

            if (!isValidUser)
            {
                MessageBox.Show("Data user tidak ditemukan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }

            return true;
        }
        public int Create(Users users)
        {
            int result = 0;
            if (string.IsNullOrEmpty(users.Name))
            {
                MessageBox.Show("Nama wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(users.Email))
            {
                MessageBox.Show("Email wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(users.Password))
            {
                MessageBox.Show("Password wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new UsersRepository(context);
                result = _repository.Create(users);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil ditambahkan!", "Informasi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
                MessageBox.Show("Data gagal ditambahkan", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }

        public int Update(Users users, string id)
        {
            int result = 0;
            if (string.IsNullOrEmpty(users.Name))
            {
                MessageBox.Show("Nama wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(users.Email))
            {
                MessageBox.Show("Email wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new UsersRepository(context);
                result = _repository.Update(users, id);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil diubah!", "Informasi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
                MessageBox.Show("Data gagal diubah", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }

        public int Delete(string id)
        {
            int result = 0;
            using (DbContext context = new DbContext())
            {
                _repository = new UsersRepository(context);
                result = _repository.Delete(id);
            }
            return result;
        }



        public List<Users> ReadAll()
        {
            List<Users> list = new List<Users>();
            using (DbContext context = new DbContext())
            {
                _repository = new UsersRepository(context);
                list = _repository.ReadAll();
            }
            return list;
        }

        public List<Users> Search(string param)
        {
            List<Users> list = new List<Users>();
            using (DbContext context = new DbContext())
            {
                _repository = new UsersRepository(context);
                list = _repository.Search(param);
            }
            return list;
        }
    }
}
