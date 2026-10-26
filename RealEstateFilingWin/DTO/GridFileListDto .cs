using RealEstateFilingWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.DTO
{
    public class GridFileListDto:ModelBaseFull
    {
        public long ID { get; set; }
        public string ContractType { get; set; }
        public string FileType { get; set; }
        public string CreationDate { get; set; }
        public string Region { get; set; } 
        public string MName { get; set; } 
        public string Tabaghe { get; set; } 
        public int? Zirbana { get; set; } 
        public string Asansor { get; set; } 
        public int? Parking { get; set; } 
        public string Anbari { get; set; } 
        public string Senbana { get; set; } 
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public string Address { get; set; }
        public string LastDate { get; set; }
        public bool isDeleted { get; set; }
        public int? Masahat { get; set; }
        public int? Tol { get; set; }
        public int Newrecord { get; set; }
        public long Customerid { get; set; }
        public string ReturnTime { get; set; }
    }
}
