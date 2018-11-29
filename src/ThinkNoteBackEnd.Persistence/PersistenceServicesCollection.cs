using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ThinkNoteBackEnd.Persistence.Config;
using ThinkNoteBackEnd.Persistence.User;

namespace ThinkNoteBackEnd.Persistence
{
    public static class PersistenceServicesCollectionExtension
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped(prov => new PersistUserFileServices(prov.GetService<IOptions<PersistenceConfigurationModel>>()));
            return services;
        }
    }
}
