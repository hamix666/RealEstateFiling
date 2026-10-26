using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.MNG
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int UserID { get; set; }
        public string UserIP { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string Action { get; set; }
        public string Changes { get; set; }
        public DateTime ActionTime { get; set; }
    }
}
