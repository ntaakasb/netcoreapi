using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV2.Model
{
    public class JsonResultData
    {
        public bool IsOk { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
        public long totalrows { get; set; }

        public JsonResultData(object Data = null, string Msg = "", long totalrows = 0, bool IsOk = false)
        {
            this.Data = Data != null ? Data : null;
            this.Msg = Msg;
            this.totalrows = totalrows;
            this.IsOk = IsOk;
        }
    }
}
