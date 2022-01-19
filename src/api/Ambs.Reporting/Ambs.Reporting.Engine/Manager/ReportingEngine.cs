using Ambs.Reporting.Engine.Model;
using System.Data;

namespace Ambs.Reporting.Engine.Manager;
public class ReportingEngine : IReportingEngine
{
    public async Task<ExportData> GetExportData(ReportData data)
    {
        var exportData = new ExportData { Rows = data.Rows, Columns = data.Columns };
        var maxLayer = GetMaxLayers(exportData.Columns);
        exportData.Layers = GetLayers(exportData.Columns, maxLayer);
        exportData.SheetName = "Sheet";
        return exportData;
    }

    public async Task<List<ExportData>> GetExportData(List<ReportData> datas)
    {
        var exportDataList = new List<ExportData>();
        foreach (var data in datas)
            exportDataList.Add(await GetExportData(data));
        exportDataList.ForEach(data =>
        {
            data.SheetName += (exportDataList.IndexOf(data) + 1);
        });
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
        if (maxLayerCount == 1)
        {
            var columnList = new List<Column>();
            var index = 0;
            foreach(var column in columns)
            {
                index++;
                columnList.Add(new Column { ColumnIndex = index, ColumnName = column, ColumnSpan = 1, ParentColumn = string.Empty, RowIndex = 1, RowSpan = 1, Order = index });
            }
            var dataLayer = new DataLayer { Columns = columnList, Order = 1 };
            return new List<DataLayer> { dataLayer };
        }
        var colOrder = 0;
        for (var i = 0; i < maxLayerCount; i++)
        {
            colOrder = 0;
            var calculatedColumns = new List<Model.Column>();
            var layerColumns = GetLayeredColumns(columns, i);
            foreach (var layerColumn in layerColumns)
            {
                colOrder++;
                var parentColumnName = string.Empty;
                if (i > 0)
                {
                    var layerColumnArr = layerColumn.ColumnName.Split("_").ToList();
                    var thisColumnIndex = layerColumnArr.IndexOf(layerColumn.Text);
                    parentColumnName = layerColumnArr[thisColumnIndex - 1];
                }
                calculatedColumns.Add(new Model.Column
                {
                    Order = colOrder,
                    ColumnName = layerColumn.Text,
                    ColumnSpan = CalculateColumnSpan(columns, layerColumn.ColumnName),
                    RowSpan = CalculateRowSpan(columns, layerColumn.ColumnName, maxLayerCount, i),
                    ParentColumn = parentColumnName
                });
            }
            dataLayers.Add(new DataLayer { Columns = calculatedColumns, Order = i + 1 });
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
        var rowSpan = i == 0 && maxLayerSize > 1 ? 1 : 1 + numOfLayers - maxLayerSize;
        return rowSpan;
    }
    private static Model.Column GetParentColumn(List<DataLayer> dataLayers, Model.Column column)
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
    private static int GetRowNumber(List<DataLayer> dataLayers, Model.Column column, ref int rowNumber)
    {
        if (string.IsNullOrEmpty(column.ParentColumn)) return rowNumber;
        var parentColumn = GetParentColumn(dataLayers, column);
        rowNumber += parentColumn.RowSpan;
        if (string.IsNullOrEmpty(parentColumn.ParentColumn)) return rowNumber;
        GetRowNumber(dataLayers, parentColumn, ref rowNumber);
        return rowNumber;
    }
    //private static List<DataLayer> GetDataLayersWithIndex(List<DataLayer> dataLayers)
    //{

    //    foreach (var layer in dataLayers)
    //    {
    //        foreach(var layerColumn in layer.Columns)
    //        {
    //            var rowNumber = 1;
    //            layerColumn.RowIndex = GetRowNumber(dataLayers,layerColumn,ref rowNumber);
    //            var previousColumns = layer.Columns.Where(d => d.Order < layerColumn.Order).ToList();
    //            if (layer.Order == 1)
    //            {
    //                layerColumn.ColumnIndex = previousColumns.Sum(c => c.ColumnSpan) + 1;
    //            }
    //            else
    //            {
    //                var parentLayer = dataLayers.OrderByDescending(d => d.Order).FirstOrDefault(d=>d.Order<layer.Order);
    //                if(parentLayer != null)
    //                {
    //                    var parentColumn = parentLayer.Columns.FirstOrDefault(c=>c.ColumnName==layerColumn.ParentColumn);                            
    //                    if (parentColumn != null)
    //                    {
    //                        var childColumns = layer.Columns.Where(c => c.ParentColumn == parentColumn.ColumnName).ToList();
    //                        layerColumn.ColumnIndex=parentColumn.ColumnIndex+childColumns.IndexOf(layerColumn);
    //                    }
    //                }

    //            }
    //        }
    //        //initialRowIndex++;
    //    }
    //    //dataLayers.ForEach(layer =>
    //    //{
    //    //    layer.Columns.Where(c => !IsParent(dataLayers, c)).ToList().ForEach(layerColumn => layerColumn.RowSpan = 1);
    //    //});
    //    return dataLayers;
    //}
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
    private static List<DataLayer> GetDataLayersWithIndex(List<DataLayer> dataLayers)
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
    private static int GetValidColumnIndex(List<DataLayer> layers, int initialColumnIndex, int i, int j)
    {

        var previousLayerSelectedColumn =
            layers[i - 1].Columns.LastOrDefault(cl => cl.ColumnIndex <= initialColumnIndex);
        var validColumn = CheckValidity(layers, previousLayerSelectedColumn, i, j);
        return previousLayerSelectedColumn != null && previousLayerSelectedColumn.ColumnIndex == validColumn.ColumnIndex ? initialColumnIndex : validColumn.ColumnIndex;

    }
    private static Model.Column CheckValidity(List<DataLayer> layers, Model.Column previousLayerSelectedColumn, int i, int j)
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
}

