using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PatientRegistration.Data;
using PatientRegistrations.Domain.Agreement;
using PatientRegistrations.Domain.Interfaces;
using PatientRegistrations.Domain.Patient;

namespace PatientRegistration.DI
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = false
            });

            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(PatientStore));
            services.AddScoped(typeof(AgreementStore));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        }
    }
}
