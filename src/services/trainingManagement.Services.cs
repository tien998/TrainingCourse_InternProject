using Model;
using Newtonsoft.Json;
using TrainingCourseManagement.dTO;
using TrainingCourseManagement.DTO;
using UserServices;

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
        var schedule_DTOs = (from _classSchedule in _trainingDb!.ClassSchedule
                             where _classSchedule.ClassId == classId
                             select new ClassSchedule_DTO(_classSchedule)).ToArray();
        return schedule_DTOs!;
    }

    // This function get ClassShedule that a defind teacher was assigned
    public ClassSchedule_DTO[]? GetSchedule(string teacherId, string classId)
    {
        var schedule_DTOs = (from _classSchedule in _trainingDb!.ClassSchedule
                             where _classSchedule.ClassId == classId && _classSchedule.TeacherIDs!.Contains(teacherId)
                             select new ClassSchedule_DTO(_classSchedule)).ToArray();
        return schedule_DTOs!;
    }

    public SubjectDropdown_DTO[] GetSubject_DTOs()
    {
        SubjectDropdown_DTO[] subjectDropdown = (from _subject in _trainingDb!.Subject
                                                 select new SubjectDropdown_DTO(_subject)).ToArray();
        return subjectDropdown;
    }

    public ClassDropdown_DTO[] GetClass_DTOs()
    {
        ClassDropdown_DTO[] classDropdown = (from _class in _trainingDb!.Class
                                             select new ClassDropdown_DTO(_class)).ToArray();
        return classDropdown;
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

    // Subject
    public SubjectDTO[] GetSubjects()
    {
        var sbjArr = (from _subject in _trainingDb!.Subject
                      select new SubjectDTO(_subject)).ToArray();
        return sbjArr;
    }

    public void AddSubject(SubjectDTO dto)
    {
        Subject subject = new();
        DTO_Transaction.ToSubject(dto, subject);
        _trainingDb!.Subject.Add(subject);
        _trainingDb.SaveChanges();
    }

    public void EditSubject(SubjectDTO dto)
    {
        var subject = (from _subject in _trainingDb!.Subject
                       where _subject.Id == dto.Id
                       select _subject).FirstOrDefault();
        DTO_Transaction.ToSubject(dto, subject!);
        _trainingDb.SaveChanges();
    }

    public void DeleteSubject(string Id)
    {
        var subject = (from _subject in _trainingDb!.Subject
                       where _subject.Id == Id
                       select _subject).FirstOrDefault();
        _trainingDb.Remove(subject!);
        _trainingDb.SaveChanges();
    }

    // SubjectDepartment
    public SubjectDepartmentDTO[] GetSubjectDepartments()
    {
        var deprt = (from _subject in _trainingDb!.SubjectDepartment
                     select _subject).ToArray();
        var department = new SubjectDepartmentDTO[deprt.Length];
        for (int i = 0; i < deprt.Length; i++)
        {
            department[i] = new SubjectDepartmentDTO(deprt[i]);
        }
        return department;
    }

    public void AddSubjectDepartment(SubjectDepartmentDTO dto)
    {
        SubjectDepartment department = new();
        DTO_Transaction.ToSubjectDepartment(dto, department);
        _trainingDb!.SubjectDepartment.Add(department);
        _trainingDb.SaveChanges();
    }

    public void EditSubjectDepartment(SubjectDepartmentDTO dto)
    {
        var department = (from _department in _trainingDb!.SubjectDepartment
                          where _department.Id == dto.Id
                          select _department).FirstOrDefault();
        DTO_Transaction.ToSubjectDepartment(dto, department!);
        _trainingDb.SaveChanges();
    }

    public void DeleteSubjectDepartment(string id)
    {
        var department = (from _department in _trainingDb!.SubjectDepartment
                          where _department.Id == id
                          select _department).FirstOrDefault();
        _trainingDb.Remove(department!);
        _trainingDb.SaveChanges();
    }

    // Class
    public GetClassDTO[] GetClasses(int index, int take)
    {
        GetClassDTO[] classes = (from _classes in _trainingDb!.Class
                                 join _course in _trainingDb.Course on _classes.CourseId equals _course.Id
                                 join _trainingCourse in _trainingDb.TrainingCourse on _classes.TrainingCourseId equals _trainingCourse.Id
                                 select new GetClassDTO()
                                 {
                                     Id = _classes.Id,
                                     Name = _classes.Name,
                                     TotalStudent = _classes.TotalStudent,
                                     Status = _classes.Status,
                                     CourseName = _course.Name,
                                     TrainingCourseName = _trainingCourse.Name
                                 }
                            ).OrderBy(e => e.Id).Skip(index).Take(take).ToArray();
        return classes;
    }

    public void AddClass(AddClassDTO dto)
    {
        Class class_ = new();
        DTO_Transaction.ToClass(dto, class_);
        _trainingDb!.Class.Add(class_);
        _trainingDb.SaveChanges();
    }

    public void EditClass(AddClassDTO dto)
    {
        Class? class_ = (from clasS in _trainingDb!.Class
                         where clasS.Id == dto.Id
                         select clasS).FirstOrDefault();
        DTO_Transaction.ToClass(dto, class_!);
        _trainingDb!.SaveChanges();
    }

    public void DeleteClass(string id)
    {
        Class? class_ = (from clasS in _trainingDb!.Class
                         where clasS.Id == id
                         select clasS).FirstOrDefault();
        _trainingDb.Remove(class_!);
        _trainingDb.SaveChanges();
    }

    public async Task<string> GetStudents_byClass(string classId, UserManipulator userManipulator, HttpContext context)
    {
        var stIdArr = (from register_ in _trainingDb!.Register
                        where register_.ClassId == classId
                        select new string(register_.ClassId)).ToArray();
        string? idArrString = JsonConvert.SerializeObject(stIdArr);
        return await userManipulator.GetUsers(idArrString, Role.student, context);
    }

    public SubjectDTO[] GetSubject_byClass(string id)
    {
        var subjectArr = (from couresrSubject in _trainingDb!.CourseSubject
                            join clasS in _trainingDb.Class on couresrSubject.CourseId equals clasS.Id
                            join subject in _trainingDb.Subject on couresrSubject.SubjectId equals subject.Id
                            select new SubjectDTO(subject)).ToArray();
        return subjectArr;
    }

    public TrainingManipulator(TraniningDb traniningDb)
    {
        _trainingDb = traniningDb;
    }
}