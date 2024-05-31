using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigurationManager configuration = builder.Configuration;


        builder.Services.AddControllers();

        builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.MaxDepth = 64;
                });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDBContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName));
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDBContext>();


        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        builder.Services.AddScoped<IBeautyService, BeautyService>();
        builder.Services.AddScoped<IBeautyRepository, BeautyRepository>();


        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ApplicationDBContext>();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            await dbContext.Database.MigrateAsync();
            await DataSeeding.AddDateSeeding(dbContext);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "Error occurred during database migration or seeding");
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseCors("AllowSpecificOrigin");
        app.MapControllers();

        app.Run();
    }
}
