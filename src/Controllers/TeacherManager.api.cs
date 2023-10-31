
using TrainingCourseManagement.DTO;
using TrainingManagementServices;
using UserServices;

public static class TeacherManagement
{
    public static void AddTeacherManagementAPI(this WebApplication app)
    {
        app.Map("/teacher", app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // 'index' to Pagination, 'take' is number of item that need to take
                endpoints.MapGet("/getAll/{index}/{take}", async (int index, int take, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    string rsJson = await userManipulator.GetUsers(index, take, Role.teacher, httpContext);
                    await httpContext.Response.WriteAsync(rsJson);
                });

                endpoints.MapGet("/getUser/{id}", async (int id, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    string? rsJson = await userManipulator.Get(id, Role.teacher, httpContext);
                    await httpContext.Response.WriteAsync(rsJson!);
                });

                endpoints.MapPost("/register", async (TeacherRegister_DTO user, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    await userManipulator.RegisterUsers<TeacherRegister_DTO>(user, Role.teacher, httpContext);
                });

                endpoints.MapPost("/edit", async (TeacherRs_DTO user, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    await userManipulator.EditUser<TeacherRs_DTO>(user, Role.teacher, httpContext);
                });

                endpoints.MapDelete("/delete/{userID}", async (int userID, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    await userManipulator.DeleteUser(userID, Role.teacher, httpContext);
                });


                // training management area
                endpoints.MapGet("/getSchedule/{teacherIDs}/{classId}", async (string teacherIDs, string classId, UserManipulator userManipulator, TrainingManipulator training, HttpContext context) =>
                {
                    bool IsAuthor_sa_teacher = await userManipulator.IsAuthors(Role.sa, Role.teacher, context);
                    if (IsAuthor_sa_teacher)
                    {
                        training.GetSchedule(teacherIDs, classId);
                    }
                });

                // After have defind values, Use this APIs to Assign teacher to class
                endpoints.MapPost("/AssignTeacher", async (ClassSchedule_DTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                        trainingManipulator.AssignTeacher(dto);
                    }
                });

                endpoints.MapPut("/ReassignTeacher", async (ClassSchedule_DTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                        trainingManipulator.ReAssignTeacher(dto);
                    }
                });

                endpoints.MapDelete("/DeleteSchedule/{teacherIDs}/{classId}", async (string teacherIDs, string classId, ClassSchedule_DTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
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