using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.CUS
{
    public class Customer:ModelBaseFull
    {
        public string Name { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Tel3 { get; set; }
        public string Title { get; set; }
        public string Addres { get; set; }
        public string FilterJson { get; set; }
        public int? CountMember { get; set; }
        public bool IsDeleted { get; set; }
    }
}
