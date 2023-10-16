
using TrainingCourse.DTO;
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
                    await userManipulator.GetUsers(index, take, RoleConventions.student, httpContext);
                });

                endpoints.MapPost("/register", (StudentRegister_DTO user, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                   
                });

                endpoints.MapPost("/edit", (StudentRs_DTO student, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    
                    
                });

                endpoints.MapGet("/delete/{userID}", (int userID, UserManipulator userManipulator, HttpContext httpContext) =>
                {
                    
                });
            });
        });
    }
}