using FinanceMenagerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinanceMenagerAPI
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FinanceContext>(options =>
            options.UseSqlServer(Configuration["ConnectionString:DefaultConnection"]));
            services.AddControllers();
            services.AddMvc();
            services.AddHealthChecks();

            //JWT Configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                // Logowanie szczegółów błędów
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        Console.WriteLine("Token received:");
                        Console.WriteLine(context.Request.Headers["Authorization"]);
                        if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
                        {
                            Console.WriteLine("Authorization header is missing.");
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"Authentication challenge: {context.ErrorDescription}");
                        if (!string.IsNullOrEmpty(context.ErrorDescription))
                        {
                            Console.WriteLine($"Challenge details: {context.ErrorDescription}");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               // endpoints.MapRazorPages();
                endpoints.MapControllers();
            //    endpoints.MapControllerRoute(
            //        name:"default",
            //        pattern:"{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
