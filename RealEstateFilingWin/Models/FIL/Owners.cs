using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Models.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.FIL
{
    public class Owners : ModelBaseFull, ISyncEntity
    {
        [Index("IX_Code", IsUnique = true,Order =1)]
        [MaxLength(10)]
        public string Code { get; set; }
        [Index("IX_MName")]
        [MaxLength(100)]
        public string MName { get; set; }
        [Index("IX_MTel1")]
        [MaxLength(100)]
        public string MTel1 { get; set; }
        public string MTel2 { get; set; }
        public string MTel3 { get; set; }
        public string SName { get; set; }
        public string STel1 { get; set; }
        public string STel2 { get; set; }
        public string STel3 { get; set; }
        public string Address { get; set; }
        public string Plak { get; set; }
        public string Vahed { get; set; }
        public string Det { get; set; }


        public DateTime? DatePishForoshTahvil { get; set; }


        public int? AllTabaghe { get; set; }
        public int? AllVahed { get; set; }
        public int? Senbana { get; set; }
        public decimal? Masahat { get; set; }
        public decimal? Tol { get; set; }
        public int? Eslahi { get; set; }



        public bool Shomali { get; set; }
        public bool Jonobi { get; set; }
        public bool Sharghi { get; set; }
        public bool Gharbi { get; set; }
        public bool PishForosh { get; set; }
        public bool Forosh { get; set; }
        public bool Ejare { get; set; }
        public bool CoolA { get; set; }
        public bool CoolB { get; set; }
        public bool Spilit { get; set; }
        public bool Warmer { get; set; }
        public bool Pakage { get; set; }
        public bool Shofazh { get; set; }
        public bool Hayat { get; set; }
        public bool Pasio { get; set; }
        public bool Estakhr { get; set; }
        public bool Sona { get; set; }
        public bool Jakozi { get; set; }
        public bool Jaroo { get; set; }
        public bool Ejtemat { get; set; }
        public bool PMotor { get; set; }
        public bool RoofGarden { get; set; }
        public bool Tajmi { get; set; }


        public long FiletypeID { get; set; }
        public long RegionID { get; set; }
        public long FileStatusID { get; set; }
        public long SanadStatusID { get; set; }
        public long ViewID { get; set; }

        public bool IsDelete { get; set; } = false;
        public DateTime? IsDeleteDateTime { get; set; }
        public DateTime? ReturnTime { get; set; }

        public DateTime? ViewPhoneDate { get; set; }
        public long? ViewPhoneUserID {get;set;}
        public decimal? MapLat { get; set; }
        public decimal? MapLong { get; set; }

        // ---------- Sync Fields ----------
        [Index("IX_Code", IsUnique = true, Order = 2)]
        public Guid BranchID { get; set; }
        public Guid MasterID { get; set; } = Guid.NewGuid();
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] RowVersion { get; set; }



        #region Rel
        [ForeignKey("FiletypeID")]
        public virtual FileType Owner_FileType { get; set; }
        [ForeignKey("RegionID")]
        public virtual GNR.Region Owner_Region { get; set; }
        [ForeignKey("FileStatusID")]
        public virtual FileStatus Owner_FileStatus { get; set; }
        [ForeignKey("SanadStatusID")]
        public virtual SanadStatus Owner_SanadStatus { get; set; }
        [ForeignKey("ViewID")]
        public virtual Views Owner_View { get; set; }
        public virtual ICollection<OwnerDet> OwnerDetsList { get; set; }
        #endregion

    }
}
