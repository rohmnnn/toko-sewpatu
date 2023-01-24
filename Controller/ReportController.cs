using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TokoSepatuApp.Model.Context;
using TokoSepatuApp.Model.Entity;
using TokoSepatuApp.Model.Repository;

namespace TokoSepatuApp.Controller
{
    internal class ReportController
    {
        private ReportRepository _repository;

        public List<Report> Report()
        {
            List<Report> list = new List<Report>();
            using (DbContext context = new DbContext())
            {
                _repository = new ReportRepository(context);
                list = _repository.Report();
            }
            return list;
        }

        public List<Report> ReportByBrandId(string id)
        {
            List<Report> list = new List<Report>();
            using (DbContext context = new DbContext())
            {
                _repository = new ReportRepository(context);
                list = _repository.ReportByBrandId(id);
            }
            return list;
        }

        public List<Report> ReportByOrderDate(string from, string to)
        {
            List<Report> list = new List<Report>();
            using (DbContext context = new DbContext())
            {
                _repository = new ReportRepository(context);
                list = _repository.ReportByDateOrder(from, to);
            }
            return list;
        }

    }
}
