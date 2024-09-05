using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Excel/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet
                        var dataTable = new DataTable();

                        // Create columns
                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            dataTable.Columns.Add(worksheet.Cells[1, col].Text);
                        }

                        // Create rows
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var dataRow = dataTable.NewRow();
                            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                            {
                                dataRow[col - 1] = worksheet.Cells[row, col].Text;
                            }
                            dataTable.Rows.Add(dataRow);
                        }

                        ViewData["DataTable"] = dataTable;
                    }
                }
            }

            return View("Display");
        }
    }
}
