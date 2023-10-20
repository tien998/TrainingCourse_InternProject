using System.Text;
using Newtonsoft.Json;
using TrainingCourse.DTO;

namespace UserServices;

public class UserManipulator
{
    string? _idenBaseURL;

    /// <summary>
    /// This function call API from Identity project
    /// </summary>
    /// <typeparam name="T">UserRsDTO</typeparam>
    /// <param name="role">Get role from "TrainingCourse.DTO.Role"</param>
    /// <returns></returns>
    public async Task<string> GetUsers(int index, int take, string role, HttpContext httpContext)
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
                return rsJson;
            }
        }
    }

    public async Task<string> Get(int id, string role, HttpContext httpContext)
    {
        string jwtBearer = httpContext.Request.Headers["Authorization"].ToString();
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", jwtBearer);
            var url = _idenBaseURL + $"/{role}/getUser/{id}";
            using (HttpRequestMessage requestMessage = new(HttpMethod.Get, url))
            {
                var rs = client.Send(requestMessage);
                var rsJson = await rs.Content.ReadAsStringAsync();
                httpContext.Response.StatusCode = (int)rs.StatusCode;
                return rsJson;
            }
        }
    }

    public async Task RegisterUsers<T>(T user, string role, HttpContext httpContext)
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

    public async Task EditUser<T>(T user, string role, HttpContext httpContext)
    {
        string jwtBearer = httpContext.Request.Headers["Authorization"].ToString();
        string jsonData = JsonConvert.SerializeObject(user);
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", jwtBearer);
            var url = _idenBaseURL + $"/{role}/edit";
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

    public async Task DeleteUser(int id, string role, HttpContext httpContext)
    {
        string jwtBearer = httpContext.Request.Headers["Authorization"].ToString();
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", jwtBearer);
            var url = _idenBaseURL + $"/{role}/delete/{id}";
            using (HttpRequestMessage requestMessage = new(HttpMethod.Get, url))
            {
                var rs = await client.SendAsync(requestMessage);
                httpContext.Response.StatusCode = (int)rs.StatusCode;
            }
        }
    }

    public async Task<bool> IsAuthor(string role, HttpContext context)
    {
        string jwtBearer = context.Request.Headers["Authorization"]!;
        string? url = _idenBaseURL + $"/authorize/{role}";
        using (HttpClient client = new())
        {
            client.DefaultRequestHeaders.Add("Authorization", jwtBearer);
            using (HttpRequestMessage requestMessage = new(HttpMethod.Get, url))
            {
                var rs = client.Send(requestMessage);
                context.Response.StatusCode = (int)rs.StatusCode;
                return Convert.ToBoolean(await rs.Content.ReadAsStringAsync());
            }
        }
    }

    public async Task<bool> IsAuthors(string role1, string role2, HttpContext context)
    {
        bool IsAuthor_role1 = await IsAuthor(role1, context);
        if(!IsAuthor_role1)
        {
            bool IsAuthor_role2 = await IsAuthor(role2, context);
            if(IsAuthor_role2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    // This function is not recommend to use. The Client should consider call api direct from the Identity_Service_Project for quickly performence
    // api: http://localhost:5024/teacher/GetAll/dropdown
    public async Task<string> GetTeachers_DropdownDTO(HttpContext context)
    {
        string url = _idenBaseURL! + $"/{Role.teacher}/GetAll/dropdown";
        using (HttpClient client = new())
        {
            using (HttpRequestMessage requestMessage = new(HttpMethod.Get, url))
            {
                var rs = await client.SendAsync(requestMessage);
                string? rsJson = await rs.Content.ReadAsStringAsync();
                return rsJson;
            }
        }
    }



    public UserManipulator(IConfiguration root)
    {
        _idenBaseURL = root.GetSection("ApiUrl").GetSection("IdentityService").Value;
    }
}