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

    public ClassSchedule[]? GetSchedule(string classId)
    {
        var studyTime = (from _classSchedule in _trainingDb!.ClassSchedule 
                             where _classSchedule.ClassId == classId 
                             select _classSchedule).ToArray();;
        return studyTime;
    }

    // This function get ClassShedule that a defind teacher was assigned
    public ClassSchedule[]? GetSchedule(string teacherId, string classId)
    {
        var studyTime = (from _classSchedule in _trainingDb!.ClassSchedule 
                             where _classSchedule.ClassId == classId && _classSchedule.TeacherIDs!.Contains(teacherId)
                             select _classSchedule).ToArray();
        return studyTime!;
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