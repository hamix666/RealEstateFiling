using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models
{
    public class ModelBase
    {
        [Key]
        public long ID { get; set; }
        public string Description { get; set; }
        public string Description_Latin { get; set; }
    }
}
