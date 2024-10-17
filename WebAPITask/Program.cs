using WebAPITask.Configuration;
using WebAPITask.DataAccess;
using WebAPITask.Middleware;
using WebAPITask.RequestCounterServices;
using WebAPITask.Services;
using WebAPITask.TimerService;

// MediatR

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAppUsageService, AppUsageService>();
builder.Services.AddScoped<IRequestCounterService, RequestCounterService>();
builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
// Optional Ex 2
builder.Services.AddTransient<ITimerService, TimerService>();
// Optional Ex 3
builder.Services.AddSingleton<ITestDataService, TestDataService>();
builder.Services.Configure<TestDataOptions>(builder.Configuration.GetSection(key: "TestData"));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

    await next.Invoke();
});

// Optional Ex 2
app.UseTimerServiceMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
