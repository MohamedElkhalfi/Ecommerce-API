using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Api.Model
{
    public class ProductApi
    {
        public ProductApi()
        {

        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal currentPrice { get; set; }
        public bool? promotion { get; set; }
        public bool? selected { get; set; }
        public bool? available { get; set; }
        public string photoName { get; set; }
        public decimal quantity { get; set; } 

}
}
