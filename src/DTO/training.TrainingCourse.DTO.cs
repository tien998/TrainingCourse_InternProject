using Model;

namespace TrainingCourseManagement.DTO;

public class TrainingCourseDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TrainingCourseDTO(TrainingCourse trainingCourse)
    {
        Id = trainingCourse.Id;
        Name = trainingCourse.Name;
        StartDate = trainingCourse.StartDate;
        EndDate = trainingCourse.EndDate;
    }
}