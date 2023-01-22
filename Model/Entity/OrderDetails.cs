using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokoSepatuApp.Model.Entity
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductIdSize { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }

    }
}
