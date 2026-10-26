using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ClsBaseSetting
    {
        static public string ServerName { get; set; }
        static public string ServerIP { get; set; }
        static public long UserID { get; set; }
        static public string timein { get; set; }
        static public Guid CurrentBranchID { get; set; }
        static public string CurrentBranchName { get; set; }
        static public RealEstateFilingWin.Models.MNG.User UserIn { get; set; }
        static public List<RealEstateFilingWin.Models.MNG.UserReminder> UserReminderList { get; set; }
    }
}
