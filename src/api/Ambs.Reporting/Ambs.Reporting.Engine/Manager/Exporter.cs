﻿using Ambs.Reporting.Engine.Model;
using Ambs.Reporting.Utility.Report;
using OfficeOpenXml;
using SautinSoft;

namespace Ambs.Reporting.Engine.Manager;

public class Exporter : IExporter
{
    public async Task<byte[]> GetExcelData(List<ExportData> datas, string fileName, string contentRootPath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(new FileInfo(fileName));
        var sheets = datas
            //.OrderBy(d => d.Order)
            .Select(d => d.SheetName).Distinct().ToList();
        var index = 0;
        foreach (var sheetData in sheets.Select(sheet => datas.FirstOrDefault(d => d.SheetName == sheet)).Where(sheetData => sheetData != null))
        {
            package.Workbook.Worksheets.Add(sheetData.SheetName);
            var worksheet = package.Workbook.Worksheets[index++];

            worksheet.Cells[1, 1].Value = sheetData.SheetName;
            worksheet.Cells[1, 1, 1, sheetData.Columns.Count].Merge = true;
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1].Style.Font.Size = 20;
            worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 1].Value = "Branch Name: Branch";
            worksheet.Cells[2, 1, 2, 6].Merge = true;
            worksheet.Cells[2, 1, 2, 6].Style.Font.Bold = true;
            worksheet.Cells[2, 1, 2, 6].Style.Font.Size = 14;
            worksheet.Cells[2, 1, 2, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 7].Value = "Date: Date";
            worksheet.Cells[2, 7, 2, 12].Merge = true;
            worksheet.Cells[2, 7, 2, 12].Style.Font.Bold = true;
            worksheet.Cells[2, 7, 2, 12].Style.Font.Size = 14;
            worksheet.Cells[2, 7, 2, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            if (File.Exists(contentRootPath + @"\Resources\Images\ASAI Logo.png"))
            {
                var asaiLogo = worksheet.Drawings.AddPicture("Test", System.Drawing.Image.FromFile(contentRootPath + @"\Resources\Images\ASAI Logo.png"));
                asaiLogo.SetPosition(3, 5, 3, 5);
                asaiLogo.SetSize(300, 300);
            }
            var layers = sheetData.Layers;
            var rowIndex = 4;
            rowIndex -= 1;
            foreach (var layer in layers)
            {
                foreach (var column in layer.Columns)
                {
                    ExcelReportUtility.FormatHeaderCell(worksheet.Cells, rowIndex + column.RowIndex, column.ColumnIndex,
                        column.RowSpan, column.ColumnSpan);
                    worksheet.Cells[rowIndex + column.RowIndex, column.ColumnIndex].Value =
                        column.ColumnName;
                }
            }

            var lastLayer = layers.LastOrDefault();
            var lastLayerLastColumn = lastLayer?.Columns.LastOrDefault();
            if (lastLayerLastColumn == null) continue;
            var maxRowIndex = 0;
            layers.ForEach(layer =>
            {
                var max = layer.Columns.Max(c => c.RowIndex);
                if (max > maxRowIndex)
                    maxRowIndex = max;
            });
            rowIndex = maxRowIndex + rowIndex + 1;
            foreach (var row in sheetData.Rows)
            {
                var colIndex = 1;
                var isTotalRow = row.Any(r => r.ToString().Contains("Total"));
                foreach (var column in row)
                {
                    ExcelReportUtility.FormatDataCell(worksheet.Cells, rowIndex, colIndex, 1, 1,
                        isTotalRow);
                    worksheet.Cells[rowIndex, colIndex++].Value = column;
                }

                rowIndex++;
            }
        }
        return await package.GetAsByteArrayAsync();
    }

    public async Task<byte[]> GetPdfData(List<ExportData> datas, string fileName, string contentRootPath)
    {
        var excelByteArray = await GetExcelData(datas, fileName, contentRootPath);
        var excelToPdf = new ExcelToPdf
        {
            OutputFormat = ExcelToPdf.eOutputFormat.Pdf
        };
        return excelToPdf.ConvertBytes(excelByteArray);
    }
}

