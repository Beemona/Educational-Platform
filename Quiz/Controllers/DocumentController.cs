using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigraDoc.Rendering;
using NPOI.SS.UserModel; // NPOI's common workbook and sheet interfaces
using NPOI.XSSF.UserModel; // NPOI's classes for .xlsx format (Excel 2007+)
using System.Data;
using System.IO;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using System.Diagnostics;
using PdfSharp.Drawing;
using NPOI.XWPF.UserModel;
using System.Text;
using DocumentPdf = MigraDoc.DocumentObjectModel.Document;
using ICell = NPOI.SS.UserModel.ICell;
using NPOI.POIFS.Common; // For PowerPoint support
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Packaging;

namespace Quiz.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Excel/Upload
        public IActionResult UploadExcel()
        {
            return View();
        }

        // POST: Excel/Upload
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    // Load the workbook using NPOI
                    stream.Position = 0; // Reset stream position to the beginning
                    IWorkbook workbook = new XSSFWorkbook(stream); // For .xlsx files

                    ISheet sheet = workbook.GetSheetAt(0); // Get the first worksheet
                    var dataTable = new DataTable();

                    // Get the header row (first row)
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum; // Get the number of columns

                    // Create columns for DataTable based on header row
                    for (int i = 0; i < cellCount; i++)
                    {
                        ICell headerCell = headerRow.GetCell(i);
                        dataTable.Columns.Add(headerCell?.ToString() ?? $"Column {i + 1}");
                    }

                    // Create rows for DataTable starting from the second row
                    for (int i = 1; i <= sheet.LastRowNum; i++) // Skip the header row
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; // Handle empty rows

                        var dataRow = dataTable.NewRow();
                        for (int j = 0; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);
                            dataRow[j] = cell?.ToString(); // Add the cell value to the DataRow
                        }

                        dataTable.Rows.Add(dataRow); // Add the DataRow to the DataTable
                    }

                    // Pass the DataTable to the ViewData for display
                    ViewData["DataTable"] = dataTable;
                }
            }

            return View("Display");
        }

        // Example: Export to PDF functionality
        public IActionResult ExportToPdf()
        {
            //  Create a new MigraDoc document
            DocumentPdf document = new DocumentPdf();
            Section section = document.AddSection();

           // Add a paragraph to the section
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 14;
            paragraph.AddText("This is a paragraph added to the PDF document.");

           // Add an image to the section(ensure image path is valid)
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/example.png");
            if (System.IO.File.Exists(imagePath))
            {
                section.AddImage(imagePath);
            }
            else
            {
                section.AddParagraph("Image not found: " + imagePath); // Error handling for missing image
            }

           // Render the document to a PDF
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = document;
            renderer.RenderDocument();

           // Save the PDF to a MemoryStream and return as a file download
            using (MemoryStream stream = new MemoryStream())
            {
                renderer.PdfDocument.Save(stream, false);
                byte[] pdfContent = stream.ToArray();
                return File(pdfContent, "application/pdf", "ParagraphAndImage.pdf");
            }
        }
        public IActionResult UploadPdf()
        {
            return View();
        }
        // POST: Document/Upload
        [HttpPost]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pdfFile != null && pdfFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, pdfFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pdfFile.CopyToAsync(stream);
                }

                // Pass the PDF URL to the ViewData for display
                ViewData["PdfUrl"] = Url.Content("~/uploads/" + pdfFile.FileName);
                return View("DisplayPdf");
            }

            return BadRequest("No file uploaded or file is empty.");
        }

        public IActionResult UploadWord()
        {
            return View(); // Returns the UploadWord view containing the form
        }

        // POST: Document/UploadWord
        [HttpPost]
        public async Task<IActionResult> UploadWord(IFormFile wordFile)
        {
            if (wordFile != null && wordFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, wordFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await wordFile.CopyToAsync(stream);
                }

                // Read the Word file and convert it to HTML
                string htmlContent = ConvertWordToHtml(filePath);

                // Pass the HTML content to the ViewData for display
                ViewData["HtmlContent"] = htmlContent;
                return View("DisplayWordContents"); // Create this view to display the HTML
            }

            return BadRequest("No file uploaded or file is empty.");
        }

        private string ConvertWordToHtml(string filePath)
        {
            StringBuilder html = new StringBuilder();

            // Open the .docx file using NPOI
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XWPFDocument doc = new XWPFDocument(fs);

                // Read paragraphs
                foreach (var paragraph in doc.Paragraphs)
                {
                    // Get alignment and indentation
                    var alignment = paragraph.Alignment;
                    var indent = paragraph.IndentationLeft; // Indentation in points
                    var indentStyle = indent > 0 ? $"margin-left: {indent / 20.0}em;" : ""; // Convert to em for CSS

                    html.Append($"<p style='{indentStyle} text-align: {GetTextAlign(alignment)};'>");

                    foreach (var run in paragraph.Runs)
                    {
                        string text = run.ToString();

                        // Handle bold text
                        if (run.IsBold)
                        {
                            html.Append("<strong>" + text + "</strong>");
                        }
                        // Handle italic text
                        else if (run.IsItalic)
                        {
                            html.Append("<em>" + text + "</em>");
                        }
                        else
                        {
                            html.Append(text);
                        }
                    }

                    html.Append("</p>");
                }

                // Read tables
                foreach (var table in doc.Tables)
                {
                    html.Append("<table border='1' style='border-collapse:collapse;'>");
                    foreach (var row in table.Rows)
                    {
                        html.Append("<tr>");

                        // Get the cells from the current row
                        foreach (var cell in row.GetTableCells())
                        {
                            html.Append("<td>" + cell.GetText() + "</td>"); // Display cell text
                        }

                        html.Append("</tr>");
                    }
                    html.Append("</table>");
                }
            }

            return html.ToString();
        }

        // Helper method to convert paragraph alignment to CSS text-align value
        private string GetTextAlign(NPOI.XWPF.UserModel.ParagraphAlignment alignment)
        {
            switch (alignment)
            {
                case NPOI.XWPF.UserModel.ParagraphAlignment.LEFT:
                    return "left";
                case NPOI.XWPF.UserModel.ParagraphAlignment.CENTER:
                    return "center";
                case NPOI.XWPF.UserModel.ParagraphAlignment.RIGHT:
                    return "right";
                case NPOI.XWPF.UserModel.ParagraphAlignment.BOTH:
                    return "justify";
                default:
                    return "left";
            }
        }

        public IActionResult UploadPPT()
        {
            return View();
        }

        // POST: Document/UploadPPT
        [HttpPost]
        public async Task<IActionResult> UploadPPT(IFormFile pptFile)
        {
            if (pptFile != null && pptFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, pptFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pptFile.CopyToAsync(stream);
                }

                // Convert PPT to HTML
            //    var htmlContent = ConvertPPTToHtml(filePath);

                // Pass HTML to the view
               // ViewData["HtmlContent"] = htmlContent;
                return View("DisplayPPTContents");
            }

            return BadRequest("No file uploaded or file is empty.");
        }

        //private string ConvertPPTToHtml(string filePath)
        //{
        //    StringBuilder html = new StringBuilder();

        //    // Open the .pptx file using NPOI
        //    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        // Create the PPTX document
        //        var pptDocument = XSLFSlideShowFactory.Create(fs);

        //        // Iterate through slides
        //        foreach (var slide in pptDocument.GetSlides())
        //        {
        //            html.Append("<div class='slide'>");
        //            html.Append($"<h2>Slide {(pptDocument.GetSlides().ToList().IndexOf(slide) + 1)}</h2>");

        //            // Iterate through shapes in each slide
        //            foreach (var shape in slide.GetShapes())
        //            {
        //                if (shape is XSLFTextShape textShape)
        //                {
        //                    string text = textShape.Text;

        //                    // Get text alignment
        //                    var alignment = textShape.TextAlign;
        //                    html.Append($"<p style='text-align: {GetTextAlign(alignment)};'>");

        //                    // Iterate through runs for formatting
        //                    foreach (var run in textShape.TextParagraphs)
        //                    {
        //                        foreach (var textRun in run.Runs)
        //                        {
        //                            string runText = textRun.GetRawText();

        //                            // Handle bold and italic text
        //                            if (textRun.IsBold)
        //                            {
        //                                html.Append("<strong>" + runText + "</strong>");
        //                            }
        //                            else if (textRun.IsItalic)
        //                            {
        //                                html.Append("<em>" + runText + "</em>");
        //                            }
        //                            else
        //                            {
        //                                html.Append(runText);
        //                            }
        //                        }
        //                    }

        //                    html.Append("</p>");
        //                }
        //                else if (shape is XSLFPictureShape pictureShape)
        //                {
        //                    var pictureData = pictureShape.GetPictureData();
        //                    var imgTag = $"<img src='data:{pictureData.GetImageType()};base64,{Convert.ToBase64String(pictureData.GetData())}' alt='Image' />";
        //                    html.Append(imgTag);
        //                }
        //            }
        //            html.Append("</div>");
        //        }
        //    }

        //    return html.ToString();
        //}

        public IActionResult DisplayPPTContents()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SelectExamType()
        {
            return View("FinalExamSelection");
        }

        [HttpPost]
        public IActionResult SelectExamType(string examType)
        {
            if (examType == "written")
            {
                return RedirectToAction("WrittenExamType");
            }
            else if (examType == "oral" || examType == "presentation")
            {
                // Redirect to oral or presentation view (not implemented here)
                return View("OralOrPresentationView");
            }

            return View("FinalExamSelection");
        }

        [HttpGet]
        [Route("Document/WrittenExamType")]
        public IActionResult SelectWrittenExamType()
        {
            return View("WrittenExamType");
        }
        // Written exam type selection (step 2)
        [HttpPost]
        public IActionResult SelectWrittenExamType(string writtenExamType)
        {
            // Based on the selected written exam type, redirect to the next step
            if (writtenExamType == "online" || writtenExamType == "onsite")
            {
                return RedirectToAction("ImportAndEditWord");
            }

            // If the selection is invalid, return to the same view with an error message
            ModelState.AddModelError("", "Please select either online or onsite.");
            return View("WrittenExamType");
        }

        // Show import and edit word view (step 3)
        [HttpGet]
        public IActionResult ImportAndEditWord()
        {
            return View();
        }

        // Handle Word file upload and display the content
        [HttpPost]
        public async Task<IActionResult> ImportWord(IFormFile wordFile)
        {
            if (wordFile != null && wordFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, wordFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await wordFile.CopyToAsync(stream);
                }

                // Convert Word to HTML for display
                string wordContent = ConvertWordToHtml(filePath);
                ViewData["WordContent"] = wordContent;

                return View("ImportAndEditWord");
            }

            return BadRequest("No file uploaded or file is empty.");
        }

        // Save the edited Word content
        [HttpPost]
        public IActionResult SaveWordContent(string editedContent)
        {
            // Here you would save the edited content, for now we'll just display a message.
            ViewData["Message"] = "Content saved successfully!";
            ViewData["WordContent"] = editedContent;  // Display the updated content

            return View("ImportAndEditWord");
        }

        [HttpPost]
        public async Task<IActionResult> ImportWordToTextarea(IFormFile wordFile)
        {
            if (wordFile != null && wordFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, wordFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await wordFile.CopyToAsync(stream);
                }

                // Convert Word file to HTML or plain text
                string wordContent = ConvertWordToHtml(filePath);

                // Pass the content to the view
                ViewData["WordContent"] = wordContent;

                // Return the view with the content populated in the textarea
                return View("WrittenExamType", ViewData["WordContent"]);
            
        }

            return BadRequest("No file uploaded or file is empty.");
        }

        // Save edited content
        [HttpPost]
        public IActionResult SaveEditedContent(string textareaContent)
        {
            // You can save the textareaContent here (for now, we are just sending it back)
            ViewData["SavedMessage"] = "Content saved successfully!";
            ViewData["WordContent"] = textareaContent;

            return View("WrittenExamType", ViewData["WordContent"]);
        }
        // POST: Document/ImportExcelToTextarea
        [HttpPost]
        public async Task<IActionResult> ImportExcelToTextarea(IFormFile excelFile)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, excelFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await excelFile.CopyToAsync(stream);
                }

                // Read the Excel file and convert it to string content
                string excelContent = ConvertExcelToString(filePath);

                // Pass the Excel content to ViewData
                ViewData["ExcelContent"] = excelContent;

                return View("WrittenExamType"); // Return to your current view
            }

            return BadRequest("No file uploaded or file is empty.");
        }

        private string ConvertExcelToString(string filePath)
        {
            StringBuilder tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1' style='border-collapse:collapse; width:100%;'>");

            // Load the workbook using NPOI
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs); // For .xlsx files
                ISheet sheet = workbook.GetSheetAt(0); // Get the first worksheet

                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; // Handle empty rows

                    tableBuilder.Append("<tr>"); // Start new row

                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        ICell cell = row.GetCell(j);

                        // Handle different types of cells
                        string cellValue = cell != null ? cell.ToString() : string.Empty;

                        // Append cell data in table cell
                        tableBuilder.Append($"<td style='padding: 8px; text-align: left;'>{cellValue}</td>");
                    }

                    tableBuilder.Append("</tr>"); // End the row
                }
            }

            tableBuilder.Append("</table>"); // End the table

            return tableBuilder.ToString();
        }

        [HttpGet]
        [Route("Document/Presentation")]
        public IActionResult Presentation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PresentationPW()
        {
            var htmlContent = ViewData["PptContent"] as string;

            ViewBag.HtmlContent = htmlContent;
            return View();
        }

        [HttpGet]
        public IActionResult PresentationP()
        {
            return View(); // Return the appropriate view for PresentationP
        }

        [HttpPost]
        public IActionResult HandlePresentation(bool hasContent)
        {
            if (hasContent)
            {
                return RedirectToAction("PresentationPW");
            }
            else
            {
                return RedirectToAction("PresentationP");
            }
        }

        // Import PowerPoint
        [HttpPost]
        public async Task<IActionResult> ImportPowerPoint(IFormFile pptFile)
        {
            if (pptFile != null && pptFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, pptFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pptFile.CopyToAsync(stream);
                }

                // Convert PowerPoint to HTML
                string htmlContent = ConvertPptToHtml(filePath);

                // Pass the HTML content to the view
                ViewData["PptContent"] = htmlContent;

                // Redirect to PresentationPW since it has content
                return View("PresentationPW");
            }

            return BadRequest("No file uploaded or the file is empty.");
        }

        private string ConvertPptToHtml(string filePath)
        {
            StringWriter writer = new StringWriter();
            writer.Write("<div style='width:100%;'>"); // Container for the slides

            // Load the PowerPoint file
            using (PresentationDocument presentationDocument = PresentationDocument.Open(filePath, false))
            {
                // Get the presentation part
                var presentationPart = presentationDocument.PresentationPart;

                // Loop through slides in the presentation
                foreach (SlideId slideId in presentationPart.Presentation.SlideIdList)
                {
                    // Get each slide
                    SlidePart slidePart = (SlidePart)presentationPart.GetPartById(slideId.RelationshipId);
                    writer.Write("<div style='margin: 20px; border: 1px solid #ccc; padding: 10px;'>");
                    writer.Write($"<h3>Slide {slideId.Id}</h3>"); // Slide number as title

                    // Iterate through all the shapes in the slide
                    foreach (var shape in slidePart.Slide.CommonSlideData.ShapeTree.ChildElements)
                    {
                        if (shape is DocumentFormat.OpenXml.Presentation.Shape textShape)
                        {
                            // Extract text from the shape
                            var text = GetShapeText(textShape);
                            if (!string.IsNullOrEmpty(text))
                            {
                                writer.Write("<p>" + text + "</p>");
                            }
                        }
                    }

                    writer.Write("</div>");
                }
            }

            writer.Write("</div>"); // End of container
            return writer.ToString();
        }

        private string GetShapeText(DocumentFormat.OpenXml.Presentation.Shape shape)
        {
            string shapeText = string.Empty;

            // Check if the shape has text body
            if (shape.TextBody != null)
            {
                foreach (var paragraph in shape.TextBody.ChildElements)
                {
                    if (paragraph is DocumentFormat.OpenXml.Drawing.Paragraph textParagraph)
                    {
                        foreach (var run in textParagraph.ChildElements)
                        {
                            if (run is DocumentFormat.OpenXml.Drawing.Run textRun)
                            {
                                shapeText += textRun.InnerText + " "; // Concatenate text from runs
                            }
                        }
                    }
                }
            }

            return shapeText.Trim(); // Return trimmed text
        }

        // Export PowerPoint (this is just a placeholder method)
        [HttpPost]
        public IActionResult ExportPowerPoint()
        {
            // Logic to export a PowerPoint file (if it's generated or edited)
            return File(new byte[0], "application/vnd.openxmlformats-officedocument.presentationml.presentation", "exported_presentation.pptx");
        }
    }
}


