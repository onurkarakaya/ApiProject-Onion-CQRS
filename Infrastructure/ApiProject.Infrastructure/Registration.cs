using ApiProject.Application.Interfaces.RedisCache;
using ApiProject.Application.Interfaces.Tokens;
using ApiProject.Infrastructure.RedisCache;
using ApiProject.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure
{
	public static class Registration
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<TokenSettings>(configuration.GetSection("JWT"));
			services.AddTransient<ITokenService, TokenService>();

			services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));
			services.AddTransient<IRedisCacheService, RedisCacheService>();

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
			{
				opt.SaveToken = true;
				opt.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
					ValidateLifetime = false,
					ValidIssuer = configuration["JWT:Issuer"],
					ValidAudience = configuration["JWT:Audience"],
					ClockSkew = TimeSpan.Zero
				};
			});

			services.AddStackExchangeRedisCache(opt =>
			{
				opt.Configuration = configuration["RedisCacheSettings:ConnectionString"];
				opt.InstanceName = configuration["RedisCacheSettings:InstanceName"];
			});
		}
	}
}
