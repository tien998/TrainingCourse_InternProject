using Microsoft.EntityFrameworkCore;
using UserServices;

namespace DI;

public static class WebAppBuilderExtension
{
    public static void AddDI(this WebApplicationBuilder builder)
    {
        string connection = builder.Configuration.GetConnectionString("dbContext")!;

        builder.Services.AddDbContext<TraniningDb>(conf =>
        {
            conf.UseSqlServer(connection);
        });
        builder.Services.AddScoped<UserManipulator>();
    }
}