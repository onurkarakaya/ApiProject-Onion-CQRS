﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Command.Register
{
	public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
	{
		public RegisterCommandValidator()
		{
			RuleFor(x => x.FullName)
				.NotEmpty()
				.MaximumLength(50)
				.MinimumLength(2)
				.WithName("Isim Soyisim");

			RuleFor(x => x.Email)
				.NotEmpty()
				.MaximumLength(60)
				.MinimumLength(8)
				.EmailAddress()
				.WithName("E-posta Adresi");


			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(6)
				.WithName("Parola");


			RuleFor(x => x.ConfirmPassword)
				.NotEmpty()
				.MinimumLength(6)
				.Equal(x => x.Password)
				.WithName("Parola Tekrari");
		}
	}
}
