using Model;

namespace TrainingCourseManagement.DTO;

// Equal to Model.ClassSchedule
public class ClassScheduleDTO
{
    public string? TeacherIDs { get; set; }
    public string? ClassId { get; set; }
    public string? DaysOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ClassScheduleDTO() { }
    public ClassScheduleDTO(ClassSchedule schedule)
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

public class SubjectDropdownDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public SubjectDropdownDTO() { }
    public SubjectDropdownDTO(Subject subject)
    {
        Id = subject.Id;
        Name = subject.Name;
    }
}

public class ClassDropdownDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public ClassDropdownDTO() { }
    public ClassDropdownDTO(Class cls)
    {
        Id = cls.Id;
        Name = cls.Id;
    }
}

public class TeacherDropdownDTO
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}