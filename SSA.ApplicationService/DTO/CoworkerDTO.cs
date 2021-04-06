using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.ApplicationService.DTO
{
    public class CoworkerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string Position { get; set; }
        public bool RecordStatus { get; set; }
    }
}
