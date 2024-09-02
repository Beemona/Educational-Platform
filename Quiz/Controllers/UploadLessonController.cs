//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using System.IO;
//using DocumentFormat.OpenXml.Packaging;
//using OpenXmlPowerTools;
//using System.Xml.Linq;
//using System.IO.Packaging;
//using System.Text;

//public class UploadLessonController : Controller
//{
//    [HttpPost]
//    public ActionResult UploadWordDocument(IFormFile file, int lessonId)
//    {
//        if (file != null && file.Length > 0)
//        {
//            // Convert the uploaded file to HTML
//            string htmlContent = ConvertDocxToHtml(file);

//            // Update the lesson with the converted HTML
//            UpdateLessonText(lessonId, htmlContent);

//            TempData["SuccessMessage"] = "Document uploaded and processed successfully!";
//            return RedirectToAction("Details", "Lessons", new { id = lessonId });
//        }

//        TempData["ErrorMessage"] = "File upload failed!";
//        return RedirectToAction("Details", "Lessons", new { id = lessonId });
//    }

//    private string ConvertDocxToHtml(IFormFile file)
//    {
//        // Create a temporary file path
//        string tempPath = Path.GetTempFileName();
//        try
//        {
//            // Save the uploaded file temporarily
//            using (var stream = new FileStream(tempPath, FileMode.Create))
//            {
//                file.CopyTo(stream);
//            }

//            // Open the DOCX file in read-only mode
//            using (WordprocessingDocument wDoc = WordprocessingDocument.Open(tempPath, false))
//            {
//                // Convert the DOCX to HTML
//                var html = HtmlConverter.ConvertToHtml(wDoc);

//                // Convert the XElement to a string
//                using (var writer = new StringWriter())
//                {
//                    html.WriteTo(writer);
//                    return writer.ToString();
//                }
//            }
//        }
//        finally
//        {
//            // Ensure the temporary file is cleaned up
//            if (File.Exists(tempPath))
//            {
//                File.Delete(tempPath);
//            }
//        }
//    }

//    private void UpdateLessonText(int lessonId, string htmlContent)
//    {
//        // Your logic for updating the lesson text in the database
//    }
//}
