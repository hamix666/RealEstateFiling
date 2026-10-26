using RealEstateFilingWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.DTO
{
    public class FileDetDto :ModelBaseFull
    {

        public long OwnerID { get; set; }
        public Guid OwnerMasterID { get; set; }
        public Guid MasterID { get; set; }
        public int? Tabaghe { get; set; }
        public int? Zirbana { get; set; }
        public int? Khab { get; set; }
        public int? Parking { get; set; }

        public long? KabinetID { get; set; }
        public string Kabinet { get; set; }
        public long? ServiceWcID { get; set; }
        public string ServiceWc { get; set; }
        public long? KafposhID { get; set; }
        public string Kafposh { get; set; }

        public bool Anbari { get; set; }
        public bool Balkon { get; set; }
        public bool Asansor { get; set; }

        public decimal Price1 { get; set; } = 0;
        public decimal Price2 { get; set; } = 0;
    }
}
