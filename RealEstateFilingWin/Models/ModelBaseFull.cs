using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models
{
    public class ModelBaseFull:Models.ModelBase
    {
        

        public long UserIDCreator { get; set; }
        public DateTime CreationDate { get; set; }

        public long? UserIDModifier { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
