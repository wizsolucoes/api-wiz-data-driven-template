using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;
using Wiz.Template.API.Infra.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Wiz.Template.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<EntityContext>();

                context.Database.Migrate();

                new EntityContextSeed().SeedInitial(context);
            }

            return host;
        }
    }
}
