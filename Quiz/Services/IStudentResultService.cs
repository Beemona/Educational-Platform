using QuestionModel.Models;
using StudentModel.Models;
using System.Collections.Generic;

public interface IStudentResultService
{
    // Method to get all student results
    List<StudentResult> GetAllResults();

    // Method to save a student result
    void SaveStudentResult(StudentResult studentResult);

    Task SaveStudentResultAsync(StudentResult studentResult);
}
