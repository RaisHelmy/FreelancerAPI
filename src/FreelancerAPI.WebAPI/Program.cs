using FreelancerAPI.Application.Interfaces;
using FreelancerAPI.Application.Mappings;
using FreelancerAPI.Application.Services;
using FreelancerAPI.Infrastructure.Data;
using FreelancerAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure connection string with environment variable support
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Replace environment variables in connection string
if (!string.IsNullOrEmpty(connectionString))
{
    connectionString = connectionString
        .Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost,1433")
        .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "FreelancerApiDb")
        .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "sa")
        .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "YourStrong@Passw0rd");
}

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repositories
builder.Services.AddScoped<IFreelancerRepository, FreelancerRepository>();
builder.Services.AddScoped<ISkillsetRepository, SkillsetRepository>();
builder.Services.AddScoped<IHobbyRepository, HobbyRepository>();

// Services
builder.Services.AddScoped<IFreelancerService, FreelancerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Enable Swagger in production for easy API testing
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Freelancer API V1");
        c.RoutePrefix = "swagger";
    });
}

// Enable static files
app.UseStaticFiles();

// Default route to serve index.html
app.MapGet("/", async context =>
{
    context.Response.Redirect("/index.html");
});

app.UseAuthorization();
app.MapControllers();

// Auto-apply migrations on startup (optional - for easier deployment)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        context.Database.Migrate();
        Console.WriteLine("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }
}

app.Run();