using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Use InMemory database for simplicity/testing. Change DB name as needed.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TodoListDb"));

            // Register the interface to the concrete DbContext
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
