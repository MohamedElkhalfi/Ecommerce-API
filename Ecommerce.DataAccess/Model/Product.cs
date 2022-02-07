﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Model
{
    public class Product
    {
        public Product()
        {

        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool? Is_Promotion { get; set; }
        public bool? Is_Selected { get; set; }
        public bool? Is_Available { get; set; }
        public string PhotoName { get; set; }
        public decimal Quantity  { get; set; }
        public virtual Category Category_ { get; set; }

}
}
