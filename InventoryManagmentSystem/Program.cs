
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using InventoryManagmentSystem.Core.BackgroundJobs.Concrete;
using InventoryManagmentSystem.Core.BackgroundJobs.Interfaces;
using InventoryManagmentSystem.Core.Data;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Domain.UnitOfWork;
using InventoryManagmentSystem.EndPoints;
using InventoryManagmentSystem.Infrastructure.Persistence.Repositories;
using InventoryManagmentSystem.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics;
using System.Text;

namespace InventoryManagmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<InventoryContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("cs"));

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.LogTo(log => Debug.WriteLine(log), LogLevel.Information);
            });
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<InventoryContext>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(Core.Interfaces.IGenericRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<ITransactionNotifier, TransactionNotifier>();
            builder.Services.AddScoped<IProductNotifier, ProductNotifier>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JWT:Iss"],
                    ValidAudience = builder.Configuration["JWT:Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });

            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddMediatR(opts =>
                opts.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Inventory management system",
                    Description = " "
                });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });

            Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.MSSqlServer(
                    connectionString: builder.Configuration.GetConnectionString("cs"),
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true }
                ).CreateLogger();
            builder.Services.AddHangfire(h => h.UseSqlServerStorage(builder.Configuration.GetConnectionString("cs")));
            builder.Services.AddHangfireServer();
            
            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/HangfireDash");
            RecurringJob.AddOrUpdate<ITransactionNotifier>(i => i.ArchieveTranssactionThanYear(), Cron.Monthly);
            RecurringJob.AddOrUpdate<IProductNotifier>(p => p.CheckProductStock(), Cron.Daily);

            var globalGroup = app.MapGroup("");

            var endpointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => typeof(EndpointDefinition).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<EndpointDefinition>();

            foreach (var endpoint in endpointDefinitions)
            {
                endpoint.RegisterEndpoints(globalGroup);
            }

            app.Run();
        }
    }
}
