using BL.Interfaces;
using BL.Managers;
using BL.DB; // Ensure this is the correct namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Register DbContext with dependency injection
builder.Services.AddDbContext<BL.DB.MvcTaskManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register the TaskManager as singleton with a scoped factory
builder.Services.AddSingleton<ITaskManager>(provider =>
{
    Func<MvcTaskManagerContext> contextFactory = () =>
    {
        var scope = provider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<MvcTaskManagerContext>();
    };
    return new TaskManager(contextFactory);
});


// Add services to the container
builder.Services.AddControllers();


// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    // Use full type names for schema IDs to avoid conflicts
    c.CustomSchemaIds(type => type.FullName);
});

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAngularApp"); // Enable CORS

app.UseAuthorization();
app.MapControllers();

app.Run();
