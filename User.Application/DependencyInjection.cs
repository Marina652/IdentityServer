using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using User.Application.DbContexts;
using User.Application.Services;
using UserEntity = User.Application.Entities.User;

namespace User.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAccountService, AccountService>();

        services.AddDbContext<UserDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("UserDbConnecion"), m =>
              {
                  m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                  m.MigrationsHistoryTable($"__{nameof(UserDbContext)}");
              }));

        services.AddIdentity<UserEntity, IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();


        //services.AddIdentityServer();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
