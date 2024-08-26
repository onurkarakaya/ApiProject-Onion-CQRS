﻿using ApiProject.Application.Features.Auth.Command.Login;
using ApiProject.Application.Features.Auth.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ApiProject.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
			_mediator = mediator;
        }

		[HttpPost]
		public async Task<IActionResult> Register(RegisterCommandRequest request)
		{
			await _mediator.Send(request);

			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginCommandRequest request)
		{
			var response = await _mediator.Send(request);

			return StatusCode(StatusCodes.Status200OK, response);
		}
	}
}
