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
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public TeacherDropdown_DTO(TeacherRs_DTO teacherRs_DTO)
    {
        Id = teacherRs_DTO.Id;
        FirstName = teacherRs_DTO.FirstName;
        LastName = teacherRs_DTO.LastName;
    }
}