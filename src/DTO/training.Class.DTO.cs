using Model;

namespace TrainingCourseManagement.dTO;

public class GetClassDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int TotalStudent { get; set; }
    public int Status { get; set; }
    public string? CourseName { get; set; }
    public string? TrainingCourseName { get; set; }
}

public class AddClassDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int TotalStudent { get; set; }
    public string? Note { get; set; }
    public int Status { get; set; }
    public string? ClassRoom { get; set; }
    public int ClassFee { get; set; }
    public string? CourseId { get; set; }
    public string? TrainingCourseId { get; set; }
    public string? Img { get; set; }
}
