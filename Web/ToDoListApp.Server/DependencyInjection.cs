using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using TodoListApp.Server.Services;

namespace TodoListApp.Server
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddWebServices(this WebApplicationBuilder builder)
        {
            // Register application and infrastructure services
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApplicationServices();

            // HttpContext access and current user service
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUser>();

            // Add controllers and OpenAPI
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            return builder;
        }
    }
}
