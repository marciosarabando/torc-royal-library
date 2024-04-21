using Domain.Interfaces.Services.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Data;
using Repository.Repositories.Base;
using System.Reflection;
using Torc.Application.Base;
using Torc.BookLibrary.Domain.Interfaces.Repositories;

namespace Torc.BookLibrary.IoC;

public static class DependencyInjectionExtension
{
    public static void ConfigureIoC(this IServiceCollection services, IConfiguration configuration, string environmentName)
    {
        services.ConfigureDbContext(configuration);
        services.AddScopedRepositories();
        services.AddScopedServices();
        services.AddAutoMapper(typeof(DependencyInjectionExtension));
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookLibraryContext>(options =>
            options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.EnableRetryOnFailure())
                //.UseInMemoryDatabase("Database")
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information)); //SQL SERVER
    }

    public static void AddScopedRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.UseAllOfType(new[] { typeof(GenericRepository<>).Assembly, typeof(IGenericRepository<>).Assembly }, "Repository");
    }

    public static void AddScopedServices(this IServiceCollection services)
    {
        services.UseAllOfType(new[] { typeof(BaseApplicationService<,>).Assembly, typeof(IBaseApplicationService<>).Assembly }, "Service");
        services.UseAllOfType(new[] { typeof(BaseService).Assembly, typeof(IBaseService).Assembly }, "Service");
    }

    public static void UpdateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        using var context = scope.ServiceProvider.GetService<BookLibraryContext>();
        context?.Database.EnsureCreated();
    }

    private static void UseAllOfType(this IServiceCollection services, Assembly[] assemblies, string serviceType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        services.AddDependenciesByNamingConvention(
            assemblies,
            x => x.Name.EndsWith(serviceType) && !x.Name.EndsWith("WebService"),
            lifetime
        );
    }

    private static void AddDependenciesByNamingConvention(this IServiceCollection services, Assembly[] assemblies, Func<Type, bool> predicate, ServiceLifetime lifetime)
    {
        var implementations = new List<Type>();
        var interfaces = new List<Type>();

        foreach (var assembly in assemblies)
        {
            implementations.AddRange(assembly.ExportedTypes
                .Where(x => !x.IsInterface && predicate(x)));
            interfaces.AddRange(assembly.ExportedTypes
                .Where(x => x.IsInterface && predicate(x)));
        }

        foreach (var @interface in interfaces)
        {
            var implementation = implementations
                .FirstOrDefault(x => @interface.IsAssignableFrom(x)
                    && $"I{x.Name}" == @interface.Name && !x.IsAbstract);

            if (implementation == null)
                throw new InvalidOperationException(string.Format("CouldNotFindAnImplementationForTheInterface", @interface));

            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(@interface, implementation);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(@interface, implementation);
                    break;
                default:
                    services.AddTransient(@interface, implementation);
                    break;
            }
        }
    }
}
