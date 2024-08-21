var builder = WebApplication.CreateBuilder(args);

// Register the service as Singleton
builder.Services.AddSingleton<IStudentResultService, StudentResultService>();
builder.Services.AddSingleton<IQuestionService, QuestionService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "quiz",
    pattern: "Quiz/{action=Index}/{id?}",
    defaults: new { controller = "Quiz" });

app.Run();
