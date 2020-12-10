using Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SYS_USER: ISYS_USER
    {
        public long ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string SESSIONID { get; set; }
        public bool ISADMIN { get; set; }
        public bool ISUSERTREAM { get; set; }
        public string TREAMNAME { get; set; }
        public bool ISUSERSELECT { get; set; }
        public string SELECTTABLE { get; set; }
        public bool ISUSERSHOULDER { get; set; }
        public string SHOULDERTABLE { get; set; }
    }
}
