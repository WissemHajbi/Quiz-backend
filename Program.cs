using System.Reflection;
using DbUp;
using qAndA.Data;

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

// registering the DataRepository for dependency injection
builder.Services.AddScoped<IDataRepository, DataRepository>();
// Scoped = generate one instance for the whole request
// Transient = generate one instance each time it is requested 
// Singleton = generate one instanec for the whole app


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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Questions}/{action=GetQuestions}");

app.Run();
