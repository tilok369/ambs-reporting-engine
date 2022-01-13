using Ambs.Reporting.Engine.Model;
using Ambs.Reporting.Utility.Report;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Engine.Manager
{
    public class ReportingEngine : IReportingEngine
    {
        public AmbsExportData GetExportData(AmbsReportData data)
        {
            var exportData = new AmbsExportData { Rows = data.Rows, Columns = data.Columns };
            var maxLayer = GetMaxLayers(exportData.Columns);
            exportData.Layers = GetLayers(exportData.Columns, maxLayer);
            exportData.SheetName = "Sheet";
            return exportData;
        }

        public List<AmbsExportData> GetExportData(List<AmbsReportData> datas)
        {
            var exportDataList=new List<AmbsExportData>();
            foreach (var data in datas)
                exportDataList.Add(GetExportData(data));
            return exportDataList;
        }
        //private static int GetMaxLayer(List<string> columns)
        //{
        //    return columns.Select(column => column.Split('_').Length - 1).Concat(new[] { 1 }).Max();
        //}
        private int GetMaxLayers(List<string> columns)
        {
            return columns.Select(column => column.Split('_').Count() - 1).Concat(new[] { 1 }).Max();
        }
        private List<AmbsDataLayer> GetLayers(List<string> columns, int maxLayerCount)
        {
            var dataLayers = new List<AmbsDataLayer>();

            for (var i = 0; i < maxLayerCount; i++)
            {
                var calculatedColumns = new List<AmbsColumn>();
                var layerColumns = GetLayeredColumns(columns, i);
                foreach (var layerColumn in layerColumns)
                {
                    calculatedColumns.Add(new AmbsColumn
                    {
                        ColumnName = layerColumn.Text,
                        ColumnSpan = CalculateColumnSpan(columns, layerColumn.ColumnName),
                        RowSpan = CalculateRowSpan(columns, layerColumn.ColumnName, maxLayerCount, i)
                    });
                }
                dataLayers.Add(new AmbsDataLayer { Columns = calculatedColumns });
            }

            var layersWithIndex = GetDataLayersWithIndex(dataLayers);

            return layersWithIndex;
        }
        private List<LayeredColumn> GetLayeredColumns(List<string> columns, int layerNumber)
        {
            var layeredColumns = new List<LayeredColumn>();
            foreach (var column in columns)
            {
                var layerColumnStr = column.Split('_').ToList();
                layerColumnStr = layerColumnStr.Take(layerColumnStr.Count - 1).ToList();
                if (layerColumnStr.Count >= layerNumber + 1)
                {
                    var columnName = string.Join("_", layerColumnStr.Take(layerNumber + 1).ToList());
                    if (!layeredColumns.Any(c => c.ColumnName.Contains(columnName)))
                        layeredColumns.Add(new LayeredColumn
                        {
                            ColumnName = columnName,
                            Text = layerColumnStr[layerNumber]
                        });
                }
            }
            return layeredColumns;
        }
        private int MaxLayerSize(List<string> columns, string columnName)
        {
            return columns.Where(c => c.Contains(columnName)).Max(c => c.Split('_').Length - 1);
        }
        private int CalculateRowSpan(List<string> columns, string columnName, int numOfLayers, int i)
        {
            return i == 0 && MaxLayerSize(columns, columnName) > 1 ? 1 : 1 + numOfLayers - MaxLayerSize(columns, columnName);
        }
        private List<AmbsDataLayer> GetDataLayersWithIndex(List<AmbsDataLayer> dataLayers)
        {
            var initialColumnIndex = 1;
            for (var i = 0; i < dataLayers.Count(); i++)
            {
                var initialRowIndex = i + 1;
                for (var j = 0; j < dataLayers[i].Columns.Count(); j++)
                {
                    dataLayers[i].Columns[j].RowIndex = initialRowIndex;
                    if (i == 0)
                    {
                        initialColumnIndex = j == 0
                            ? initialColumnIndex
                            : initialColumnIndex + dataLayers[i].Columns[j - 1].ColumnSpan;
                        dataLayers[i].Columns[j].ColumnIndex = initialColumnIndex;


                    }

                    if (i <= 0) continue;
                    // ReSharper disable once PossibleInvalidOperationException
                    initialColumnIndex = (int)(j == 0
                        ? dataLayers[i - 1].Columns
                            .FirstOrDefault(cl => (cl.RowSpan + cl.RowIndex) <= initialRowIndex)?.ColumnIndex
                        : initialColumnIndex + dataLayers[i].Columns[j - 1].ColumnSpan);
                    initialColumnIndex = j == 0 ? initialColumnIndex : GetValidColumnIndex(dataLayers, initialColumnIndex, i, j);
                    dataLayers[i].Columns[j].ColumnIndex = initialColumnIndex;

                }
            }
            return dataLayers;
        }
        private int GetValidColumnIndex(List<AmbsDataLayer> layers, int initialColumnIndex, int i, int j)
        {

            var previousLayerSelectedColumn =
                layers[i - 1].Columns.LastOrDefault(cl => cl.ColumnIndex <= initialColumnIndex);
            var validColumn = CheckValidity(layers, previousLayerSelectedColumn, i, j);
            return previousLayerSelectedColumn != null && previousLayerSelectedColumn.ColumnIndex == validColumn.ColumnIndex ? initialColumnIndex : validColumn.ColumnIndex;

        }
        private AmbsColumn CheckValidity(List<AmbsDataLayer> layers, AmbsColumn previousLayerSelectedColumn, int i, int j)
        {

            while (previousLayerSelectedColumn.RowSpan + previousLayerSelectedColumn.RowIndex >
                   layers[i].Columns[j].RowIndex)
            {
                var idx = layers[i - 1].Columns.FindIndex(c => c.ColumnIndex == previousLayerSelectedColumn.ColumnIndex);
                previousLayerSelectedColumn = layers[i - 1].Columns[idx + 1];
            }
            return previousLayerSelectedColumn;
        }
        private int CalculateColumnSpan(List<string> columns, string columnName)
        {
            return columns.Count(c => c.Contains(columnName));
        }

        public byte[] GetExcelData(List<AmbsExportData> datas,string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(new FileInfo(fileName));
            var sheets = datas
                //.OrderBy(d => d.Order)
                .Select(d => d.SheetName).Distinct().ToList();
            foreach (var sheetData in sheets.Select(sheet => datas.FirstOrDefault(d => d.SheetName == sheet)).Where(sheetData => sheetData != null))
            {
                package.Workbook.Worksheets.Add("Sheet 1");
                var worksheet = package.Workbook.Worksheets[0];

                worksheet.Cells[1, 1].Value = sheetData.SheetName;
                worksheet.Cells[1, 1, 1, sheetData.Columns.Count].Merge = true;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 20;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[2, 1].Value = "Branch Name: Branch" ;
                worksheet.Cells[2, 1, 2, 6].Merge = true;
                worksheet.Cells[2, 1, 2, 6].Style.Font.Bold = true;
                worksheet.Cells[2, 1, 2, 6].Style.Font.Size = 14;
                worksheet.Cells[2, 1, 2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[2, 7].Value = "Date: Date";
                worksheet.Cells[2, 7, 2, 12].Merge = true;
                worksheet.Cells[2, 7, 2, 12].Style.Font.Bold = true;
                worksheet.Cells[2, 7, 2, 12].Style.Font.Size = 14;
                worksheet.Cells[2, 7, 2, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                var asaiLogo = worksheet.Drawings.AddPicture("Test", Image.FromFile(@"D:\ASAI Logo.png"));
                asaiLogo.SetPosition(3, 5, 3, 5);
                asaiLogo.SetSize(300, 300);
                var layers = sheetData.Layers;
                var rowIndex = 4;
                rowIndex -= 1;
                foreach (var column in layers.SelectMany(layer => layer.Columns))
                {
                    ExcelReportUtility.FormatHeaderCell(worksheet.Cells, rowIndex + column.RowIndex,
                        column.ColumnIndex,
                        column.RowSpan, column.ColumnSpan);
                    worksheet.Cells[rowIndex + column.RowIndex, column.ColumnIndex].Value =
                        column.ColumnName;
                }

                var lastLayer = layers.LastOrDefault();
                var lastLayerLastColumn = lastLayer?.Columns.LastOrDefault();
                if (lastLayerLastColumn == null) continue;
                rowIndex = lastLayerLastColumn.RowIndex + rowIndex + 1;
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
            return package.GetAsByteArray();
        }

        public byte[] GetPdflData(List<AmbsExportData> datas, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
