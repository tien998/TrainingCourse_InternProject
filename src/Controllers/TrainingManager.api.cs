using TrainingCourseManagement.dTO;
using TrainingCourseManagement.DTO;
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

                //
                endpoint.MapGet("/GetClassSubjects/{classId}", async (string classId, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                                {
                                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                                    if (IsAuthor)
                                    {
                                        ClassSubjectDTO? classSubject = trainingManipulator.GetClassSubjects(classId);
                                        await context.Response.WriteAsJsonAsync(classSubject);
                                    }
                                });
            });
        });

        // ________________________________________________________________________________________________
        // TrainingCourse APIs
        app.Map("/trainingCourse", (app) =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/getAll/{index}/{take}", async (int index, int take, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        var trc = trainingManipulator.GetTrainingCourse(index, take);
                        await context.Response.WriteAsJsonAsync(trc);
                    }
                });
                endpoints.MapPost("/addNew", async (TrainingCourseDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.AddTrainingCourse(dto);
                    }
                });
                endpoints.MapPut("/edit", async (TrainingCourseDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.EditTrainingCourse(dto);
                    }
                });
                endpoints.MapDelete("/delete/{id}", async (string id, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.DeleteTrainingCourse(id);
                    }
                });
            });
        });

        // ________________________________________________________________________________________________
        // Subject APIs
        app.Map("/subject", (app) =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/getAll", async (UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        var subjectArr = trainingManipulator.GetSubjects();
                        await context.Response.WriteAsJsonAsync(subjectArr);
                    }
                });
                endpoints.MapPost("/addNew", async (SubjectDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.AddSubject(dto);
                    }
                });
                endpoints.MapPut("/edit", async (SubjectDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.EditSubject(dto);
                    }
                });
                endpoints.MapDelete("/delete/{id}", async (string id, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.DeleteSubject(id);
                    }
                });
            });
        });

        // ________________________________________________________________________________________________
        // SubjectDepartment APIs
        app.Map("/subjectDepartment", (app) =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/getAll", async (UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        var departmentArr = trainingManipulator.GetSubjectDepartments();
                        await context.Response.WriteAsJsonAsync(departmentArr);
                    }
                });
                endpoints.MapPost("/addNew", async (SubjectDepartmentDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.AddSubjectDepartment(dto);
                    }
                });
                endpoints.MapPut("/edit", async (SubjectDepartmentDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.EditSubjectDepartment(dto);
                    }
                });
                endpoints.MapDelete("/delete/{id}", async (string id, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.DeleteSubjectDepartment(id);
                    }
                });
            });
        });

        // ________________________________________________________________________________________________
        // Classes APIs
        app.Map("/Class", (app) =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/getAll/{index}/{take}", async (int index, int take, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        var classArr = trainingManipulator.GetClasses(index, take);
                        await context.Response.WriteAsJsonAsync(classArr);
                    }
                });
                endpoints.MapPost("/addNew", async (AddClassDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.AddClass(dto);
                    }
                });
                endpoints.MapPut("/edit", async (AddClassDTO dto, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.EditClass(dto);
                    }
                });
                endpoints.MapDelete("/delete/{id}", async (string id, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        trainingManipulator.DeleteClass(id);
                    }
                });
                endpoints.MapGet("/getStudentInClass/{classId}", async (string classId, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        string? studentJson = await trainingManipulator.GetStudents_inClass(classId, userManipulator, context);
                        await context.Response.WriteAsync(studentJson);
                    }
                });
                endpoints.MapGet("/getSubjectsInClass/{classId}", async (string classId, UserManipulator userManipulator, TrainingManipulator trainingManipulator, HttpContext context) =>
                {
                    bool IsAuthor = await userManipulator.IsAuthor(Role.sa, context);
                    if (IsAuthor)
                    {
                        SubjectDTO[] subjects = trainingManipulator.GetSubject_inClass(classId);
                        await context.Response.WriteAsJsonAsync(subjects);
                    }
                });
            });
        });
    }
}