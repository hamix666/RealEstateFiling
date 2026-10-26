using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.DTO
{
    public class UserActivityDTO
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string NameCustomer { get; set; }
        public long UserIDCreator { get; set; }
        public string CreationDate { get; set; }

        public long? UserIDModifier { get; set; }
        public string ModificationDate { get; set; }
    }
}
