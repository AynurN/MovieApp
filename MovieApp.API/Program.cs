using MovieApp.Data;
using MovieApp.Business;
using FluentValidation.AspNetCore;
using FluentValidation;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using MovieApp.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace MovieApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddValidatorsFromAssemblyContaining<MovieCreateDtoValidator>(); // register validators
    builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
            builder.Services.AddFluentValidationClientsideAdapters(); // for client side
            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile<MapProfile>();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRepositories(builder.Configuration.GetConnectionString("default"));
            builder.Services.AddServices();
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredUniqueChars=2;
                opt.Password.RequireNonAlphanumeric=true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit=true;
                opt.Password.RequireLowercase=true;
                    opt.Password.RequireUppercase=true;
                opt.User.RequireUniqueEmail=true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer= true,
                    ValidateAudience= true,
                    ValidIssuer= "https://localhost:7267/",
                    ValidAudience= "https://localhost:7267/",
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7537a543-a120-4843-80c9-28e454c0c351")),
                    ValidateLifetime =true,
                    ClockSkew=TimeSpan.Zero

                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
