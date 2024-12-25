using BookStore.BLL.Services.TokenServices.Realizations;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BookStore.WebApi.Extensions
{
    public static class ServicesExtensions
    {
        const string ENV_VAR = "ASPNETCORE_ENVIRONMENT";

        const string DEFAULT_ENV_VAR = "Development";

        public static void AddHttp_ContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static string EnableCORS(this IServiceCollection services, IConfiguration config, string policy)
        {
            var env = Environment.GetEnvironmentVariable(ENV_VAR) ?? DEFAULT_ENV_VAR;
            
            var origins = config
                .GetSection(env)
                .GetSection("CORS")
                .GetSection("AllowedOrigins")
                .Get<string[]>();
           
            CorsPolicy corsPolicy = new CorsPolicy();
            
            foreach (var o in origins)
            {
                corsPolicy.Origins.Add(o);
            }
           
            services.AddCors(conf =>
            {                
                conf.AddPolicy(policy, builder =>
                {
                    builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()                    ;
                });
            });

            return policy;
        }

        public static void AddJWTTokenConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var env = Environment.GetEnvironmentVariable(ENV_VAR) ?? DEFAULT_ENV_VAR;

            var config = configuration.GetSection(env)
                .GetSection("JWTTokenConfiguration")
                .Get<JWTTokenConfiguration>();
            if (config is not null)
                services.AddSingleton(config);
        }
    }
}
