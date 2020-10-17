using AutoMapper;
using FlashCards.Data.DataModel;
using FlashCards.Helpers.AutoMapper;
using FlashCards.Helpers.AutoMapper.ExtendedProfiles;
using FlashCards.Services.Abstracts;
using FlashCards.Services.Implementations;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.Repositories.Implementations;
using FlashCards.Services.UnitOfWork.Abstracts;
using FlashCards.Services.UnitOfWork.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace FlashCards.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: Get all classes from assembly
            services.AddAutoMapper(typeof(CommonProfiles), 
                typeof(UserForDetailProfile), typeof(CourseForCreateProfile), typeof(CourseForListProfile),
                typeof(CourseForDetailProfile));

            services.AddDbContext<FlashcardsDataModel>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                config.EnableSensitiveDataLogging();
            });
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "FlashCards API", 
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Gracjan Bryt",
                        Email = "gracjanb97@gmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ISubscribedCourseRepository, SubscribedCourseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "FlashCards API");
                x.DefaultModelRendering(ModelRendering.Model);
            });

            app.UseCors(x =>
                x.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
