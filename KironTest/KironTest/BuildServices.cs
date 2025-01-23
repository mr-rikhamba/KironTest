using System;
using System.Data;
using System.Text;
using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;
using KironTest.Logic.Models;
using KironTest.Logic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KironTest;

public static class BuildServices
{
    public static void Init(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddMemoryCache();
        builder.Services.AddHttpClient();
        ConnectionManager.InitialiseConnectionString(builder.Configuration.GetConnectionString("KironConnection"));
        builder.Services.AddSingleton<ConnectionManager>();
        builder.Services.AddSingleton<BankServiceManager>();
        builder.Services.AddSingleton<UkBankBackgroundService>();
        builder.Services.AddHostedService(sp => sp.GetRequiredService<UkBankBackgroundService>());

        builder.Services.AddTransient<IDalContract, DalService>();
        builder.Services.AddTransient<ICacheContract, CacheHelper>();
        builder.Services.AddTransient<IUserContract, UserService>();
        builder.Services.AddTransient<IBankHolidayContract, BankHolidayService>();
        builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("AuthConfig"));

        // Validate AutConfig
        var serviceProvider = builder.Services.BuildServiceProvider();
        var authConfig = serviceProvider.GetRequiredService<IOptions<AuthConfig>>().Value;
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(authConfig.JwtSecret);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });

    }

}
