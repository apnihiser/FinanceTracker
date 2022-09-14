using FinanceTracker.DataAccess.DbInitializer;
using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Database;
using FinanceTracker.DataAccess.Identity;
using FinanceTracker.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using FinanceTracker.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(new ConnectionStringData
{
    Name = "Default"
});

builder.Services.AddScoped<IDatabaseAccess, SqlServerDb>();
builder.Services.AddScoped<IProviderData, ProviderData>();
builder.Services.AddScoped<IAccountData, AccountData>();
builder.Services.AddScoped<ITransactionData, TransactionData>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<ApplicationUserIdentity, ApplicationRole>()
    .AddUserStore<UserStore>()
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<ApplicationUserIdentity>>()
    .AddDefaultUI();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseDbInitializer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
