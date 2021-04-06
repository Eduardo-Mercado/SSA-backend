using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.ApplicationService.DTO.Activity
{
   public class ActivityListDTO
    {
        public int IdActivity { get; set; }
        public string Title { get; set; }
        public string AssignedTo { get; set; }
        public string Priority { get; set; }
    }
}
