using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Model
{
    public class OrderItem
    {
        public int ID { get; set; }
        public virtual Product Product_ { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Order Order_ { get; set; }
    }
}
