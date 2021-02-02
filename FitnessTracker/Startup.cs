using AutoMapper;
using FitnessTracker.Data;
using FitnessTracker.Filters;
using FitnessTracker.Helpers;
using FitnessTracker.Options;
using FitnessTracker.Services;
using FitnessTracker.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace FitnessTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });

            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IAuthHelper, AuthHelper>();

            InstallDatabaseServices(services);
            InstallOtherServices(services);
            InstallAuthService(services);
            InstallPolicies(services);
            InstallSwaggerService(services);

            // Rejestrowanie Serwisów
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<ITrainingService, TrainingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Test") || env.IsEnvironment("Backend") || true) //TODO
            {
                app.UseDeveloperExceptionPage();

                if (!env.IsEnvironment("Test") || true) //TODO
                {
                    app.UseSwagger(options =>
                    {
                        options.RouteTemplate = "swagger/{documentName}/swagger.json";
                    });
                    app.UseSwaggerUI(option =>
                    {
                        option.SwaggerEndpoint("../swagger/v1/swagger.json", "V1 Docs");
                    });
                }
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "ClientApp\\build\\static")),
                RequestPath = "/static"
            });

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }


        private void InstallDatabaseServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
        private void InstallPolicies(IServiceCollection service)
        {
            // service.AddAuthorization(options =>
            // {
            //     options.AddPolicy("Post.Editor", builder =>
            //     {
            //         builder.RequireClaim("Post.Editor", "true");
            //     });
            // });

            // service.AddAuthorization(options =>
            // {
            //     options.AddPolicy("Post.Editor",builder =>
            //     {
            //         builder.RequireClaim("Post.Editor", "true");
            //         builder.RequireRole("Editor");
            //     });
            // });
        }
        private void InstallOtherServices(IServiceCollection service)
        {
            service.AddScoped<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), request.Path);
                return new UriService(absoluteUrl);
            });

            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // Wyłącz domyślną walidację aby użyć fluent validation
            });

            service
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
        private void InstallAuthService(IServiceCollection service)
        {
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            service.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            service.AddSingleton(tokenValidationParameters);
            service.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });
        }
        private void InstallSwaggerService(IServiceCollection service)
        {
            service.AddSwaggerGen(x =>
            {

                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Fitness Tracker API",
                    Description = "Example API Description",
                });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
                x.AddFluentValidationRules();
                x.ExampleFilters();
                x.OperationFilter<AddResponseHeadersFilter>();
                x.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                x.OperationFilter<AuthResponsesOperationFilter>();
                x.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            service.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

    }
}