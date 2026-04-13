using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Application.Mappings;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Identity;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Infrastructure.UnitOfWork;
using LibraryManagement.Presentation.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LibraryManagement.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBorrowService, BorrowService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            builder.Services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<LibraryDbContext>()
                .AddDefaultTokenProviders();

           
            builder.Services.AddAuthorization();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<BookProfile>();
            builder.Services.AddTransient<RequestLoggingMiddleware>();
            builder.Services.AddTransient<ExceptionMiddleware>();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<BookProfile>();
                cfg.AddProfile<ReviewProfile>();
                cfg.AddProfile<BorrowProfile>();
            }, AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
            });


            builder.Services
         .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.SaveToken = true;

             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ClockSkew = TimeSpan.Zero,

                 ValidIssuer = builder.Configuration["JWT:Issuer"],
                 ValidAudience = builder.Configuration["JWT:Audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
             };

             options.Events = new JwtBearerEvents
             {
                 OnAuthenticationFailed = context =>
                 {
                     Console.WriteLine("Authentication failed: " + context.Exception.Message);
                     return Task.CompletedTask;
                 }
             };
         });


            var app = builder.Build();

            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
