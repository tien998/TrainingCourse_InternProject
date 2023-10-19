namespace Model;
public class Training
{
    public string? Id { get; set;}
    public string? Name { get; set;}
    public DateTime StartDate { get; set;}
    public DateTime EndDate { get; set;}
    public List<DepartmentPerTraining>? _departmentPerTraining{ get; set;}
}

public class SubjectDepartment
{
    public string? Id { get; set;}
    public string? Name { get; set;}
    public List<DepartmentPerTraining>? _departmentPerTraining{ get; set;}
    public List<Subject>? _subjects { get; set;}
}

public class DepartmentPerTraining
{
    public string? TrainingId { get; set;}
    public Training? _training{ get; set;}
    public string? DepartmentId { get; set;}
    public SubjectDepartment? _subjectDepartment{ get; set;}
}

public class Class
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? TrainingId { get; set; }
    public int TotalStudent { get; set; }
    public int CourseFee { get; set; }
    public string? Note { get; set; }
    // StudyTime will contain JSON string that represent `Training.DTO.StudyTimeDTO[]` object
    public string? StudyTime { get; set; }
    public List<Register>? _register { get; set; }
    public List<ClassSchedule>? _classSchedule { get; set; }
    public List<TrainingProcess>? _trainingProcesses{ get; set; }
}

public class Register
{
    public int UserId { get; set; }
    public string? ClassId { get; set; }
    public Class? _class{ get; set; }
    public int DisCount {get; set;}
    public int ActualFee {get; set;}
}

// TrainingCourse.DTO.StudyTimeDTO
public class ClassSchedule
{
    public int TeacherUserId {get;set;}
    public string? ClassId { get; set; }
    // DaysOfWeek is a JSON String contain an int[] represent days of week
    public string? DaysOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Class? _class {get;set;}
}

public class Subject
{
    public string? Id { get; set;}
    public string? Name { get; set;}
    public string? DepartmentId {get; set; }
    public List<TrainingProcess>? _trainingProcesses {get; set;}
    public SubjectDepartment? _subjectDepartment{get; set;}
}

public class TrainingProcess
{
    public string? ClassId { get; set;}
    public Class? _class{ get; set; }
    public string? SubjectId { get; set;}
    public Subject? _subject{ get; set; }
}

