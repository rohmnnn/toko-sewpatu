using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.Model.Context;
using TokoSepatuApp.Model.Entity;
using TokoSepatuApp.Model.Repository;

namespace TokoSepatuApp.Controller
{
    internal class ProductsController
    {
        private ProductsRepository _repository;

        public int Create(Products products)
        {
            int result = 0;
            if (string.IsNullOrEmpty(products.Name))
            {
                MessageBox.Show("Nama wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (products.Price < 1)
            {
                MessageBox.Show("Harga wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (products.BrandId < 1)
            {
                MessageBox.Show("Brand wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (!string.IsNullOrEmpty(products.File))
            {
                string saveDirectory = Directory.GetCurrentDirectory() +  @"\Images\";

                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string fileName = Path.GetFileName(products.File);
                string fileSavePath = Path.Combine(saveDirectory, fileName);

                File.Copy(products.File, fileSavePath, true);

                products.Photo = fileSavePath;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new ProductsRepository(context);
                result = _repository.Create(products);
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

        public int Update(Products products, string id)
        {
            int result = 0;
            if (string.IsNullOrEmpty(products.Name))
            {
                MessageBox.Show("Nama wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (products.Price < 1)
            {
                MessageBox.Show("Harga wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (products.BrandId < 1)
            {
                MessageBox.Show("Brand wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (!string.IsNullOrEmpty(products.File))
            {
                string saveDirectory = Directory.GetCurrentDirectory() + @"\Images\";

                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string fileName = Path.GetFileName(products.File);
                string fileSavePath = Path.Combine(saveDirectory, fileName);

                File.Copy(products.File, fileSavePath, true);

                products.Photo = fileSavePath;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new ProductsRepository(context);
                result = _repository.Update(products, id);
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
                _repository = new ProductsRepository(context);
                result = _repository.Delete(id);
            }
            return result;
        }



        public List<Products> ReadAll()
        {
            List<Products> list = new List<Products>();
            using (DbContext context = new DbContext())
            {
                _repository = new ProductsRepository(context);
                list = _repository.ReadAll();
            }
            return list;
        }

        public List<Products> Search(string param)
        {
            List<Products> list = new List<Products>();
            using (DbContext context = new DbContext())
            {
                _repository = new ProductsRepository(context);
                list = _repository.Search(param);
            }
            return list;
        }
    }
}
