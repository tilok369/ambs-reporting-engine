using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambs.Reporting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IWebHostEnvironment _environment;

        public ReportController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        // GET: api/<ReportController>

        //[HttpGet]
        //[Route("report-export")]
        //public async Task<IActionResult> ReportExport()
        //{
        //    ExcelPackage package = new ExcelPackage(); //To create excel package  
        //    package.Workbook.Worksheets.Add("xyz"); //To add new sheet to Excel package with name‘ xyz’  
        //    ExcelWorksheet testWorksheet = package.Workbook.Worksheets["xyz"]; //to get excel worksheet object by name  
        //    ExcelChart chart = testWorksheet.Drawings.AddChart("chart", eChartType.ColumnClustered); //To add chart to added sheet of type column clustered chart  

        //    chart.XAxis.Title.Text = "Months"; //give label to x-axis of chart  
        //    chart.XAxis.Title.Font.Size = 10;
        //    chart.YAxis.Title.Text = "Usage(kwh)"; //give label to Y-axis of chart  
        //    chart.YAxis.Title.Font.Size = 10;
        //    chart.SetSize(1200, 300);
        //    chart.SetPosition(1, 0, 5, 0);
        //    consumptionWorksheet.Cells[1, 1].LoadFromCollection(consumptionComparisonDetails, false, OfficeOpenXml.Table.TableStyles.Medium1);

        //    consumptionWorksheet.Cells[1, 1].Value = "Month";
        //    consumptionWorksheet.Cells[1, 2].Value = "Current Year Consumption";
        //    consumptionWorksheet.Cells[1, 3].Value = "Previous Year Consumption";
        //    consumptionWorksheet.Cells[1, 1].Style.Font.Bold = true;
        //    consumptionWorksheet.Cells[1, 2].Style.Font.Bold = true;
        //    consumptionWorksheet.Cells[1, 3].Style.Font.Bold = true;


        //    var row = 1;
        //    var consumptionCurrentYearSeries = chart.Series.Add(("B" + (row + 1) + ":" + "B" + (consumptionComparisonDetails.Count + 1)), ("A" + (row + 1) + ":" + "A" + (consumptionComparisonDetails.Count + 1)));
        //    consumptionCurrentYearSeries.Header = "Current Year";
        //    var consumptionPreviousYearSeries = chart.Series.Add(("C" + (row + 1) + ":" + "C" + (consumptionComparisonDetails.Count + 1)), ("A" + (row + 1) + ":" + "A" + (consumptionComparisonDetails.Count + 1)));
        //    consumptionPreviousYearSeries.Header = "Previous Year";

        //    var consumptionCurrentYearSeries = chart.Series.Add("B2:B13", "A2:A13");
        //}

        [HttpGet]
        [Route("report-export")]
        public async Task<IActionResult> ReportExport(string fileName)
        {
            var filePath = string.Empty;

            filePath = Path.Combine("D:\\ambs-reporting-engine\\src\\api\\Ambs.Reporting\\Ambs.Reporting.Api\\ExportData\\" + fileName);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fileInfo = new FileInfo(fileName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet myWorksheet = package.Workbook.Worksheets.Add("Test Report");


                myWorksheet.Cells[1, 1].Value = "Developer";
                myWorksheet.Cells[2, 1].Value = 26;
                myWorksheet.Cells[1, 2].Value = "QA";
                myWorksheet.Cells[2, 2].Value = 10;
                myWorksheet.Cells[1, 3].Value = "Implementation";
                myWorksheet.Cells[2, 3].Value = 5;


                var myChart = myWorksheet.Drawings.AddChart("pieChart", eChartType.Pie3D) as ExcelPieChart;

                myChart.Series.Add(ExcelRange.GetAddress(2, 1, 2, 3), ExcelRange.GetAddress(1, 1, 1, 3));
                //var series = myChart.Series.Add("C2: C4", "B2: B4");
                myChart.Border.Fill.Color = System.Drawing.Color.Green;
                myChart.Title.Text = "Employee Ratio";

                myChart.SetSize(400, 400);
                myChart.SetPosition(6, 0, 6, 0);

                FileInfo fi = new FileInfo(@"D:\ambs-reporting-engine\src\api\Ambs.Reporting\Ambs.Reporting.Api\ExportData\" + fileName + ".xlsx");
                package.SaveAs(fi);
            }

            return Ok();

        }



    }
}
