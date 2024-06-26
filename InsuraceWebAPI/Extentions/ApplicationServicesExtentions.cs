using InsuranceWebAPI.Data.Repositories;
using InsuranceWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using InsuranceWebAPI.Interfaces;
using InsuranceWebAPI.Services;

namespace InsuranceWebAPI.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=insurance.db"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInsurancePolicyRepository, InsurancePolicyRepository>();
            services.AddScoped<IInsurancePolicyService, InsurancePolicyService>();
            services.AddScoped(typeof(Lazy<>), typeof(Lazy<>));

            return services;

        }
    }
}
