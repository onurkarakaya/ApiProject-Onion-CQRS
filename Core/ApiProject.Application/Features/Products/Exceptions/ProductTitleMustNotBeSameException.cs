﻿using ApiProject.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Products.Exceptions
{
	public class ProductTitleMustNotBeSameException : BaseException
	{
        public ProductTitleMustNotBeSameException() : base("Urun basligi zaten var!")
        {
            
        }
    }
}
