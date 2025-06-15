using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using AppLogin.Application.Shared;
using AppLogin.Api.Mutations;
using AppLogin.Api.Queries;
using AppLogin.Api.Core;
using AppLogin.Models;
using AppLogin.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPooledDbContextFactory<AppDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("conection")));
builder.Services.AddScoped(typeof(MutationService<>));
builder.Services.AddScoped(typeof(QueryService<>));


// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Access/Login"; // Redirect to the login page if not authenticated
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the cookie expiration time
    options.AccessDeniedPath = "/Access/AccessDenied"; // Redirect to access denied page
});

// Define GraphQL services
builder.Services.AddGraphQLServer()
    .RegisterDbContextFactory<AppDBContext>()
    .AddQueryType(d => d.Name("Query")) //root "Query"
    .AddMutationType(d => d.Name("Mutation")) //root "Mutation"
    .AddAllModelTypes(typeof(User).Assembly, "AppLogin.Models") //models
    .AddAllExtensions(typeof(UserQuery).Assembly, "AppLogin.Api.Queries") //queries
    .AddAllExtensions(typeof(UserMutation).Assembly, "AppLogin.Api.Mutations") //mutations
    .AddProjections().AddFiltering().AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); }

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Access}/{action=Login}/{id?}");
app.MapGraphQL("/graphql"); // Use GraphQL endpoint
app.Run();