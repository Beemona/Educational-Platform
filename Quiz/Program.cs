using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizDbContext.Data;
using QuizDbContext.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core with SQL Server using Windows Authentication
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<IStudentResultService, StudentResultService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuizService, QuizService>();

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add authentication and authorization
builder.Services.AddAuthentication("YourAuthScheme")
    .AddCookie("YourAuthScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "quiz",
        pattern: "Quiz/{action=Index}/{id?}",
        defaults: new { controller = "Quiz" });

    endpoints.MapControllerRoute(
        name: "quizcreation",
        pattern: "QuizCreation/{action=Create}/{id?}",
        defaults: new { controller = "QuizCreation" });

    endpoints.MapControllerRoute(
        name: "courses",
        pattern: "Course/{action=Course}/{id?}",
        defaults: new { controller = "Course" });

    endpoints.MapControllerRoute(
        name: "admissions",
        pattern: "Admission/{action=Index}/{id?}",
        defaults: new { controller = "Admission" });
});

app.Run();
