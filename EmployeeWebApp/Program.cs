using EmployeeWebApp.Services;
using EmployeeWebApp.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();
