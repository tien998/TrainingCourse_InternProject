using System.Security.Claims;
using Model;


namespace TrainingCourse.DTO;

public class User_AuthenDTO
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class StudentRegister_DTO : UserBaseDTO, IStudentUser
{
    public string? Password { get; set; }
    public string? Parents { get; set; }
    public string? Avatar { get; set; }
}

public class StudentRs_DTO : UserBaseDTO, IStudentUser
{
    public string? Id { get; set; }
    public string? Parents { get; set; }
}

public class TeacherRegister_DTO : UserBaseDTO, ITeacherUser
{
    public string? TaxIdentificationNumber { get; set; }
    public string? MajorSubject { get; set; }
    public string? MinorSubject { get; set; }
    public string? Password { get; set; }
}

public class TeacherRs_DTO : UserBaseDTO, ITeacherUser
{
    public string? Id { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public string? MajorSubject { get; set; }
    public string? MinorSubject { get; set; }
}
public class EmailDTO
{
    public string? EmailAddress { get; set; }
}

public class ResetPassDTO
{
    public string? GUID { get; set; }
    public int UserID { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public static class JwtPayloadConst
{
    public const string userID = "userID";
    public const string role = ClaimTypes.Role;
    public const string iss = "iss";
    public const string aud = "aud";
}

public static class Role
{
    public const string sa = "sa";
    public const string teacher = "teacher";
    public const string student = "student";
}

public enum DaysOfWeek
{
    Monday = 2,
    Tuesday = 3,
    Wednesday = 4,
    Thursday = 5,
    Friday = 6,
    Saturday = 7,
    Sunday = 0,
}

public enum ClassStatus
{
    InEnrollment = 1,
    InProcess = 2,
    Done = 3
}