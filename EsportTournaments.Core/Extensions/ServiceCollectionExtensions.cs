using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace EsportTournaments.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainService(
            this IServiceCollection services)
        {
            Assembly
                .GetAssembly(typeof(IService))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{i.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }
    }
}
