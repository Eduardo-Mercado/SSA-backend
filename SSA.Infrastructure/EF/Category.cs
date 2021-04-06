using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class Category
    {
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool RecordStatus { get; set; }
        public string CategoryType { get; set; }
        public string Color { get; set; }
    }
}
