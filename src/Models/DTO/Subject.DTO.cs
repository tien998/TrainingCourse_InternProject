using Model;

namespace TrainingCourseManagement.DTO;
public class SubjectDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? DepartmentId { get; set; }
    public SubjectDTO(Subject subject)
    {
        Id = subject.Id;
        Name = subject.Name;
        DepartmentId = subject.DepartmentId;
    }
}

public class SubjectDepartmentDTO
{
    public string? Id {get;set;}
    public string? Name {get;set;}
    public SubjectDepartmentDTO(SubjectDepartment department)
    {
        Id = department.Id;
        Name = department.Name;
    }
}
