using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.GNR
{
    public class Settings
    {
        public Guid ID { get; set; }= Guid.NewGuid();
        public Guid? BranchID { get; set; }
        public bool Active { get; set; }
        public string ftpServer { get; set; } = "";
        public int ftpPort { get; set; } = 21;
        public string ftpUser { get; set; } = "";
        public string ftpPass { get; set; } = "";
        public string ftpRootPath { get; set; } = "";
        public string masterConnectionString { get; set; } = "";
        public int IntervalFileUpdate { get; set; } = 10;
        public int IntervalOwnerUpdate { get; set; }=10;
        public int IntervalInfoUpdate { get; set; }=5;
    }
}
