using Application.Features.Auths.Rules;
using Application.Features.Products.Rules;
using Application.Pipelines;
using Application.Utils.Hashing;
using Application.Utils.Hashing.Models;
using Application.Utils.JWT;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationDependencyResolver
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly: Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSingleton<IHashingTool, SHA512HashingTool>();
            services.AddSingleton<IHashingTool, SHA512HashingTool>();
            services.AddSingleton<ITokenHelper,JwtTokenHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthRuleManager>();
            services.AddScoped<ProductRuleManager>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
        }
    }
}
