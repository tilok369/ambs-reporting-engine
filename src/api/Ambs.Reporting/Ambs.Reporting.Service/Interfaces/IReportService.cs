﻿using Ambs.Reporting.DAL.CalculativeModels;
namespace Ambs.Reporting.Service.Interfaces;
public interface IReportService
{
    Report Get(long id);
    IEnumerable<ReportList> GetAll(int page, int size);
    Report Add(Report report);
    Report Edit(Report report);
    Report Delete(long id);
}

