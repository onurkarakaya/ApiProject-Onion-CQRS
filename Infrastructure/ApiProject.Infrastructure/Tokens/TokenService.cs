﻿using ApiProject.Application.Interfaces.Tokens;
using ApiProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.Tokens
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<User> _userManager;
		private readonly TokenSettings _tokenSettings;

		public TokenService(IOptions<TokenSettings> options, UserManager<User> userManager)
		{
			_tokenSettings = options.Value;
			_userManager = userManager;
		}

		public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
		{
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));

			var token = new JwtSecurityToken(
				issuer: _tokenSettings.Issuer,
				audience: _tokenSettings.Audience,
				expires: DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMinutes),
				claims: claims,
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
				);

			await _userManager.AddClaimsAsync(user, claims);

			return token;
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];

			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		public ClaimsPrincipal? GetPrincipalFremExpiredToken(string? token)
		{
			TokenValidationParameters tokenValidationParameters = new()
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret)),
				ValidateLifetime = false
			};

			JwtSecurityTokenHandler tokenHandler = new();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
			if (securityToken is not JwtSecurityToken jwtSecurityToken 
				|| !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Token bulunamadi.");
			}

			return principal;
		}
	}
}
