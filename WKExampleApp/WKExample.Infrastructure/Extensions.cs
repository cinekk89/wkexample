using Microsoft.Extensions.DependencyInjection;
using WKExample.Domain.Repositories;
using WKExample.Infrastructure.Repositories;

namespace WKExample.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services
            //.AddSingleton<IEmployeeRepository, EmployeeRepository>() //todo change to scope when repo will be from file
            .AddScoped<IEmployeeRepository, EmployeeFileRepository>() //todo change to scope when repo will be from file
            .AddScoped<IRegistrationNumberRepository, RegistrationNumberRepository>();
    }
}
