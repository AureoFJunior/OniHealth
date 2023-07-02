using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;
using OniHealth.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Web.Config;
using OniHealth.Domain.Caching;
using Microsoft.Extensions.Configuration;

namespace OniHealth.Application.DI
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string conection, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conection));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var mapper = MapperConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Repositorys
            services.AddScoped(typeof(IRepository<Employer>), typeof(EmployerRepository));
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IRepository<Customer>), typeof(CustomerRepository));
            services.AddScoped(typeof(IRepository<ConsultTime>), typeof(ConsultTimeRepository));
            services.AddScoped(typeof(IRepository<ConsultType>), typeof(ConsultTypeRepository));
            services.AddScoped(typeof(IRepository<Customer>), typeof(CustomerRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepositoryRoles), typeof(RolesRepository));
            services.AddTransient(typeof(IRepositoryConsult), typeof(ConsultRepository));
            services.AddTransient(typeof(IRepositoryPlans), typeof(PlansRepository));
            #endregion

            #region Services
            services.AddScoped(typeof(IEmployerService<Employer>), typeof(EmployerService));
            services.AddScoped(typeof(IRolesService<Roles>), typeof(RolesService));
            services.AddScoped(typeof(ICustomerService<Customer>), typeof(CustomerService));
            services.AddScoped(typeof(IPlanService<Plans>), typeof(PlansService));
            services.AddScoped(typeof(TokenService));
            services.AddScoped(typeof(IUserService<User>), typeof(UserService));
            services.AddScoped(typeof(IConsultService<Consult>), typeof(ConsultService));
            services.AddScoped(typeof(IConsultTimeService<ConsultTime>), typeof(ConsultTimeService));
            services.AddScoped(typeof(IConsultTypeService<ConsultType>), typeof(ConsultTypeService));
            services.AddScoped(typeof(ICachingService), typeof(CachingService));
            #endregion

            services.AddScoped(typeof(IUnitOfWork<AppDbContext>), typeof(UnitOfWork<AppDbContext>));

            services.AddScoped(typeof(IValidator), typeof(Validator));

            services.AddStackExchangeRedisCache(o =>
            {
                o.InstanceName = "onihealthRedis";
                o.Configuration = configuration.Get;
            });
        }
    }
}