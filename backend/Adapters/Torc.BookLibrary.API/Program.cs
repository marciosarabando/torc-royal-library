using Newtonsoft.Json;
using Torc.API.Configurations;
using Torc.BookLibrary.IoC;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    x.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
    x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
});

builder.Logging.ClearProviders();

builder.Services.SerilogConfigure(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.ConfigureIoC(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => {
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UpdateDatabase();

app.UseAuthorization();

app.MapControllers();

app.Run();
