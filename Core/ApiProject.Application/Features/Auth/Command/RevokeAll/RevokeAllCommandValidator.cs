using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Command.RevokeAll
{
	public class RevokeAllCommandValidator : AbstractValidator<RevokeAllCommandRequest>
	{
        public RevokeAllCommandValidator()
        {          
        }
    }
}
