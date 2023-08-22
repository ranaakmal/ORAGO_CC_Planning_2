using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ORAGO_CC_Planning.Areas.Identity;
using ORAGO_CC_Planning.Data;
using ORAGO_CC_Planning.Models;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var transferConnectionString = builder.Configuration.GetConnectionString("TransferConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// builder.Services.AddDbContextFactory<ORAGOCCPlanningContext>(options =>
//     options.UseSqlite(connectionString));

builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddDbContextFactory<UseTransferDataContext>(options =>
    options.UseSqlServer(transferConnectionString));
// builder.Services.AddDbContextFactory<UseTransferDataContext>(options =>
//    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IBudgetEntryRepository, BudgetEntryRepository>();
builder.Services.AddScoped<IYearlyLockRepository, YearlyLockRepository>();
builder.Services.AddScoped<IBudgetCurrencyRepository, BudgetCurrencyRepository>();
builder.Services.AddScoped<IVolumeTemplateRepository, VolumeTemplateRepository>();
builder.Services.AddScoped<IEntityLocalCurrencyRepository, EntityLocalCurrencyRepository>();
builder.Services.AddScoped<ITransactionMonthlyLockRepository, TransactionMonthlyLockRepository>();
builder.Services.AddSingleton<IArticleFinalRepository, ArticleFinalRepository>();
builder.Services.AddSingleton<ICustomerFinalRepository, CustomerFinalRepository>();
builder.Services.AddScoped<IUploadLogHeaderRepository, UploadLogHeaderRepository>();
builder.Services.AddScoped<IUploadLogItemRepository, UploadLogItemRepository>();


builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYxMDUyQDMyMzAyZTMxMmUzMGdXUzF4UjNqNmNlTzljYTU5bWdHa2ZsZnQ2MEpJdkxSNUdYTUw0cDgwL2s9");
app.UseRequestLocalization("de");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();             
app.UseMiddleware<BlazorCookieLoginMiddleware>();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
