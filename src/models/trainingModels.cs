namespace Model;
public class Training
{
    public string? Id { get; set;}
    public string? Name { get; set;}
    public DateTime StartDate { get; set;}
    public DateTime EndDate { get; set;}
    public List<DepartmentPerTraining>? @departmentPerTraining{ get; set;}
}

public class SubjectDepartment
{
    public string? Id { get; set;}
    public string? Name { get; set;}
    public List<DepartmentPerTraining>? @departmentPerTraining{ get; set;}
    public List<Subject>? @subjects { get; set;}
}

public class DepartmentPerTraining
{
    public string? TrainingId { get; set;}
    public Training? @training{ get; set;}
    public string? DepartmentId { get; set;}
    public SubjectDepartment? @subjectDepartment{ get; set;}
}

public class Class
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? TrainingId { get; set; }
    public int TotalStudent { get; set; }
    public int CourseFee { get; set; }
    public int ActualFee { get; set; }
    public string? Note { get; set; }
    // StudyTime will contain JSON string that represent `Training.DTO.StudyTimeDTO[]` object
    public string? StudyTime { get; set; }
    public List<Register>? @register { get; set; }
    public List<TrainingProcess>? @trainingProcesses{ get; set; }
}

public class Register
{
    public string? UserId { get; set; }
    public string? ClassId { get; set; }
    public Class? @class{ get; set; }
}

public class Subject
{
    public string? Id { get; set;}
    public string? Name { get; set;}
    public string? DepartmentId {get; set; }
    public List<TrainingProcess>? @trainingProcesses {get; set;}
    public SubjectDepartment? @subjectDepartment{get; set;}
}

public class TrainingProcess
{
    public string? ClassId { get; set;}
    public Class? @class{ get; set; }
    public string? SubjectId { get; set;}
    public Subject? @subject{ get; set; }
}

