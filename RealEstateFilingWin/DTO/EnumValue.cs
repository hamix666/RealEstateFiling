using System;
using System.Collections.Generic;

namespace RealEstateFilingWin.DTO
{
    public class EnumValue
    {
        public EnumValue()
        {
            Senbana = new List<CmbValue>()
            {
                new CmbValue { ID = 0, Name = "نوساز" },
                new CmbValue { ID = 1, Name = "1" },
                new CmbValue { ID = 2, Name = "2" },
                new CmbValue { ID = 3, Name = "3" },
                new CmbValue { ID = 4, Name = "4" },
                new CmbValue { ID = 5, Name = "5" },
                new CmbValue { ID = 6, Name = "6" },
                new CmbValue { ID = 7, Name = "7" },
                new CmbValue { ID = 8, Name = "8" },
                new CmbValue { ID = 9, Name = "9" },
                new CmbValue { ID = 10, Name = "10" },
                new CmbValue { ID = 11, Name = "11" },
                new CmbValue { ID = 12, Name = "12" },
                new CmbValue { ID = 13, Name = "13" },
                new CmbValue { ID = 14, Name = "14" },
                new CmbValue { ID = 15, Name = "15" },
                new CmbValue { ID = 16, Name = "16" },
                new CmbValue { ID = 17, Name = "17" },
                new CmbValue { ID = 18, Name = "18" },
                new CmbValue { ID = 19, Name = "19" },
                new CmbValue { ID = 20, Name = "20" },
                new CmbValue { ID = 21, Name = "21" },
                new CmbValue { ID = 22, Name = "22" },
                new CmbValue { ID = 23, Name = "23" },
                new CmbValue { ID = 24, Name = "24" },
                new CmbValue { ID = 25, Name = "25" }
            };

            Tabaghe = new List<CmbValue>()
            {
                new CmbValue { ID = -1, Name = "زیر همکف" },
                new CmbValue { ID = 0, Name = "همکف" },
                new CmbValue { ID = 1, Name = "1" },
                new CmbValue { ID = 2, Name = "2" },
                new CmbValue { ID = 3, Name = "3" },
                new CmbValue { ID = 4, Name = "4" },
                new CmbValue { ID = 5, Name = "5" },
                new CmbValue { ID = 6, Name = "6" },
                new CmbValue { ID = 7, Name = "7" },
                new CmbValue { ID = 8, Name = "8" },
                new CmbValue { ID = 9, Name = "9" },
                new CmbValue { ID = 10, Name = "10" }
            };
            Khab = new List<CmbValue>()
            {
                new CmbValue { ID = 0, Name = "ندارد" },
                new CmbValue { ID = 1, Name = "1" },
                new CmbValue { ID = 2, Name = "2" },
                new CmbValue { ID = 3, Name = "3" },
                new CmbValue { ID = 4, Name = "4" },
                new CmbValue { ID = 5, Name = "5" },
                
            };
            Parking = new List<CmbValue>()
            {
                new CmbValue { ID = 0, Name = "0" },
                new CmbValue { ID = 1, Name = "1" },
                new CmbValue { ID = 2, Name = "2" },
                new CmbValue { ID = 3, Name = "3" },
                new CmbValue { ID = 4, Name = "4" },
                new CmbValue { ID = 5, Name = "5" },
                new CmbValue { ID = 6, Name = "6" },
                new CmbValue { ID = 7, Name = "7" },
                new CmbValue { ID = 8, Name = "8" },
                new CmbValue { ID = 9, Name = "9" },
                new CmbValue { ID = 10, Name = "10" }
            };
        }

        public List<CmbValue> Senbana { get; set; }
        public List<CmbValue> Tabaghe { get; set; }
        public List<CmbValue> Khab { get; set; }
        public List<CmbValue> Parking { get; set; }

        public class CmbValue
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
    }
}
