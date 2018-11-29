using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using ThinkNoteBackEnd.Persistence.Config;
using ThinkNoteBackEnd.Persistence.User;

namespace ThinkNoteBackEnd.Persistence
{
    public static class PersistenceServicesCollectionExtension
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(assm => assm.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IPersistence))))
                        .Where(x => x.IsClass)
                        .ForEachService(x => services.AddScoped(x));
            return services;
        }
        public static IEnumerable<T> ForEachService<T>(this IEnumerable<T> serv, Action<T> action)
        {
            foreach (var item in serv)
            {
                action(item);
            }
            return serv;
        }
    }
}
