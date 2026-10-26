using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFiling.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double Area { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }

}
