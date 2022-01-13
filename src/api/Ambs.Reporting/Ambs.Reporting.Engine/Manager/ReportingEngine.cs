using Ambs.Reporting.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Engine.Manager
{
    internal class ReportingEngine : IReportingEngine
    {
        public AmbsExportData GetExportData(AmbsReportData data)
        {
            var exportData = new AmbsExportData { Rows = data.Rows, Columns = data.Columns };
            var maxLayer = GetMaxLayer(exportData.Columns);
            exportData.Layers = GetLayers(exportData.Columns, maxLayer);
            return exportData;
        }

        public List<AmbsExportData> GetExportData(List<AmbsReportData> datas)
        {
            var exportDataList=new List<AmbsExportData>();
            foreach (var data in datas)
                exportDataList.Add(GetExportData(data));
            return exportDataList;
        }
        private static int GetMaxLayer(List<string> columns)
        {
            return columns.Select(column => column.Split('_').Length - 1).Concat(new[] { 1 }).Max();
        }
        private List<AmbsDataLayer> GetLayers(List<string> columns, int maxLayer)
        {
            var dataLayers = new List<AmbsDataLayer>();
            for (var i = 0; i < maxLayer; i++)
            {
                var calculatedColumns = new List<AmbsColumn>();
                var layerColumns =GetLayeredColumns(columns, i);
                foreach (var layerColumn in layerColumns)
                {
                    calculatedColumns.Add(new AmbsColumn
                    {
                        ColumnName = layerColumn.Text,
                        ColumnSpan = CalculateColumnSpan(columns, layerColumn.ColumnName),
                        RowSpan = CalculateRowSpan(columns, layerColumn.ColumnName, maxLayer, i)
                    });
                }
                dataLayers.Add(new AmbsDataLayer { Columns = calculatedColumns });
            }
            var layersWithIndex= GetDataLayersWithIndex(dataLayers);
            return layersWithIndex;
        }
        private List<LayeredColumn> GetLayeredColumns(List<string> columns, int layerNumber)
        {
            var layeredColumns = new List<LayeredColumn>();
            foreach (var column in columns)
            {
                var layerColumnStr = column.Split('_').ToList();
                layerColumnStr = layerColumnStr.Take(layerColumnStr.Count - 1).ToList();
                if (layerColumnStr.Count < layerNumber + 1) continue;
                var columnName = string.Join("_", layerColumnStr.Take(layerColumnStr.Count + 1).ToList());
                if (!layeredColumns.Any(c => c.ColumnName.Contains(columnName)))
                    layeredColumns.Add(new LayeredColumn
                    {
                        ColumnName = columnName,
                        Text = layerColumnStr[layerNumber]
                    });
            }
            return layeredColumns;
        }
        private static int CalculateColumnSpan(List<string> columns, string columnName)
        {
            return columns.Count(c => c.Contains(columnName));
        }
        private static int MaxLayerSize(List<string> columns, string columnName)
        {
            return columns.Where(c => c.Contains(columnName)).Max(c => c.Split('_').Length - 1);
        }
        private static int CalculateRowSpan(List<string> columns, string columnName, int numOfLayers, int i)
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
    }
}
