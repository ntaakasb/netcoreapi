using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interface
{
    public interface ISYS_USER
    {
        long ID { get; set; }
        string USERNAME { get; set; }
        string PASSWORD { get; set; }
        string SESSIONID { get; set; }
        bool ISADMIN { get; set; }
        bool ISUSERTREAM { get; set; }
        string TREAMNAME { get; set; }
        bool ISUSERSELECT { get; set; }
        string SELECTTABLE { get; set; }
        bool ISUSERSHOULDER { get; set; }
        string SHOULDERTABLE { get; set; }
    }
}
