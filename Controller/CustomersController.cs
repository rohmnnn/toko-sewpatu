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
    internal class CustomersController
    {
        private CustomersRepository _repository;

        public List<Customers> ReadAll()
        {
            List<Customers> list = new List<Customers>();
            using (DbContext context = new DbContext())
            {
                _repository = new CustomersRepository(context);
                list = _repository.ReadAll();
            }
            return list;
        }

        public List<Customers> Search(string param)
        {
            List<Customers> list = new List<Customers>();
            using (DbContext context = new DbContext())
            {
                _repository = new CustomersRepository(context);
                list = _repository.Search(param);
            }
            return list;
        }
    }
}
