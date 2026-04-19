using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ContactsInfrastructureModule
{
    public static IServiceCollection AddContactsEfModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
       
        services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("CrmDb")));

 
        services.AddIdentity<CrmUser, CrmRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ContactsDbContext>();

        return services;
    }
}