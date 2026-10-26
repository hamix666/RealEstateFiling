using RealEstateFilingWin.Models;
using RealEstateFilingWin.Models.FIL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Models.MNG
{
    public class User:ModelBaseFull
    {
        public string Name { get; set; }
        [MaxLength(10)]
        public string CodeMeli { get; set; }
        [MaxLength(11)]
        public string Mobile { get; set; }
        public string PhonNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }


        public virtual ICollection<UserAccsess> UserAccsessList { get; set; }
    }

}
