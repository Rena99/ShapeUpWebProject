using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<ShapeUp>(options =>
            options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\new project\ShapeUpDBProject\DB\ShapeUp.mdf;Integrated Security=True;Connect Timeout=30"), ServiceLifetime.Scoped);
            //options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rina lerner\Documents\ShapeUPProject\ShapeUpDBProject\DB\ShapeUp.mdf;Integrated Security=True;Connect Timeout=30"), ServiceLifetime.Scoped);
            return services;
        }
    }
}
