using System.Text;
using BusinessObject.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Configurations;
using WebAPI.Configurations.OpenApi;
using WebAPI.Hubs.DataReport;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SettingsBinding();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey))
                    };
                });

            builder.Services.AddServices();
            builder.Services.AddSignalR();

            builder.Services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                // this defines a CORS policy for SignalR hub
                options.AddPolicy("SignalRHubs", builder => builder
               .WithOrigins(new string[] { "http://localhost:3000" })
               .AllowAnyHeader()
               .WithMethods("GET", "POST")
               .AllowCredentials());

            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerAPI();
            }

            app.UseCors("AllowAll");

            //use middleware
            app.UseMiddleware<ExceptionMiddleware>();

            //use SignalR hub
            app.MapHub<DataReportHub>("/hubs/data-report").RequireCors("SignalRHubs");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}