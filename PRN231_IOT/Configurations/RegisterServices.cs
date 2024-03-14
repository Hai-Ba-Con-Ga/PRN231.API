using Repository.Common;
using WebAPI.Middleware;

namespace WebAPI.Configurations
{
    public static class RegisterServices
    {

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(AuthensMidlleware));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddFluentValidation();
        }
    }
}
