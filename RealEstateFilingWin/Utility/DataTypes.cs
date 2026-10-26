using System.Collections.Generic;
using System.ComponentModel;

namespace RealEstateFilingWin.Utility
{
    public   class DataType
    {
        public struct MethodResult
        {
            [DefaultValue(true)]
            public bool ShowDialog;
            public bool Result;
            public string MessageBody;
            public string MessageTitle;
            public string KeyCode;
        }

        public struct ChartResult
        {
            public string BaseColName;
            public List<string> ColName;

        }
    }


}
