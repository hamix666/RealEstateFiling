
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Models.GNR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.MNG
{
    public class UserAccsess:ModelBaseFull
    {
        public long AccessID { get; set; }
        public long UserID { get; set; }

        public int MyProperty { get; set; }

        #region Rel
        [ForeignKey("AccessID")]
        public virtual AccsessName UserAccsess_AccsessName { get; set; }
        [ForeignKey("UserID")]
        public virtual User UserAccsess_User { get; set; }
       
        #endregion
    }
}
