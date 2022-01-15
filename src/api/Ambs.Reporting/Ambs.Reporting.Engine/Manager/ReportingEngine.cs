using Ambs.Reporting.Engine.Model;
using Ambs.Reporting.Utility.Report;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Ambs.Reporting.Engine.Manager
{
    public class ReportingEngine : IReportingEngine
    {
        public ExportData GetExportData(ReportData data)
        {
            var exportData = new ExportData { Rows = data.Rows, Columns = data.Columns };
            var maxLayer = GetMaxLayers(exportData.Columns);
            exportData.Layers = GetLayers(exportData.Columns, maxLayer);
            exportData.SheetName = "Sheet";
            return exportData;
        }

        public List<ExportData> GetExportData(List<ReportData> datas)
        {
            var exportDataList=new List<ExportData>();
            foreach (var data in datas)
                exportDataList.Add(GetExportData(data));
            return exportDataList;
        }
        //private static int GetMaxLayer(List<string> columns)
        //{
        //    return columns.Select(column => column.Split('_').Length - 1).Concat(new[] { 1 }).Max();
        //}
        private static int GetMaxLayers(List<string> columns)
        {
            return columns.Select(column => column.Split('_').Count() - 1).Concat(new[] { 1 }).Max();
        }
        private static List<DataLayer> GetLayers(List<string> columns, int maxLayerCount)
        {
            var dataLayers = new List<DataLayer>();
            var colOrder = 0;
            for (var i = 0; i < maxLayerCount; i++)
            {
                colOrder = 0;
                var calculatedColumns = new List<Column>();
                var layerColumns = GetLayeredColumns(columns, i);
                foreach (var layerColumn in layerColumns)
                {
                    colOrder++;
                    var parentColumnName = string.Empty;
                    if (i > 0)
                    {
                        var layerColumnArr = layerColumn.ColumnName.Split("_").ToList();
                        var thisColumnIndex = layerColumnArr.IndexOf(layerColumn.Text);
                        parentColumnName = layerColumnArr[thisColumnIndex-1];
                    }
                    calculatedColumns.Add(new Column
                    {
                        Order = colOrder,
                        ColumnName = layerColumn.Text,
                        ColumnSpan = CalculateColumnSpan(columns, layerColumn.ColumnName),
                        RowSpan = CalculateRowSpan(columns, layerColumn.ColumnName, maxLayerCount, i),
                        ParentColumn= parentColumnName
                    });
                }
                dataLayers.Add(new DataLayer { Columns = calculatedColumns,Order=i+1 });
            }

            var layersWithIndex = GetDataLayersWithIndex(dataLayers);

            return layersWithIndex;
        }
        private static List<LayeredColumn> GetLayeredColumns(List<string> columns, int layerNumber)
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
        private static int MaxLayerSize(List<string> columns, string columnName)
        {
            return columns.Where(c => c.Contains(columnName)).Max(c => c.Split('_').Length - 1);
        }
        private static int CalculateRowSpan(List<string> columns, string columnName, int numOfLayers, int i)
        {
            var maxLayerSize = MaxLayerSize(columns, columnName);
            var rowSpan= i == 0 && maxLayerSize > 1 ? 1 : 1 + numOfLayers - maxLayerSize;
            return rowSpan;
        }
        private static Column GetParentColumn(List<DataLayer> dataLayers, Column column)
        {
            if (string.IsNullOrEmpty(column.ParentColumn)) return column;
            foreach (var layer in dataLayers)
            {
                var parentColumn = layer.Columns.FirstOrDefault(pc => pc.ColumnName == column.ParentColumn);
                if (parentColumn != null)
                {
                    return parentColumn;
                }

            }
            return column;
        }
        private static int GetRowNumber(List<DataLayer> dataLayers, Column column,ref int rowNumber)
        {
            if (string.IsNullOrEmpty(column.ParentColumn)) return rowNumber;
            var parentColumn=GetParentColumn(dataLayers, column);
            rowNumber += parentColumn.RowSpan;
            if (string.IsNullOrEmpty(parentColumn.ParentColumn)) return rowNumber;
            GetRowNumber(dataLayers, parentColumn,ref rowNumber);
            return rowNumber;
        }
        private static List<DataLayer> GetDataLayersWithIndex(List<DataLayer> dataLayers)
        {
            
            foreach (var layer in dataLayers)
            {
                foreach(var layerColumn in layer.Columns)
                {
                    var rowNumber = 1;
                    layerColumn.RowIndex = GetRowNumber(dataLayers,layerColumn,ref rowNumber);
                    var previousColumns = layer.Columns.Where(d => d.Order < layerColumn.Order).ToList();
                    if (layer.Order == 1)
                    {
                        layerColumn.ColumnIndex = previousColumns.Sum(c => c.ColumnSpan) + 1;
                    }
                    else
                    {
                        var parentLayer = dataLayers.OrderByDescending(d => d.Order).FirstOrDefault(d=>d.Order<layer.Order);
                        if(parentLayer != null)
                        {
                            var parentColumn = parentLayer.Columns.FirstOrDefault(c=>c.ColumnName==layerColumn.ParentColumn);                            
                            if (parentColumn != null)
                            {
                                var childColumns = layer.Columns.Where(c => c.ParentColumn == parentColumn.ColumnName).ToList();
                                layerColumn.ColumnIndex=parentColumn.ColumnIndex+childColumns.IndexOf(layerColumn);
                            }
                        }
                        
                    }
                }
                //initialRowIndex++;
            }
            //dataLayers.ForEach(layer =>
            //{
            //    layer.Columns.Where(c => !IsParent(dataLayers, c)).ToList().ForEach(layerColumn => layerColumn.RowSpan = 1);
            //});
            return dataLayers;
        }
        //private bool IsParent(List<DataLayer> dataLayers, Column parentColumn)
        //{
        //    if (string.IsNullOrEmpty(parentColumn.ParentColumn)) return true;
        //    var isParent = false;
        //    var layers= dataLayers.Where(d => d.Columns.IndexOf(parentColumn) != -1).ToList();
        //    foreach (DataLayer layer in layers)
        //    {
        //        foreach(var column in layer.Columns)
        //        {
        //            if (column.ParentColumn == parentColumn.ColumnName)
        //            {
        //                isParent = true;
        //                break;
        //            }
        //        }
        //    }
        //    return isParent;
        //}
        //private List<DataLayer> GetDataLayersWithIndex(List<DataLayer> dataLayers)
        //{
        //    var initialColumnIndex = 1;
        //    for (var i = 0; i < dataLayers.Count(); i++)
        //    {
        //        var initialRowIndex = i + 1;
        //        for (var j = 0; j < dataLayers[i].Columns.Count(); j++)
        //        {
        //            dataLayers[i].Columns[j].RowIndex = initialRowIndex;
        //            if (i == 0)
        //            {
        //                initialColumnIndex = j == 0
        //                    ? initialColumnIndex
        //                    : initialColumnIndex + dataLayers[i].Columns[j - 1].ColumnSpan;
        //                dataLayers[i].Columns[j].ColumnIndex = initialColumnIndex;


        //            }

        //            if (i <= 0) continue;
        //            initialColumnIndex = (int)(j == 0
        //                ? dataLayers[i - 1].Columns
        //                    .FirstOrDefault(cl => (cl.RowSpan + cl.RowIndex) <= initialRowIndex)?.ColumnIndex
        //                : initialColumnIndex + dataLayers[i].Columns[j - 1].ColumnSpan);
        //            initialColumnIndex = j == 0 ? initialColumnIndex : GetValidColumnIndex(dataLayers, initialColumnIndex, i, j);
        //            dataLayers[i].Columns[j].ColumnIndex = initialColumnIndex;

        //        }
        //    }
        //    return dataLayers;
        //}
        private static int GetValidColumnIndex(List<DataLayer> layers, int initialColumnIndex, int i, int j)
        {

            var previousLayerSelectedColumn =
                layers[i - 1].Columns.LastOrDefault(cl => cl.ColumnIndex <= initialColumnIndex);
            var validColumn = CheckValidity(layers, previousLayerSelectedColumn, i, j);
            return previousLayerSelectedColumn != null && previousLayerSelectedColumn.ColumnIndex == validColumn.ColumnIndex ? initialColumnIndex : validColumn.ColumnIndex;

        }
        private static Column CheckValidity(List<DataLayer> layers, Column previousLayerSelectedColumn, int i, int j)
        {

            while (previousLayerSelectedColumn.RowSpan + previousLayerSelectedColumn.RowIndex >
                   layers[i].Columns[j].RowIndex)
            {
                var idx = layers[i - 1].Columns.FindIndex(c => c.ColumnIndex == previousLayerSelectedColumn.ColumnIndex);
                previousLayerSelectedColumn = layers[i - 1].Columns[idx + 1];
            }
            return previousLayerSelectedColumn;
        }
        private static int CalculateColumnSpan(List<string> columns, string columnName)
        {
            return columns.Count(c => c.Contains(columnName));
        }

        public byte[] GetExcelData(List<ExportData> datas,string fileName)
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
                var maxRowIndex=0;
                layers.ForEach(layer =>
                {
                    var max = layer.Columns.Max(c => c.RowIndex);
                    if (max > maxRowIndex)
                        maxRowIndex = max;
                });
                rowIndex = maxRowIndex + rowIndex + 2;
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

        public byte[] GetPdflData(List<ExportData> datas, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
