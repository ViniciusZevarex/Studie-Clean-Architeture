using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using CleanArch.Domain.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using CleanArch.Application.DTOs;
using CleanArch.Infra.Data.Identity;
using CleanArch.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );



            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(options =>
                    options.AccessDeniedPath = "/Account/Login");


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();


            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();



            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));


            var myhandlers = AppDomain.CurrentDomain.Load("CleanArch.Application");
            services.AddMediatR(myhandlers);

            return services;
        }

    }
}
