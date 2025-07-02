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

// Database// Database - Update this section
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("FreelancerAPI.WebAPI")));  // Add this line

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

app.UseHttpsRedirection();

// Enable static files
app.UseStaticFiles();

// Default route to serve index.html
app.MapGet("/", async context =>
{
    context.Response.Redirect("/index.html");
});

app.UseAuthorization();
app.MapControllers();

app.Run();