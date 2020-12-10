using ModelContext.DatabaseModel.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContext.DatabaseModel
{
    public class SYS_USER: ISYS_USER
    {
        public long ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string SESSIONID { get; set; }
    }
}
