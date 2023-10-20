using Model;
using TrainingCourse.DTO;

namespace TrainingManagementServices;

public class TrainingManipulator
{
    TraniningDb? _trainingDb;

    public void RegisterClass_student(int userId, string classId)
    {
        Register registerObj = new()
        {
            UserId = userId,
            ClassId = classId
        };
        _trainingDb!.Register.Add(registerObj);
        _trainingDb!.SaveChanges();
    }

    public Class[] GetClasses_student(int userId)
    {
        Class[] classes = (from register in _trainingDb!.Register
                           join _class in _trainingDb.Class
                           on register.ClassId equals _class.Id
                           select _class).ToArray();
        return classes;
    }

    public void RegisterClassCancel(int userId, string classId)
    {
        Register? register = (from _register in _trainingDb!.Register
                              where _register.UserId == userId && _register.ClassId == classId
                              select _register).FirstOrDefault();
        _trainingDb.Remove(register!);
        _trainingDb.SaveChanges();
    }

    public ClassSchedule_DTO[]? GetSchedule(string classId)
    {
        var studyTime = (from _classSchedule in _trainingDb!.ClassSchedule
                         where _classSchedule.ClassId == classId
                         select _classSchedule).ToArray();
        ClassSchedule_DTO[] schedule_DTOs = new ClassSchedule_DTO[studyTime.Length];
        for (int i = 0; i < studyTime.Length; i++)
        {
            schedule_DTOs[i] = new ClassSchedule_DTO(studyTime[i]);
        }
        return schedule_DTOs!;
    }

    // This function get ClassShedule that a defind teacher was assigned
    public ClassSchedule_DTO[]? GetSchedule(string teacherId, string classId)
    {
        var studyTime = (from _classSchedule in _trainingDb!.ClassSchedule
                         where _classSchedule.ClassId == classId && _classSchedule.TeacherIDs!.Contains(teacherId)
                         select _classSchedule).ToArray();
        ClassSchedule_DTO[] schedule_DTOs = new ClassSchedule_DTO[studyTime.Length];
        for (int i = 0; i < studyTime.Length; i++)
        {
            schedule_DTOs[i] = new ClassSchedule_DTO(studyTime[i]);
        }
        return schedule_DTOs!;
    }

    public SubjectDropdown_DTO[] GetSubject_DTOs()
    {
        Subject[] subjects = (from _subject in _trainingDb!.Subject
                              select _subject).ToArray();
        var dtoArr = new SubjectDropdown_DTO[subjects.Length];
        for (int i = 0; i < subjects.Length; i++)
        {
            dtoArr[i] = new SubjectDropdown_DTO(subjects[i]);
        }
        return dtoArr;
    }

    public ClassDropdown_DTO[] GetClass_DTOs()
    {
        Class[] classes = (from _class in _trainingDb!.Class
                           select _class).ToArray();
        var dtoArr = new ClassDropdown_DTO[classes.Length];
        for (int i = 0; i < classes.Length; i++)
        {
            dtoArr[i] = new ClassDropdown_DTO(classes[i]);
        }
        return dtoArr;
    }

    // ClassSchedule_DTO is also TeachingAssignment
    public void AssignTeacher(ClassSchedule_DTO dto)
    {
        ClassSchedule? classSchedule = new();
        DTO_Transaction.ToClassSchedule(dto, classSchedule);
        _trainingDb!.ClassSchedule.Add(classSchedule);
        _trainingDb.SaveChanges();
    }

    // ClassSchedule_DTO is also TeachingAssignment
    public void ReAssignTeacher(ClassSchedule_DTO dto)
    {
        var classSchedule = (from _schedule in _trainingDb!.ClassSchedule
                             where _schedule.TeacherIDs == dto.TeacherIDs && _schedule.ClassId == dto.ClassId
                             select _schedule).FirstOrDefault();
        DTO_Transaction.ToClassSchedule(dto, classSchedule!);
        _trainingDb.SaveChanges();
    }

    public void DeleteSchedule(string teacherIDs, string classId)
    {
        var classSchedule = (from _schedule in _trainingDb!.ClassSchedule
                             where _schedule.TeacherIDs == teacherIDs && _schedule.ClassId == classId
                             select _schedule).FirstOrDefault();
        _trainingDb.ClassSchedule.Remove(classSchedule!);
        _trainingDb.SaveChanges();
    }

    public void FeeCollection(Register register)
    {
        var fee = (from _register in _trainingDb!.Register
                   where _register.UserId == register.UserId && _register.ClassId == register.ClassId
                   select _register).FirstOrDefault();
        fee = register;
        _trainingDb.SaveChanges();
    }

    public TrainingManipulator(TraniningDb traniningDb)
    {
        _trainingDb = traniningDb;
    }
}