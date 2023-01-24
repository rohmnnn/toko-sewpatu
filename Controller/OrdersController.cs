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
    internal class OrdersController
    {
        private OrdersRepository _repository;

        public int Create(Orders orders, OrderDetails orderDetails, Customers customers)
        {
            int result = 0;
            if (string.IsNullOrEmpty(orders.CustomerId.ToString()))
            {
                MessageBox.Show("Nama Customer wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(orderDetails.ProductIdSize.ToString()))
            {
                MessageBox.Show("Product wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(orderDetails.Amount.ToString()))
            {
                MessageBox.Show("Jumlah Item Product wajib diisi!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new OrdersRepository(context);
                result = _repository.Create(orders, orderDetails, customers);
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

        public List<Orders> ReadAll()
        {
            List<Orders> list = new List<Orders>();
            using (DbContext context = new DbContext())
            {
                _repository = new OrdersRepository(context);
                list = _repository.ReadAll();
            }
            return list;
        }

        public List<Orders> Search(string param)
        {
            List<Orders> list = new List<Orders>();
            using (DbContext context = new DbContext())
            {
                _repository = new OrdersRepository(context);
                list = _repository.Search(param);
            }
            return list;
        }
    }
}
