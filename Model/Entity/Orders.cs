using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokoSepatuApp.Model.Entity
{
    public class Orders
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int Total { get; set; }
    }
}
