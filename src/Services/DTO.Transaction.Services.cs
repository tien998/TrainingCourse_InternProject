using Model;
using TrainingCourseManagement.DTO;

namespace TrainingCourseManagement.dTO;

public static class DTO_Transaction
{
    // ClassScheduleDTO is also TeachingAssignment
    public static void ToClassSchedule(ClassScheduleDTO dto, ClassSchedule classSchedule)
    {
        classSchedule.TeacherIDs = dto.TeacherIDs;
        classSchedule.ClassId = dto.ClassId;
        classSchedule.DaysOfWeek = dto.DaysOfWeek;
        classSchedule.StartTime = dto.StartTime;
        classSchedule.EndTime = dto.EndTime;
        classSchedule.StartDate = dto.StartDate;
        classSchedule.EndDate = dto.EndDate;
    }
    public static void ToSubject(SubjectDTO dto, Subject subject)
    {
        subject.Id = dto.Id;
        subject.Name = dto.Name;
        subject.DepartmentId = dto.DepartmentId;
    }

    public static void ToSubjectDepartment(SubjectDepartmentDTO dto, SubjectDepartment department)
    {
        department.Id = dto.Id;
        department.Name = dto.Name;
    }

    public static void ToClass(AddClassDTO dto, Class class_)
    {
        class_.Id = dto.Id;
        class_.Name = dto.Name;
        class_.TotalStudent = dto.TotalStudent;
        class_.Note = dto.Note;
        class_.Status = dto.Status;
        class_.ClassRoom = dto.ClassRoom;
        class_.CourseId = dto.CourseId;
        class_.TrainingCourseId = dto.TrainingCourseId;
    }
    
    public static void ToTrainingCourse(TrainingCourseDTO dto, TrainingCourse trainingCourse)
    {
        trainingCourse.Id = dto.Id;
        trainingCourse.Name = dto.Name;
        trainingCourse.StartDate = dto.StartDate;
        trainingCourse.EndDate = dto.EndDate;
    }
}