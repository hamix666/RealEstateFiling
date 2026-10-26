using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.FIL
{
    public class FileInfo:ModelBaseFull
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string MimeType { get; set; }
        public string Size { get; set; }
        public Guid MasterID { get; set; }       // شناسه یکتا عکس
        public Guid OwnerID { get; set; }        // شناسه ملک
        public string FileHash { get; set; }     // SHA256
        public DateTime LastModified { get; set; }
        public bool IsDelete { get; set; }

    }
}
