using RealEstateFilingWin.Models.FIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.Sync
{
    public class SyncPayload
    {
        public List<Owners> Owners { get; set; }
        public List<OwnerDet> OwnerDets { get; set; }

        public DateTime SyncTime { get; set; }
    }
}
