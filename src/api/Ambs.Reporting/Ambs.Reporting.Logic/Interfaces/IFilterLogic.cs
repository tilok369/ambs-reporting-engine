using Ambs.Reporting.ViewModel.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Logic.Interfaces
{
    public interface IFilterLogic
    {
        FilterResponseDTO Get(long id);
        IList<FilterResponseDTO> GetAll(int page, int size);
        FilterPostResponseDTO Save(FilterPostRequestDTO filter);
        IEnumerable<DropdownFilter> GetGraphType();
    }
}
