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
    internal class BrandsController
    {
        private BrandsRepository _repository;

        public int Create(Brands brands)
        {
            int result = 0;
            if (string.IsNullOrEmpty(brands.Name))
            {
                MessageBox.Show("Nama wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new BrandsRepository(context);
                result = _repository.Create(brands);
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

        public int Update(Brands brands, int id)
        {
            int result = 0;
            if (string.IsNullOrEmpty(brands.Name))
            {
                MessageBox.Show("Nama wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


            using (DbContext context = new DbContext())
            {
                _repository = new BrandsRepository(context);
                result = _repository.Update(brands, id);
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
                _repository = new BrandsRepository(context);
                result = _repository.Delete(id);
            }
            return result;
        }


        public List<Brands> ReadAll()
        {
            List<Brands> list = new List<Brands>();
            using (DbContext context = new DbContext())
            {
                _repository = new BrandsRepository(context);
                list = _repository.ReadAll();
            }
            return list;
        }

        public List<Brands> Search(string param)
        {
            List<Brands> list = new List<Brands>();
            using (DbContext context = new DbContext())
            {
                _repository = new BrandsRepository(context);
                list = _repository.Search(param);
            }
            return list;
        }
    }
}
