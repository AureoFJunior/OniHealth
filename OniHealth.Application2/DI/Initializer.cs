using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;
using OniHealth.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OniHealth.Application.DI
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string conection)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conection));

            services.AddScoped(typeof(IRepository<Employer>), typeof(EmployerRepository));
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(EmployerService));
            services.AddScoped(typeof(TokenService));
            services.AddScoped(typeof(UserService));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}