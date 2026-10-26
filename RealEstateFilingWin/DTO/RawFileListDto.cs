using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.DTO
{
    public class RawFileListDto
    {
        public long ID { get; set; }
        public string ContractType { get; set; }
        public string FileType { get; set; }
        public DateTime? CreationDate { get; set; }   // <-- Nullable برای ایمنی
        public DateTime? ModificationDate { get; set; }   // <-- Nullable برای ایمنی
        public string Region { get; set; }
        public string MName { get; set; }
        public int? Tabaghe { get; set; }
        public int? Zirbana { get; set; }
        public string Asansor { get; set; }
        public int? Parking { get; set; }
        public string Anbari { get; set; }
        public int? Senbana { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public string Address { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ReturnTime { get; set; }
        public bool isDeleted { get; set; }
        public decimal? Masahat { get; set; }
        public decimal? Tol { get; set; }
    }
}
