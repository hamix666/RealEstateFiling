using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.FIL
{
    public class OwnersDeleted
    {
        public Guid ID { get; set; }= Guid.NewGuid();
        public Guid OwnerGuiD { get; set; }
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
    }
}
