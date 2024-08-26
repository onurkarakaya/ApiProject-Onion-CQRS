using ApiProject.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Auth.Exceptions
{
	public class UserAlreadyExistException : BaseException
	{
        public UserAlreadyExistException() : base("Boyle bir kullanici zaten var!")
        {
            
        }
    }
}
