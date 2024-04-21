using Serilog;
using Serilog.Exceptions;

namespace Torc.API.Configurations;

public static class SerilogConfiguration
{
    public static void SerilogConfigure(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration, string environmentName)
    {
        var serilogLogger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .WriteTo.Debug()
            .WriteTo.Console()
            .Enrich.WithProperty("Environment", environmentName)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.AddSerilog(logger: serilogLogger, dispose: true);
        });
    }
}
