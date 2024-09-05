using System.ComponentModel.DataAnnotations;

namespace Lesson.Models { 
public class LessonProgressModel
{
    public int LessonId { get; set; }
    public bool IsFinished { get; set; }
}
}