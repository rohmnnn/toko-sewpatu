using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokoSepatuApp.Model.Entity
{
    public class ProductSizes
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Size { get; set; }
        public bool IsAvaliable { get; set; }
        public int Stock { get; set; }
    }
}
