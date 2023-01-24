using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace TokoSepatuApp.Model.Entity
{
    internal class Report
    {
        public string Name { get; set; }
        public string ProductName { get; set; }
        public int Size { get; set; }
        public DateTime Date { get; set; }
        public string OrderNo { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }
    }
}
