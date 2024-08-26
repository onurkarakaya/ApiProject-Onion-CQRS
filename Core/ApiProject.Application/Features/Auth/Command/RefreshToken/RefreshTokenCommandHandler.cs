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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Command.RefreshToken
{
	public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly AuthRules _authRules;
		private readonly ITokenService _tokenService;
		private readonly IConfiguration _configuration;

		public RefreshTokenCommandHandler(UserManager<User> userManager, AuthRules authRules, ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
			: base(mapper, unitOfWork, httpContextAccessor)
		{
			_userManager = userManager;
			_tokenService = tokenService;
			_authRules = authRules;
		}
        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
		{
			var principal = _tokenService.GetPrincipalFremExpiredToken(request.AccessToken);

			string email = principal.FindFirstValue(ClaimTypes.Email);

			var user = await _userManager.FindByEmailAsync(email);
			var roles = await _userManager.GetRolesAsync(user);

			await _authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime);

			var newAccessToken = await _tokenService.CreateToken(user, roles);
			string newRefreshToken = _tokenService.GenerateRefreshToken();

			user.RefreshToken = newRefreshToken;
			await _userManager.UpdateAsync(user);

			return new()
			{
				AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
				RefreshToken = newRefreshToken,
			};
		}
	}
}
