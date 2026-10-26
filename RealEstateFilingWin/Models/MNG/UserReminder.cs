using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.MNG
{
    public class UserReminder
    {
        [Key]
        public Guid ID { get; set; }
        public long UserID { get; set; }
        public DateTime DateReminder { get; set; }
        public string ShamsiDateReminder { get; set; }
        public string TextReminder { get; set; }
    }
}
