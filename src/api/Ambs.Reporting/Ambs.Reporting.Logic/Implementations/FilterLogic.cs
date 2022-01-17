
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Logic.Implementations
{
    public class  FilterLogic : IFilterLogic
    {
        private readonly IFilterService _filterService;

        public FilterLogic(IFilterService filterService)
        {
            _filterService = filterService;
        }

        public FilterResponseDTO Get(long id)
        {
            var filter = _filterService.Get(id);
            if (filter == null) return null;

            return new FilterResponseDTO(filter.Id)
            {
                Name = filter.Name,
                Label = filter.Label,
                Script = filter.Script,
                Parameter = filter.Parameter,
                DependentParameters = filter.DependentParameters,
                Status = filter.Status,
                CreatedOn = filter.CreatedOn,
                CreatedBy = filter.CreatedBy,
                UpdatedOn = filter.UpdatedOn,
                UpdatedBy = filter.UpdatedBy
            };
        }

        public IList<FilterResponseDTO> GetAll(int page, int size)
        {
            var filterList = _filterService.GetAll();
            var filters = new List<FilterResponseDTO>();
            foreach (var filter in filterList.Take((page - 1)..size))
            {
                filters.Add(new FilterResponseDTO(filter.Id)
                {
                    Name = filter.Name,
                    Label = filter.Label,
                    Script = filter.Script,
                    Parameter = filter.Parameter,
                    DependentParameters = filter.DependentParameters,
                    Status = filter.Status,
                    CreatedOn = filter.CreatedOn,
                    CreatedBy = filter.CreatedBy,
                    UpdatedOn = filter.UpdatedOn,
                    UpdatedBy = filter.UpdatedBy
                });
            }

            return filters;
        }

        public FilterPostResponseDTO Save(FilterPostRequestDTO filter)
        {
            try
            {
                var db = new Filter
                {
                    Id = filter.Id,
                    Name = filter.Name,
                    Label = filter.Label,
                    Script = filter.Script,
                    Parameter = filter.Parameter,
                    DependentParameters = filter.DependentParameters,
                    Status = filter.Status,
                    UpdatedOn = filter.UpdatedOn,
                    UpdatedBy = "admin",
                    CreatedBy = "admin" ,
                    CreatedOn = filter.Id == 0 ? DateTime.Now : filter.CreatedOn,

                };
                var result = _filterService.Save(db);

                return new FilterPostResponseDTO
                {
                    Id = result?.Id ?? 0,
                    Success = result != null,
                    Message = result != null ? "filter saved successfully" : "Error while saving"
                };
            }
            catch (Exception ex)
            {
                return new FilterPostResponseDTO
                {
                    Id = 0,
                    Success = false,
                    Message = "Error while saving: " + ex.Message
                };
            }
        }
    }
}
