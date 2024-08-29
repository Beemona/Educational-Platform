using Microsoft.EntityFrameworkCore;
using QuizDbContext.Data;
using StudentModel.Models;
using System.Collections.Generic;
using System.Linq;

public class StudentResultService : IStudentResultService
{
    private readonly ApplicationDbContext _context;

    public StudentResultService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<StudentResult> GetAllResults()
    {
        var results = _context.StudentResults
            .Include(sr => sr.ResultDetails)
            .ToList();

        // Log or debug output
        foreach (var result in results)
        {
            Console.WriteLine($"Student: {result.StudentName}, Results Count: {result.ResultDetails?.Count}");
        }

        return results;
    }


    public void SaveStudentResult(StudentResult studentResult)
    {
        if (studentResult != null &&
            !string.IsNullOrEmpty(studentResult.StudentName) &&
            studentResult.ResultDetails != null &&
            studentResult.ResultDetails.All(r => !string.IsNullOrEmpty(r.QuestionText) && r.Points.HasValue))
        {
            // Ensure each QuestionResult has the correct StudentResultId
            foreach (var resultDetail in studentResult.ResultDetails)
            {
                resultDetail.StudentResultId = studentResult.Id;
            }

            _context.StudentResults.Add(studentResult);
            _context.SaveChanges(); // Persist changes to the database
        }
        else
        {
            throw new ArgumentException("Invalid student result");
        }
    }
    public async Task SaveStudentResultAsync(StudentResult studentResult)
    {
        if (studentResult != null &&
            !string.IsNullOrEmpty(studentResult.StudentName) &&
            studentResult.ResultDetails != null &&
            studentResult.ResultDetails.All(r => r.QuestionText != null))
        {
            _context.StudentResults.Add(studentResult);
            await _context.SaveChangesAsync(); // Persist changes to the database
        }
        else
        {
            // Handle the case where the studentResult is invalid
            // Log the error or throw an exception based on your needs
            throw new ArgumentException("Invalid student result");
        }
    }

}



//using QuestionModel.Models;
//using StudentModel.Models;
//using System.Collections.Generic;

//public class StudentResultService : IStudentResultService
//{
//    private readonly List<StudentResult> _studentResults = new List<StudentResult>();

//    public List<StudentResult> GetAllResults()
//    {
//        return _studentResults;
//    }

//    public void SaveStudentResult(StudentResult studentResult)
//    {
//        // Validate the studentResult before adding it
//        if (studentResult != null && !string.IsNullOrEmpty(studentResult.StudentName) && studentResult.ResultDetails != null)
//        {
//            _studentResults.Add(studentResult);
//        }
//        else
//        {
//            // Handle the case where the studentResult is invalid
//            // Log the error or throw an exception based on your needs
//            throw new ArgumentException("Invalid student result");
//        }
//    }
//}


