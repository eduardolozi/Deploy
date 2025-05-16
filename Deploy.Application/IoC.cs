using Deploy.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deploy.Application;

public static class IoC
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddDbContext<DeployContext>();
        services.AddScoped<ProjectService>();
    }
}