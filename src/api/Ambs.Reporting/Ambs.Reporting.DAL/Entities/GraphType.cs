using System;
using System.Collections.Generic;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class GraphType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? Status { get; set; }
        public int SortOrder { get; set; }
    }
}
