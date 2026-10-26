using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Models.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.FIL
{
    public class OwnerDet : ModelBaseFull,ISyncEntity
    {
        public long OwnerID { get; set; }
        public int? Tabaghe { get; set; } 
        public int? Zirbana { get; set; } 
        public int? Khab { get; set; } 
        public int? Parking { get; set; } 

        public long? KabinetID { get; set; }
        public long? ServiceWcID { get; set; } 
        public long? KafposhID { get; set; } 

        public bool Anbari { get; set; }
        public bool Balkon { get; set; }
        public bool Asansor { get; set; }

        public decimal Price1 { get; set; } = 0;
        public decimal Price2 { get; set; } = 0;


        // ---------- Sync Fields ----------
        public Guid BranchID { get; set; }
        public Guid MasterID { get; set; } = Guid.NewGuid();
        public Guid OwnerMasterID { get; set; }
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] RowVersion { get; set; }



        #region Rel
        [ForeignKey("OwnerID")]
        public virtual Owners OwnerDet_Owners { get; set; }
        [ForeignKey("KabinetID")]
        public virtual Kabinet OwnerDet_Kabinet { get; set; }
        [ForeignKey("ServiceWcID")]
        public virtual ServiceWc OwnerDet_ServiceWc { get; set; }
        [ForeignKey("KafposhID")]
        public virtual Kafposh OwnerDet_Kafposh { get; set; }
        #endregion

    }
   
}
