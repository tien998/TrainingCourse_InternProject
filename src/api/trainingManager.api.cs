using TrainingCourse.DTO;
using TrainingManagementServices;
using UserServices;

public static class TrainingManagement
{
    public static void AddTrainingManagementAPI(this WebApplication app)
    {
        app.Map("/training", app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoint =>
            {
                // APIs for dropdown box in Client (Teaching Assignment)
                endpoint.MapGet("/GetSubjects", async (UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        SubjectDropdown_DTO[]? subject_DTOs = trainingManipulator.GetSubject_DTOs();
                        await context.Response.WriteAsJsonAsync(subject_DTOs);
                    }
                });
                endpoint.MapGet("/GetClasses", async (UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        ClassDropdown_DTO[]? class_DTOs = trainingManipulator.GetClass_DTOs();
                        await context.Response.WriteAsJsonAsync(class_DTOs);
                    }
                });

                // This API is not recommend to use. The Client should consider call api direct from the Identity_Service_Project for quickly performence
                // api: http://localhost:5024/teacher/GetAll/dropdown
                endpoint.MapGet("/GetTeachers", async (UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    string? subject_DTOs = await userManipulator.GetTeachers_DropdownDTO(context);
                    await context.Response.WriteAsync(subject_DTOs);
                });

                // After have defind values, Use this APIs to Assign teacher to class
                endpoint.MapPost("/AssignTeacher", async (TeachingAssignment_DTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) => {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                       trainingManipulator.AssignTeacher(dto);
                    }
                });

                endpoint.MapPut("/ReassignTeacher", async (TeachingAssignment_DTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) => {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                       trainingManipulator.ReAssignTeacher(dto);
                    }
                });

                endpoint.MapDelete("/DeleteSchedule/{teacherIDs}/{classId}", async (string teacherIDs, string classId, TeachingAssignment_DTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) => {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                       trainingManipulator.DeleteSchedule(teacherIDs, classId);
                    }
                });
            });
        });
    }
}