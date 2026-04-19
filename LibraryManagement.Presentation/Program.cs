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
using LibraryManagement.Presentation.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
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
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddTransient<ExceptionMiddleware>();
            builder.Services.AddTransient<RequestLoggingMiddleware>();


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

            var jwtKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
        path.StartsWithSegments("/hubs/notification"))
            {
                context.Token = accessToken;
                return Task.CompletedTask;
            }
            if (context.Request.Cookies.ContainsKey("jwt"))
            {
                context.Token = context.Request.Cookies["jwt"];
            }
            return Task.CompletedTask;
        }
    };
});

            //            builder.Services
            //.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;

            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,

            //        ValidIssuer = builder.Configuration["JWT:Issuer"],
            //        ValidAudience = builder.Configuration["JWT:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //   Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),

            //        NameClaimType = ClaimTypes.NameIdentifier,
            //        RoleClaimType = ClaimTypes.Role
            //    };


            //    options.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            //            return Task.CompletedTask;
            //        }
            //    };
            //});


            builder.Services.AddSwaggerGen(options =>
            {

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token as: *Bearer [your_token]*",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
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
