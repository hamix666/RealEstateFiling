using RealEstateFilingWin.Models.FIL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.MNG
{
    public class UserOwner:ModelBase
    {
        public long UserID { get; set; }
        public long OwnerID { get; set; }

        #region Rel
        
        [ForeignKey("OwnerID")]
        public virtual Owners UserOwner_Owners { get; set; }

        #endregion
    }
}
