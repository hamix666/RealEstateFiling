using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.MNG
{
    public class Branch
    {
        [Key]
        public Guid ID { get; set; }
        public int? Code { get; set; }

        public string Name { get; set; }
        public string localRoot { get; set; }

        public bool IsActive { get; set; }
    }
}
