using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserEntity = User.Application.Entities.User;

namespace User.Application.DbContexts;

public class UserDbContext : IdentityDbContext<UserEntity>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) 
        : base(options)
    {
        Database.EnsureCreated();
    }
}
