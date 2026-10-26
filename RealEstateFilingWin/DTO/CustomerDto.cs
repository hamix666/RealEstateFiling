using RealEstateFilingWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.DTO
{
    public class CustomerDto:ModelBaseFull
    {
        public string Name { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Tel3 { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string FilterJson { get; set; }
        public int CountFind { get; set; }
        public string ListOwnerID { get; set; }
        public string CreateDate { get; set; }
        public string FileType { get; set; }
        public bool IsDeleted { get; set; }
        public int? CountMember { get; set; }
    }
}
