using Microsoft.AspNetCore.Authentication;
using RaceDay_web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var baseUrl = "https://localhost:7159/";
builder.Services.AddHttpClient("RaceDayAPI", client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<IAuthenticationApiClient, AuthenticationApiClient>();
builder.Services.AddScoped<IEventsApiClient, EventsApiClient>();
builder.Services.AddScoped<ICategoriesApiClient, CategoriesApiClient>();
builder.Services.AddScoped<IEnrollmentApiClient, EnrollmentApiClient>();
builder.Services.AddScoped<IResultsApiClient, ResultsApiClient>();
builder.Services.AddScoped<IProfileApiClient, ProfileApiClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
