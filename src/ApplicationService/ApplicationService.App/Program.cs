
using ApplicationService.App;
using ApplicationService.InternalContracts.Application;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddSingleton<IApplicationService, ApplicationService.App.Services.ApplicationService>();

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

