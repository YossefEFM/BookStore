using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();


    services.AddScoped<IAuthService, AuthService>();

        return services;
    }


}
