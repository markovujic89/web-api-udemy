using DemoProject;
using DemoProject.Data;
using DemoProject.Logger;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
            .WriteTo.File("log/booklogs.txt", rollingInterval:RollingInterval.Day).CreateLogger();

        // builder.Host.UseSerilog();
        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddAutoMapper(typeof(MappingConfig));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<ILogging, Logging>();
        builder.Services.AddScoped<IBookStore, BookStore>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}