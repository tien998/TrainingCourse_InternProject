namespace Model;
public class TrainingYear
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<DepartmentPerTrainingYear>? _departmentPerTraining { get; set; }
}

public class SubjectDepartment
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<DepartmentPerTrainingYear>? _departmentPerTraining { get; set; }
    public List<Subject>? _subjects { get; set; }
}

public class DepartmentPerTrainingYear
{
    public string? TrainingYearId { get; set; }
    public TrainingYear? _trainingYear { get; set; }
    public string? DepartmentId { get; set; }
    public SubjectDepartment? _subjectDepartment { get; set; }
}

public class Course
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? TrainingYearId { get; set; }
    public int CourseFee { get; set; }
    public List<CourseSubject>? _courseSubject { get; set; }
    public List<Class>? _class { get; set; }
}

public class Class
{
    public string? Id { get; set; }
    public int TotalStudent { get; set; }
    public string? Note { get; set; }
    // Convention in TrainingCourse.DTO.ClassStatus
    public int Status { get; set; }
    public string? CourseId { get; set; }
    public string? TrainingYearId { get; set; }
    public Course? _course { get; set; }
    public List<Register>? _register { get; set; }
    public List<ClassSchedule>? _classSchedule { get; set; }

}

public class Register
{
    public int UserId { get; set; }
    public string? ClassId { get; set; }
    public Class? _class { get; set; }
    public int DisCount { get; set; }
    public int ActualFee { get; set; }
}

// TrainingCourse.DTO.ClassScheduleDTO
public class ClassSchedule
{
    // TeacherIDs is a JSON string that represent an array int[] contain teacherIDs (userId)
    public string? TeacherIDs { get; set; }
    public string? ClassId { get; set; }
    // DaysOfWeek is a JSON String contain an int[] represent days of week
    public string? DaysOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    // Convention in TrainingCourse.DTO.ClassStatus
    public int Status { get; set; }
    public Class? _class { get; set; }
}

public class Subject
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? DepartmentId { get; set; }
    public List<CourseSubject>? _courseSubject { get; set; }
    public SubjectDepartment? _subjectDepartment { get; set; }
}

public class CourseSubject
{
    public string? CourseId { get; set; }
    public Course? _course { get; set; }
    public string? SubjectId { get; set; }
    public Subject? _subject { get; set; }
}

