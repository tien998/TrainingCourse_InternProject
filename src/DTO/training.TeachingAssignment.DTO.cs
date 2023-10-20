using Model;

namespace TrainingCourse.DTO;

// Equal to Model.ClassSchedule
public class ClassSchedule_DTO
{
    public string? TeacherIDs { get; set; }
    public string? ClassId { get; set; }
    public string? DaysOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ClassSchedule_DTO() { }
    public ClassSchedule_DTO(ClassSchedule schedule)
    {
        TeacherIDs = schedule.TeacherIDs;
        ClassId = schedule.ClassId;
        DaysOfWeek = schedule.DaysOfWeek;
        StartTime = schedule.StartTime;
        EndTime = schedule.EndTime;
        StartDate = schedule.StartDate;
        EndDate = schedule.EndDate;
    }
}

public class SubjectDropdown_DTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public SubjectDropdown_DTO() { }
    public SubjectDropdown_DTO(Subject subject)
    {
        Id = subject.Id;
        Name = subject.Name;
    }
}

public class ClassDropdown_DTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public ClassDropdown_DTO() { }
    public ClassDropdown_DTO(Class cls)
    {
        Id = cls.Id;
        Name = cls.Id;
    }
}

public class TeacherDropdown_DTO
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}