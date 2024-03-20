﻿using BusinessObject.Common;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Service.Implement;
using Service.Interface;
using WebAPI.Middleware;

namespace WebAPI.Configurations
{
    public static class RegisterServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<DbContext>(options =>
            {
                options.UseSqlServer(AppConfig.ConnectionStrings.DefaultConnection);
            } , ServiceLifetime.Transient);

            services.AddScoped<DbContext, Prn231IotContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(AuthensMidlleware));
            services.AddScoped(typeof(ExceptionMiddleware));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDeviceTypeService, DeviceTypeService>();
            services.AddFluentValidation();
        }
    }
}