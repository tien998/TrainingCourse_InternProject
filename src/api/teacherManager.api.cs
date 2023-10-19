
using TrainingCourse.DTO;
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
                endpoints.MapGet("/getSchedule/{userId}/{class}", (TrainingManipulator training, HttpContext context) => {
                    // training.GetSchedule()
                });
            });
        });
    }
}