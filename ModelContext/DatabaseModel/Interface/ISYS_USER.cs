using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContext.DatabaseModel.Interface
{
    public interface ISYS_USER
    {
        long ID { get; set; }
        string USERNAME { get; set; }
        string PASSWORD { get; set; }
        string SESSIONID { get; set; }
    }
}
