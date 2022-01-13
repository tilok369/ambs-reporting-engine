using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Utility.Report
{
    public static class ExcelReportUtility
    {
        public static void FormatHeaderCell(ExcelRange cell, int rowIndex, int colIndex, int rowSpan = 1, int colSpan = 1)
        {
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Merge = true;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Font.Bold = true;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(174, 211, 240));
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        }
        public static void FormatDataCell(ExcelRange cell, int rowIndex, int colIndex, int rowSpan = 1, int colSpan = 1, bool isBoldRow = false)
        {
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Merge = true;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Fill.BackgroundColor.SetColor(Color.White);
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;

            if (!isBoldRow) return;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Font.Bold = true;
            cell[rowIndex, colIndex, rowIndex + rowSpan - 1, colIndex + colSpan - 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
        }
    }
}
