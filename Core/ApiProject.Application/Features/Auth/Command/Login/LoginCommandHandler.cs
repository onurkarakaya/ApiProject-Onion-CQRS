using ApiProject.Application.Bases;
using ApiProject.Application.Features.Auth.Rules;
using ApiProject.Application.Interfaces.AutoMapper;
using ApiProject.Application.Interfaces.Tokens;
using ApiProject.Application.Interfaces.UnitOfWorks;
using ApiProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Command.Login
{
	public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly AuthRules _authRules;
		private readonly ITokenService _tokenService;
		private readonly IConfiguration _configuration;

		public LoginCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager, AuthRules authRules, ITokenService tokenService, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
			: base(mapper, unitOfWork, httpContextAccessor)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_authRules = authRules;
			_tokenService = tokenService;
			_configuration = configuration;	
		}

		public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

			await _authRules.EMailOrPasswordShouldNotBeInvalid(user, checkPassword);

			var roles = await _userManager.GetRolesAsync(user);

			JwtSecurityToken token = await _tokenService.CreateToken(user, roles);

			string refreshToken = _tokenService.GenerateRefreshToken();

			_ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

			await _userManager.UpdateAsync(user);
			await _userManager.UpdateSecurityStampAsync(user);

			string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", tokenString);

			return new()
			{
				Token = tokenString,
				RefreshToken = refreshToken,
				Expiration = token.ValidTo
			};
		}
	}
}
