using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;
using OniHealth.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Web.Config;

namespace OniHealth.Application.DI
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string conection)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conection));

            var mapper = MapperConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped(typeof(IRepository<Employer>), typeof(EmployerRepository));
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IRepository<Customer>), typeof(CustomerRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepositoryRoles), typeof(RolesRepository));
            services.AddTransient(typeof(IRepositoryConsult), typeof(ConsultRepository));


            services.AddScoped(typeof(IEmployerService<Employer>), typeof(EmployerService));
            services.AddScoped(typeof(IRolesService<Roles>), typeof(RolesService));
            services.AddScoped(typeof(ICustomerService<Customer>), typeof(CustomerService));
            services.AddScoped(typeof(TokenService));
            services.AddScoped(typeof(IUserService<User>), typeof(UserService));
            services.AddScoped(typeof(IConsultService<Consult>), typeof(ConsultService));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IValidator), typeof(Validator));
        }
    }
}