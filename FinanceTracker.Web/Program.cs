using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Database;
using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using FinanceTracker.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using FinanceTracker.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(new ConnectionStringData
{
    Name = "Default"
});
builder.Services.AddTransient<IUserStore<ApplicationUserIdentity>, UserStore>();
builder.Services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddScoped<IDatabaseAccess, SqlServerDb>();
builder.Services.AddScoped<IProviderData, ProviderData>();
builder.Services.AddScoped<IAccountData, AccountData>();
builder.Services.AddScoped<ITransactionData, TransactionData>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ISelectListProvider, SelectListProvider>();
builder.Services.AddScoped<IRecordConsistency, RecordConsistency>();

builder.Services.AddIdentity<ApplicationUserIdentity, ApplicationRole>(option => option.SignIn.RequireConfirmedEmail = true)
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<ApplicationUserIdentity>>()
    .AddDefaultUI();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.DollarTracker";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
});

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transaction}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
