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
    internal class ProductSizesController
    {
        private ProductSizesRepository _repository;

        public int Create(ProductSizes productSizes)
        {
            int result = 0;
            if (string.IsNullOrEmpty(productSizes.ProductId.ToString()))
            {
                MessageBox.Show("Produk wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(productSizes.Size.ToString()))
            {
                MessageBox.Show("Size wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(productSizes.IsAvaliable.ToString()))
            {
                MessageBox.Show("Available produk wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(productSizes.Stock.ToString()))
            {
                MessageBox.Show("Stock wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new ProductSizesRepository(context);
                result = _repository.Create(productSizes);
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

        public int Update(ProductSizes productSizes, int id)
        {
            int result = 0;
            if (string.IsNullOrEmpty(productSizes.ProductId.ToString()))
            {
                MessageBox.Show("Produk wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


            using (DbContext context = new DbContext())
            {
                _repository = new ProductSizesRepository(context);
                result = _repository.Update(productSizes, id);
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

        public int Delete(int id)
        {
            int result = 0;
            using (DbContext context = new DbContext())
            {
                _repository = new ProductSizesRepository(context);
                result = _repository.Delete(id);
            }
            return result;
        }


        public List<ProductSizes> ReadAll()
        {
            List<ProductSizes> list = new List<ProductSizes>();
            using (DbContext context = new DbContext())
            {
                _repository = new ProductSizesRepository(context);
                list = _repository.ReadAll();
            }
            return list;
        }

        public List<ProductSizes> Search(string param)
        {
            List<ProductSizes> list = new List<ProductSizes>();
            using (DbContext context = new DbContext())
            {
                _repository = new ProductSizesRepository(context);
                list = _repository.Search(param);
            }
            return list;
        }
    }
}
