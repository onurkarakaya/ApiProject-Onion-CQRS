﻿using ApiProject.Application.Bases;
using ApiProject.Application.Exceptions;
using ApiProject.Application.Features.Auth.Exceptions;
using ApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Rules
{
	public class AuthRules : BaseRules
	{
		public Task UserShouldNotBeExist(User? user)
		{
			if (user is not null)
			{
				throw new UserAlreadyExistException();
			}

			return Task.CompletedTask;
		}

		public Task EMailOrPasswordShouldNotBeInvalid(User? user, bool checkPassword)
		{
			if (user is null || !checkPassword)
			{
				throw new EMailOrPasswordShouldNotBeInvalidException();
			}

			return Task.CompletedTask;
		}

		public Task RefreshTokenShouldNotBeExpired(DateTime? expiryDate)
		{
			if (expiryDate <= DateTime.Now)
			{
				throw new RefreshTokenShouldNotBeExpiredException();
			}

			return Task.CompletedTask;
		}

		public Task EmailAddressShouldBeValid(User? user)
		{
			if (user is null)
			{
				throw new EmailAddressShouldBeValidException();
			}

			return Task.CompletedTask;
		}


	}
}
