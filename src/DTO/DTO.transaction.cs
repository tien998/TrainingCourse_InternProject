using Model;

namespace TrainingCourse.DTO;

public static class DTO_Transaction
{
    // ClassSchedule_DTO is also TeachingAssignment
    public static void ToClassSchedule(ClassSchedule_DTO dto, ClassSchedule classSchedule)
    {
        classSchedule.TeacherIDs = dto.TeacherIDs;
        classSchedule.ClassId = dto.ClassId;
        classSchedule.DaysOfWeek = dto.DaysOfWeek;
        classSchedule.StartTime = dto.StartTime;
        classSchedule.EndTime = dto.EndTime;
        classSchedule.StartDate = dto.StartDate;
        classSchedule.EndDate = dto.EndDate;
    }
}