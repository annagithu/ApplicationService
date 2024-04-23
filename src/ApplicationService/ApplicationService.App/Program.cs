using ApplicationService.InternalContracts.Application;
using Serilog;
using Serilog.AspNetCore;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();

        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        builder.Services.AddSerilog();
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IApplicationService, ApplicationService.App.Services.ApplicationService>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}