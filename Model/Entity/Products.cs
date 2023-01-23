using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokoSepatuApp.Model.Entity
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string Brand{ get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
        public string File { get; set; }
        public string Size { get; set; }
    }
}
