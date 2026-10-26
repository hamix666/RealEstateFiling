using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.DTO
{
    public class FileSearchFilterDto
    {
        public string Code { get; set; }
        public string MName { get; set; }
        public string MTel { get; set; }

        public bool Forosh { get; set; }
        public bool PishForosh { get; set; }
        public bool Ejare { get; set; }

        public long FileType { get; set; }

        public string Address { get; set; }

        public List<long> Region { get; set; } = new List<long>();

        public string Price1_Start { get; set; }
        public string Price1_End { get; set; }

        public string Price2_Start { get; set; }
        public string Price2_End { get; set; }

        public string Tozihat { get; set; }

        public int IsDelete { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public int? Khab_az { get; set; }
        public int? Khab_ta { get; set; }
        public int? Senbana_az { get; set; }
        public int? Senbana_ta { get; set; }
        public int? Tabaghe_az { get; set; }
        public int? Tabaghe_ta { get; set; }
        public int? Vahed_az { get; set; }
        public int? Vahed_ta { get; set; }


        public int? VahedinTbaghe_az { get; set; }
        public int? VahedinTbaghe_ta { get; set; }
        public int? Tabaghat_az { get; set; }
        public int? Tabaghat_ta { get; set; }
        public string Parking_az { get; set; }
        public string Parking_ta { get; set; }
        public int? AsansorTabaghe { get; set; }

        public bool Asansor { get; set; }
        public bool Parking { get; set; }
        public bool Anbari { get; set; }

        public int? Metrazh_az { get; set; }
        public int? Metrazh_ta { get; set; }  
        public decimal? Masahat_az { get; set; }
        public decimal? Masahat_ta { get; set; }


    }

}
