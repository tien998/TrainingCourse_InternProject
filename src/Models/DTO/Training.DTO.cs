using Model;


namespace TrainingCourseManagement.DTO;

public class UserAuthenDTO
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class UserInfo
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public int Status { get; set; }
}

public class StudentRegisterDTO : UserBaseDTO, IStudentUser
{
    public string? Password { get; set; }
    public string? Parents { get; set; }
    public string? Avatar { get; set; }
}

public class StudentRsDTO : UserBaseDTO, IStudentUser
{
    public string? Id { get; set; }
    public string? Parents { get; set; }
}

public class TeacherRegisterDTO : UserBaseDTO, ITeacherUser
{
    public string? TaxIdentificationNumber { get; set; }
    public string? MajorSubject { get; set; }
    public string? MinorSubject { get; set; }
    public string? Password { get; set; }
}

public class TeacherRsDTO : UserBaseDTO, ITeacherUser
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

