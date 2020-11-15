using AutoMapper;
using FlashCards.Data.DataModel;
using FlashCards.Helpers.AutoMapper;
using FlashCards.Services.Abstracts;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Common.Implementations;
using FlashCards.Services.Implementations;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
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
            // AutoMapper config
            var automapperProfiles = Assembly.GetEntryAssembly()
                                             .GetReferencedAssemblies()
                                             .Select(Assembly.Load)
                                             .SelectMany(x => x.GetTypes())
                                             .Where(x => x.Name.EndsWith("Profile") && x.FullName.StartsWith("FlashCards.Helpers.AutoMapper"))
                                             .ToList();
            automapperProfiles.Add(typeof(CommonProfiles));
            services.AddAutoMapper(automapperProfiles.ToArray());

            // Database connection config
            services.AddDbContext<FlashcardsDataModel>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                config.EnableSensitiveDataLogging();
            });

            // Swagger documentation config
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

            // Authentication config
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

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IFlashcardRepository, FlashcardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISubscriptionsService, SubscriptionService>();
            
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

            //app.UseExceptionHandler(config =>
            //{
            //    config.Run(async context =>
            //    {
            //        context.Response.StatusCode = 500;
            //        context.Response.ContentType = "application/json";

            //        var error = context.Features.Get<IExceptionHandlerFeature>();
            //        if(error != null)
            //        {
            //            var ex = error.Error;

            //            await context.Response.WriteAsync(new ErrorModel()
            //            {

            //            })
            //        }
            //    });
            //});

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
