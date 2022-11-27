using Microsoft.Extensions.DependencyInjection;

namespace ClientApiConfig
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiWithCors(this IServiceCollection services)
        {
            services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5003/"); // WebApiWithCors
            });

            return services;
        }

        public static IServiceCollection AddApiWithoutCors(this IServiceCollection services)
        {
            services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5002/"); //WebApiWithoutCors
            });

            return services;
        }


    }
}