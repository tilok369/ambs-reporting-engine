using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.ViewModel.Reponse.Filter
{
    public record FilterResponseDTO : BaseGetResponseDTO
    {
        public FilterResponseDTO(long Id) : base(Id)
        {

        }
        public string Name { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string Script { get; set; } = null!;
        public string Parameter { get; set; } = null!;
        public string DependentParameters { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public int? Type { get; set; }
        public string TypeStr { get; set; }
    }
}
