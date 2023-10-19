
using Model;
using Newtonsoft.Json;
using TrainingCourse.DTO;
using TrainingManagementServices;
using UserServices;

public static class StudentManagement
{
    public static void AddStudentManagementAPI(this WebApplication app)
    {
        app.Map("/student", app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // 'index' to Pagination, 'take' is number of item that need to take
                endpoints.MapGet("/getAll/{index}/{take}", async (int index, int take, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    await userManipulator.GetUsers(index, take, Role.student, httpContext);
                });

                endpoints.MapGet("/getUser/{id}", async (int id, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    string? rsJson = await userManipulator.Get(id, Role.student, httpContext);
                    await httpContext.Response.WriteAsync(rsJson!);
                });


                endpoints.MapPost("/register", async (StudentRegister_DTO user, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    await userManipulator.RegisterUsers<StudentRegister_DTO>(user, Role.student, httpContext);
                });

                endpoints.MapPost("/edit", async (StudentRs_DTO user, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    await userManipulator.EditUser<StudentRs_DTO>(user, Role.student, httpContext);
                });

                endpoints.MapDelete("/delete/{userID}", async (int userID, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    await userManipulator.DeleteUser(userID, Role.student, httpContext);
                });


                // training management area
                // RegisterClass & RegisterClassCancel APIs has struture of http Body as AnonymusType { UserId = 0, ClassId = "" }
                // only SA user can register & register cancel class & study fee collection for student
                endpoints.MapGet("/getSchedule/{userId}/{classId}", async (int userId, string classId, TrainingManipulator training, UserManipulator userManipulator, HttpContext context) =>
                {
                    bool IsAuthor_sa_student = await userManipulator.IsAuthors(Role.sa, Role.student, context);
                    if (IsAuthor_sa_student)
                    {
                        await context.Response.WriteAsJsonAsync(training.GetSchedule(userId, classId)!);
                    }
                });

                endpoints.MapPost("/RegisterClass", async (TrainingManipulator training, UserManipulator userManipulator, HttpContext context) =>
                {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                        using (StreamReader reader = new(context.Request.Body))
                        {
                            string? jsonRs = await reader.ReadToEndAsync();
                            var register = JsonConvert.DeserializeAnonymousType(jsonRs, new { UserId = 0, ClassId = "" });
                            training.RegisterClass_student(register!.UserId, register!.ClassId!);
                        }
                    }
                });
                endpoints.MapGet("/GetClasses/{userId}", async (int userId, TrainingManipulator training, UserManipulator userManipulator, HttpContext context) =>
                {
                    bool IsAuthor_sa_student = await userManipulator.IsAuthors(Role.sa, Role.student, context);
                    if (IsAuthor_sa_student)
                    {
                        await context.Response.WriteAsJsonAsync(training.GetClasses_student(userId));
                    }
                });
                endpoints.MapPost("/RegisterClassCancel", async (TrainingManipulator training, UserManipulator userManipulator, HttpContext context) =>
                {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                        using (StreamReader reader = new(context.Request.Body))
                        {
                            string? jsonRs = await reader.ReadToEndAsync();
                            var register = JsonConvert.DeserializeAnonymousType(jsonRs, new { UserId = 0, ClassId = "" });
                            training.RegisterClassCancel(register!.UserId, register.ClassId!);
                        }
                    }
                });
                endpoints.MapPost("/FeeCollection", async (TrainingManipulator training, UserManipulator userManipulator, HttpContext context) =>
                {
                    bool IsAuthor_sa = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor_sa)
                    {
                        using (StreamReader reader = new(context.Request.Body))
                        {
                            string? jsonRs = await reader.ReadToEndAsync();
                            var register = JsonConvert.DeserializeObject<Register>(jsonRs);
                            training.FeeCollection(register!);
                        }
                    }
                });
            });
        });
    }
}