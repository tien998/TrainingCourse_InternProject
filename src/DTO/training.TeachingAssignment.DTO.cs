using Model;

namespace TrainingCourse.DTO;

// Equal to Model.ClassSchedule
public class TeachingAssignment_DTO
{
    public string? TeacherIDs { get; set; }
    public string? ClassId { get; set; }
    public string? DaysOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class Subject_DTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public Subject_DTO() { }
    public Subject_DTO(Subject subject)
    {
        Id = subject.Id;
        Name = subject.Name;
    }
}

public class Class_DTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public Class_DTO() { }
    public Class_DTO(Class cls)
    {
        Id = cls.Id;
        Name = cls.Id;
    }
}