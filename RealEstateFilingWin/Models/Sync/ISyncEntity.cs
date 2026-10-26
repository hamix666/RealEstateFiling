using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.Sync
{
    public interface ISyncEntity
    {
        //Guid BranchID { get; set; }
        DateTime LastModified { get; set; }

        byte[] RowVersion { get; set; }
    }
}
