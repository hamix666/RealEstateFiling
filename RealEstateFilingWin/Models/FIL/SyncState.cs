using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.FIL
{
    public class SyncState
    {
        public int Id { get; set; }
        public DateTime? LastPush { get; set; }
        public DateTime? LastPull { get; set; }
        public DateTime? LastPushImage { get; set; }
        public DateTime? LastPullImage { get; set; }
        public DateTime? LastDeleteOwner { get; set; } = DateTime.UtcNow;
    }
}
