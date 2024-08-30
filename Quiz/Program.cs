using Microsoft.EntityFrameworkCore;
using QuizDbContext.Data;
using QuizDbContext.Services; 


var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core with SQL Server using Windows Authentication
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your services
builder.Services.AddScoped<IStudentResultService, StudentResultService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuizService, QuizService>();

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    // endpoints.MapRazorPages(); // If you're using Razor Pages
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "quiz",
    pattern: "Quiz/{action=Index}/{id?}",
    defaults: new { controller = "Quiz" });

app.MapControllerRoute(
    name: "quizcreation",
    pattern: "QuizCreation/{action=Create}/{id?}",
    defaults: new { controller = "QuizCreation" });

app.MapControllerRoute(
    name: "courses",
    pattern: "Course/{action=Course}/{id?}",
    defaults: new { controller = "Course" });


app.Run();
