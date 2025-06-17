using Microsoft.AspNetCore.Authentication.Cookies;
using AppLogin.Api.Graphql.Mutations;
using Microsoft.EntityFrameworkCore;
using AppLogin.Api.Graphql.Queries;
using AppLogin.Application.Shared;
using AppLogin.Api.Core;
using AppLogin.Models;
using AppLogin.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPooledDbContextFactory<AppDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("conection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped(typeof(Mutation<>));
builder.Services.AddScoped(typeof(Query<>));

// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Auth/Login"; // Redirect to the login page if not authenticated
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the cookie expiration time
    options.AccessDeniedPath = "/Auth/AuthDenied"; // Redirect to auth denied page
});

// Define GraphQL services
builder.Services.AddGraphQLServer()
    .RegisterDbContextFactory<AppDBContext>()
    .AddQueryType(d => d.Name("Query")) //root "Query"
    .AddMutationType(d => d.Name("Mutation")) //root "Mutation"
    .AddAllModelTypes(typeof(User).Assembly, "AppLogin.Models") //models
    .AddAllExtensions(typeof(UserQuery).Assembly, "AppLogin.Api.Graphql.Queries") //queries
    .AddAllExtensions(typeof(UserMutation).Assembly, "AppLogin.Api.Graphql.Mutations") //mutations
    .AddProjections().AddFiltering().AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); }

app.UseStaticFiles();
app.UseRouting();

// Enable authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Auth}/{action=Login}/{id?}");
app.MapGraphQL("/graphql"); // Use GraphQL endpoint
app.Run();