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
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Command.Revoke
{
	public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest, Unit>
	{
		private readonly UserManager<User> _userManager;
		private readonly AuthRules _authRules;

		public RevokeCommandHandler(UserManager<User> userManager, AuthRules authRules, ITokenService tokenService, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
			: base(mapper, unitOfWork, httpContextAccessor)
		{
			_userManager = userManager;
			_authRules = authRules;
		}

		public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			await _authRules.EmailAddressShouldBeValid(user);

			user.RefreshToken = null;
			await _userManager.UpdateAsync(user);

			return Unit.Value;
		}
	}
}
