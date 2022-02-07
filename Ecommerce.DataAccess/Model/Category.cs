using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Model
{
    public class Category
    {
        public Category()
        {
            Product_ = new HashSet<Product>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Product_ { get; set; }


    }
}
