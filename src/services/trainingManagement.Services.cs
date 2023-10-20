using Model;
using Newtonsoft.Json;
using TrainingCourse.DTO;

namespace TrainingManagementServices;

public class TrainingManipulator
{
    TraniningDb? _trainingDb;
    string? _idenBaseURL;

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

    public Subject_DTO[] GetSubject_DTOs()
    {
        Subject[] subjects = (from _subject in _trainingDb!.Subject
                                    select _subject).ToArray();
        var dtoArr = new Subject_DTO[subjects.Length];
        for (int i = 0; i < subjects.Length; i++)
        {
            dtoArr[i] = new Subject_DTO(subjects[i]);
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

    // This function is not recommend to use. The Client should consider call api direct from the Identity_Service_Project for quickly performence
    public async Task<TeacherDropdown_DTO[]> GetTeacher_DTO()
    {
        string url = _idenBaseURL! + $"/{Role.teacher}/GetAll";
        using (HttpClient client = new())
        {
            using (HttpRequestMessage requestMessage = new(HttpMethod.Get, url))
            {
                var rs = await client.SendAsync(requestMessage);
                string? rsJson = await rs.Content.ReadAsStringAsync();
                TeacherRs_DTO[] rs_DTO_Arr = JsonConvert.DeserializeObject<TeacherRs_DTO[]>(rsJson)!;
                var dtoArr = new TeacherDropdown_DTO[rs_DTO_Arr.Length];
                for (int i = 0; i < rs_DTO_Arr.Length; i++)
                {
                    dtoArr[i] = new TeacherDropdown_DTO(rs_DTO_Arr[i]);
                }
                return dtoArr;
            }
        }
    }
    
    public void FeeCollection(Register register)
    {
        var fee = (from _register in _trainingDb!.Register
                   where _register.UserId == register.UserId && _register.ClassId == register.ClassId
                   select _register).FirstOrDefault();
        fee = register;
        _trainingDb.SaveChanges();
    }

    public TrainingManipulator(TraniningDb traniningDb, IConfiguration root)
    {
        _trainingDb = traniningDb;
        _idenBaseURL = root.GetSection("ApiUrl").GetSection("IdentityService").Value;
    }
}