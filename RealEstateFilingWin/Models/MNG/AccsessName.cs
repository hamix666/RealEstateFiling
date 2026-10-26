using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.MNG
{
    public class AccsessName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public long ID { get; set; }
        public string Name { get; set; }
        public string Name_E { get; set; }
        public  long? value { get; set; }
    }
}
