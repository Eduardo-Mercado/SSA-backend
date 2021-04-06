using Newtonsoft.Json;
using SSA.ApplicationService.Helpers;
using System;

namespace SSA.ApplicationService.DTO.Task
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int IdCategory { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double TimeInvested { get; set; }
        public float ProgressPercent { get; set; }
        public int IdActivity { get; set; }
        public string Status { get; set; }

        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool IsAutorize { get; set; }
        public string ComentAutorize { get; set; }
    }
}
