using System;
using System.Collections.Generic;

namespace SSA.Security.EF
{
    public partial class Account
    {
        public int IdUser { get; set; }
        public DateTime RecordDate { get; set; }
        public int IdRol { get; set; }
        public string UserName { get; set; }
        public string Passwd { get; set; }
        public bool RecordStatus { get; set; }
        public int IdCoworker { get; set; }
    }
}
