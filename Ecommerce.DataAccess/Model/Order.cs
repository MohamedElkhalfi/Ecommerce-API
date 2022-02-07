using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Model
{
    public class Order

    {
        public Order()
        {

        }

        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Client client { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderItem> OrderItem_ { get; set; }
 

    }
}
