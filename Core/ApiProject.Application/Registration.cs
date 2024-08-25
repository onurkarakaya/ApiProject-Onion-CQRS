using ApiProject.Application.Bases;
using ApiProject.Application.Beheviors;
using ApiProject.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application
{
	public static class Registration
	{
		public static void AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddTransient<ExceptionMiddleware>();

			services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

			services.AddValidatorsFromAssembly(assembly);
			ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
		}

	    private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly, Type type)
		{
			var types = assembly.GetTypes().Where(t=> t.IsSubclassOf(type) && type != t).ToList();
            foreach (var t in types)
            {
				services.AddTransient(t);
            }

			return services;
        }
	}
}
