using AspNet.Security.OpenIdConnect.Primitives;
using Common.Repository.Interfaces;
using Common.Service.Interfaces;
using Common.UOW;
using Database;
using Database.AuthDomain;
using DLog.Domain.Model;
using DLog.Service.Interfaces;
using DLog.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Numerology.API.Authorization;
using Numerology.API.Authorization.Requirments;
using Numerology.API.Claims;
using Numerology.API.Filters;
using Numerology.API.Hubs;
using Numerology.API.Managers.Interfaces;
using Numerology.API.Managers.Managers;
using Numerology.Application.Interfaces;
using Numerology.Application.Services;
using Numerology.Domain.Models;
using Numerology.Repository;
using OpenIddict.Abstractions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.Prerendering;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System;
using System.Threading.Tasks;
using Numerology.Application.Init;
using Microsoft.AspNetCore.Http;

namespace Numerology.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static string ClientAppPath { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = Configuration.GetSection("DefaultConnection").Value;
            //services.AddDbContext<DataContext>(options =>
            //{
            //    options.UseSqlServer(connection);
            //    options.UseOpenIddict();
            //});

            //Add Unit of work DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(Authorization.Policies.ViewAllUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, Numerology.API.Managers.ApplicationPermissions.ViewUsers));
            //    options.AddPolicy(Authorization.Policies.ManageAllUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, Numerology.API.Managers.ApplicationPermissions.ManageUsers));

            //    options.AddPolicy(Authorization.Policies.ViewAllRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, Numerology.API.Managers.ApplicationPermissions.ViewRoles));
            //    options.AddPolicy(Authorization.Policies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new Authorization.Requirments.ViewRoleAuthorizationRequirement()));
            //    options.AddPolicy(Authorization.Policies.ManageAllRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, Numerology.API.Managers.ApplicationPermissions.ManageRoles));

            //    options.AddPolicy(Authorization.Policies.AssignAllowedRolesPolicy, policy => policy.Requirements.Add(new Authorization.Requirments.AssignRolesAuthorizationRequirement()));
            //});

            //// Add identity
            //services.AddIdentity<AppUser, AppUserRole>()
            //    .AddEntityFrameworkStores<DataContext>()
            //    .AddDefaultTokenProviders();

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<IRepository<ClientModel>, ClientRepository>();
            services.AddScoped<IRepository<LetterModel>, LetterRepository>();
            services.AddScoped<IRepository<NameModel>, NameRepository>();
            services.AddScoped<IRepository<NumerologyPortraitModel>, NumerologyPortraitRepository>();
            services.AddScoped<IRepository<ClientMeetings>, ClientMeetingsRepository>();


            // Account Manager
            //services.AddScoped<IAccountManager, AccountManager>();
            //services.AddScoped<IDLogService, DLogService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ILetterService, LetterService>();
            services.AddScoped<INameService, NameService>();
            services.AddScoped<INumerologyPortraitService, NumerologyPortraitService>();
            services.AddScoped<IClientMeetingsService, ClientMeetingsService>();
            services.AddScoped<INumerologyCalculator, NumerologyCalculator>();



            //// Auth Handlers
            //services.AddSingleton<IAuthorizationHandler, ViewUserAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, ManageUserAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, ViewRoleAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, AssignRolesAuthorizationHandler>();

            //// Configure Identity options and password complexity here
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // User settings
            //    options.User.RequireUniqueEmail = true;

            //    //    //// Password settings
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 0;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    //    //options.Password.RequireLowercase = false;

            //    //    //// Lockout settings
            //    //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    //    //options.Lockout.MaxFailedAccessAttempts = 10;

            //    options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
            //    options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
            //    options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            //});

            //// Register the OpenIddict services.
            //services.AddOpenIddict()
            //    .AddCore(options =>
            //    {
            //        options.UseEntityFrameworkCore().UseDbContext<DataContext>();
            //    })
            //    .AddServer(options =>
            //    {
            //        options.UseMvc();
            //        options.EnableTokenEndpoint("/connect/token");
            //        options.AllowPasswordFlow();
            //        options.AllowRefreshTokenFlow();
            //        options.AcceptAnonymousClients();
            //        options.DisableHttpsRequirement(); // Note: Comment this out in production
            //        options.RegisterScopes(
            //            OpenIdConnectConstants.Scopes.OpenId,
            //            OpenIdConnectConstants.Scopes.Email,
            //            OpenIdConnectConstants.Scopes.Phone,
            //            OpenIdConnectConstants.Scopes.Profile,
            //            OpenIdConnectConstants.Scopes.OfflineAccess,
            //            OpenIddictConstants.Scopes.Roles);

            //        // options.UseRollingTokens(); //Uncomment to renew refresh tokens on every refreshToken request
            //        // Note: to use JWT access tokens instead of the default encrypted format, the following lines are required:
            //        // options.UseJsonWebTokens();
            //    })
            //    .AddValidation(); //Only compatible with the default token format. For JWT tokens, use the Microsoft JWT bearer handler.

            services.AddSession();

            // Add SignalR
            services.AddSignalR();

            //// Add cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllCorsPolicy",
                    builder =>
                        builder
                            //.WithOrigins("http://localhost:44363")
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            // Health check
            services.AddHealthChecks();

            // Add mvc and session
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddMvcOptions((x) => { x.EnableEndpointRouting = false; x.Filters.Add<UnitOfWorkActionFilter>(); });
            services.AddSession();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory/*, ILetterService letterService*/)
        {
            loggerFactory.AddFile("Logs/logAuth-{Date}.txt");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            /*if(letterService.CountAsync().GetAwaiter().GetResult() == 0)
            {
                LetterDBInit.InitLetters(letterService).GetAwaiter().GetResult();
            }*/

            app.UseRouting();

            //app.UseStaticFiles();
            app.UseCors("AllowAllCorsPolicy");
            //app.UseCorsMiddleware();
            app.UseSession();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notification");
            });
            app.UseMvc();// Propably because of this we have a problem with cors origin. If this does not work [EnableCors("MyPolicy")] in each controller

            CreateAngularPaths();

            ////////// For dev development it's not really comfortable. Without this we have hot reload when working with angular but when we lunch app by this we lose also debugging by vs code
            //////// This code is only for Reales
            ////if(env.IsProduction())
            ////{
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                // Check how does it work on production

                spa.Options.SourcePath = Configuration.GetSection("ClientPath").Value;

                spa.UseAngularCliServer(npmScript: "start");
                spa.Options.StartupTimeout = TimeSpan.FromSeconds(120); // Increase the timeout if angular app is taking longer to startup

            });
            ////}
        }

        private void CreateAngularPaths()
        {
            const string clientAppPath = @"\ClientApp2\";

            var envPath = System.Environment.CurrentDirectory;
            var index = envPath.IndexOf("Source");
            var subPath = envPath.Substring(0, index + 7);

            //ClientAppPath = subPath + clientAppPath;
            ClientAppPath = @"P:\Mom\Numerologia3\ClientApp2";
        }

        
    }

    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}
