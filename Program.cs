using System.Reflection;
using DbUp;
using Microsoft.AspNetCore.Authorization;
using qAndA.Authorization;
using qAndA.Data;
using qAndA.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Dbup setup
var connectionString ="Server=(localdb)\\MSSQLLocalDB; Database=QandA; Trusted_connection=true";
EnsureDatabase.For.SqlDatabase(connectionString);
var upgrader =
        DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

if(upgrader.IsUpgradeRequired()){
    upgrader.PerformUpgrade();
}


// Add services to the container.
builder.Services.AddRazorPages();


// Jwt-Based Authentication middleware

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
// builder.Services.AddAuthentication(options =>
//             {
//                 options.DefaultAuthenticateScheme =
//                   JwtBearerDefaults.AuthenticationScheme;
//                 options.DefaultChallengeScheme =
//                   JwtBearerDefaults.AuthenticationScheme;
//             }).AddJwtBearer(options =>
//             {
//                 options.Authority = builder.Configuration["Auth0:Authority"];
//                 options.Audience = builder.Configuration["Auth0:Audience"];
//             });

// Custom Authorizations
builder.Services.AddHttpClient();
builder.Services.AddAuthorization(
    options => options.AddPolicy("MustBeQuestionAuthor", 
        policy=> policy.Requirements.Add(new MustBeQuestionAuthorRequirements())
        )
    );


// Registration for dependency injection :

// Scoped = generate one instance for the whole request
// Transient = generate one instance each time it is requested 
// Singleton = generate one instanec for the whole app

builder.Services.AddSingleton<IQuestionCache, QuestionCache>();
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddScoped<IAuthorizationHandler,MustBeQuestionAuthorHandler>();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Questions}/{action=GetQuestions}");

app.Run();
