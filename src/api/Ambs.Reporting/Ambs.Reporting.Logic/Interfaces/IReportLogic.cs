﻿using static Ambs.Reporting.Utility.Enum.ExportEnum;

namespace Ambs.Reporting.Logic.Interfaces;

public interface IReportLogic
{
    Task<byte[]> GetReportData(ExportType exportType);
    Task<byte[]> GetReportExport(string fileName);
}
