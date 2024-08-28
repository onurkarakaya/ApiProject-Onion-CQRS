using ApiProject.Application.Bases;
using ApiProject.Application.Features.Auth.Rules;
using ApiProject.Application.Interfaces.AutoMapper;
using ApiProject.Application.Interfaces.Tokens;
using ApiProject.Application.Interfaces.UnitOfWorks;
using ApiProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Command.RevokeAll
{
	public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommandRequest, Unit>
	{
		private readonly UserManager<User> _userManager;
		private readonly AuthRules _authRules;

		public RevokeAllCommandHandler(UserManager<User> userManager, AuthRules authRules, ITokenService tokenService, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
			: base(mapper, unitOfWork, httpContextAccessor)
		{
			_userManager = userManager;
			_authRules = authRules;
		}

		public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
		{
			var users = await _userManager.Users.ToListAsync(cancellationToken);

			foreach (var user in users)
			{
				user.RefreshToken = null;
				await _userManager.UpdateAsync(user);
			}

			return Unit.Value;
		}
	}
}
