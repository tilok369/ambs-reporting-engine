using Ambs.Reporting.ViewModel.Reponse.Report;
using Ambs.Reporting.ViewModel.Request.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IReportLogic
{
    ReportResponseDTO Get(long id);
    IEnumerable<ReportListResponseDTO> GetAll(int page, int size);
    ReportPostResponseDTO Add(ReportPostRequestDTO report);
    ReportPostResponseDTO Edit(ReportPostRequestDTO report);
    ReportPostResponseDTO Delete(long id);
}

