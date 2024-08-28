using QuestionModel.Models;
using StudentModel.Models;
using System.Collections.Generic;

public class StudentResultService : IStudentResultService
{
    private readonly List<StudentResult> _studentResults = new List<StudentResult>();

    public List<StudentResult> GetAllResults()
    {
        return _studentResults;
    }

    public void SaveStudentResult(StudentResult studentResult)
    {
        // Validate the studentResult before adding it
        if (studentResult != null && !string.IsNullOrEmpty(studentResult.StudentName) && studentResult.ResultDetails != null)
        {
            _studentResults.Add(studentResult);
        }
        else
        {
            // Handle the case where the studentResult is invalid
            // Log the error or throw an exception based on your needs
            throw new ArgumentException("Invalid student result");
        }
    }
}

//public class StudentResultService : IStudentResultService
//{
//    private readonly List<StudentResult> _studentResults = new List<StudentResult>();

//    public List<StudentResult> GetAllResults()
//    {
//        return _studentResults;
//    }

//    public void SaveStudentResult(StudentResult studentResult)
//    {
//        _studentResults.Add(studentResult);
//    }
//}


