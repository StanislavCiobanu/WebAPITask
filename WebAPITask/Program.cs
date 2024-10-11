using WebAPITask.Configuration;
using WebAPITask.DataAccess;
using WebAPITask.RequestCounterServices;
using WebAPITask.Services;
using WebAPITask.TimerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IAppUsageService, AppUsageService>();
builder.Services.AddScoped<IRequestCounterService, RequestCounterService>();
builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
// Optional Ex 1
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
// Optional Ex 2
builder.Services.AddTransient<ITimerService, TimerService>();
// Optional Ex 3
builder.Services.AddSingleton<ITestDataService, TestDataService>();
builder.Services.Configure<TestDataOptions>(builder.Configuration.GetSection(key: "TestData"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    // Optional Ex 1
    app.Services.GetService<IAppUsageService>().IncreaseCount();
    context.RequestServices.GetService<IRequestCounterService>().IncreaseCount();

    // Optional Ex 2
    app.Logger.LogInformation(app.Services.GetService<ITimerService>().GetCurrentTime().ToString());

    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
