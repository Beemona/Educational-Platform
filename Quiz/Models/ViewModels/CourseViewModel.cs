// ViewModel for CourseLessons
using Lesson.Models;

public class CourseLessonsViewModel
{
    public Subject? Subject { get; set; }
    public IEnumerable<LessonCard>? Lessons { get; set; }
}

