using System.Security.Claims;

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