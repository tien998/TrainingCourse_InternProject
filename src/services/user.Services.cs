using System.Net;
using System.Text;
using Newtonsoft.Json;
using TrainingCourse.DTO;

namespace UserServices;

public class UserManipulator
{
    TraniningDb _traniningDb;
    string? _idenBaseURL;
    /// <summary>
    /// This function call API from Identity project
    /// </summary>
    /// <typeparam name="T">UserRsDTO</typeparam>
    /// <param name="role">Get role from "TrainingCourse.DTO.RoleConventions"</param>
    /// <returns></returns>
    public async Task GetUsers(int index, int take, string role, HttpContext httpContext)
    {
        string jwtBearer = httpContext.Request.Headers["Authorization"].ToString();
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", jwtBearer);
            var url = _idenBaseURL + $"/{role}/Getall/{index}/{take}";
            using (HttpRequestMessage requestMessage = new(HttpMethod.Get, url))
            {
                var rs = client.Send(requestMessage);
                var rsJson = await rs.Content.ReadAsStringAsync();
                httpContext.Response.StatusCode = (int)rs.StatusCode;
                await httpContext.Response.WriteAsync(rsJson);
            }
        }
    }

    public async Task RegisterUsers(StudentRegister_DTO user, string role, HttpContext httpContext)
    {
        string jwtBearer = httpContext.Request.Headers["Authorization"].ToString();
        string jsonData = JsonConvert.SerializeObject(user);
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", jwtBearer);
            var url = _idenBaseURL + $"/{role}/register";
            using (HttpRequestMessage requestMessage = new(HttpMethod.Post, url))
            {
                using (HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                {
                    requestMessage.Content = httpContent;
                    var rs = client.Send(requestMessage);
                    var rsJson = await rs.Content.ReadAsStringAsync();
                    httpContext.Response.StatusCode = (int)rs.StatusCode;
                    await httpContext.Response.WriteAsync(rsJson);
                }

            }
        }
    }
    // public Task EditStudent(StudentRs_DTO student)
    // {

    // }

    // public async Task<TeacherRs_DTO[]> GetTeachers(int index, int take)
    // {

    // }

    // public Task EditTeacher(TeacherRs_DTO teacher)
    // {

    // }

    // public Task DeleteUser(int id)
    // {

    // }

    public UserManipulator(TraniningDb traniningDb, IConfiguration root)
    {
        _traniningDb = traniningDb;
        _idenBaseURL = root.GetSection("ApiUrl").GetSection("IdentityService").Value;
    }
}