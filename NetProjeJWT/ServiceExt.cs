using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetProjeJWT.Identities;
using NetProjeJWT.Token;
using NetProjeJWT.Users;
using NetProjeJWT.Weather;
using System;

namespace NetProjeJWT
{
    public static class ServiceExt
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(x => { x.SuppressModelStateInvalidFilter = true; });
            services.AddAutoMapper(typeof(ServiceAssembly).Assembly);
           

            services.Configure<CustomTokenOptions>(configuration.GetSection("TokenOptions"));
            services.Configure<Clients>(configuration.GetSection("Clients"));
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<UserService>();
            services.AddIdentityExt();
            services.AddAuthenticationExt(configuration);
        }

        public static async Task SeedIdentityData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();


            await UserSeedData.Seed(userManager, roleManager);
        }

        // add to Identity
        // UserManager<AppUser> UserManager
        // RoleManager<AppRole> RoleManager
        // SignInManager<AppUser> SignInManager => web uygulaması olsa cookie oluşturmak için.
        public static void AddIdentityExt(this IServiceCollection services)
        {
            
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<AppDbContext>();
        }


        public static void AddAuthenticationExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var tokenOptions = configuration.GetSection("TokenOptions").Get<CustomTokenOptions>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateIssuer = true,

                    ValidAudiences = tokenOptions.Audience,
                    ValidateAudience = true,

                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenOptions.Signature)),
                    ValidateIssuerSigningKey = true,

                    ValidateLifetime = true
                };
            });
        }
    }
}
