using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThinkNoteBackEnd.Persistence
{
    public static class PersistenceServicesCollectionExtension
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,Type FlagInstanceType)
        {
            AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(assm => assm.GetTypes().Where(t => t.GetInterfaces().Contains(FlagInstanceType)))
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
