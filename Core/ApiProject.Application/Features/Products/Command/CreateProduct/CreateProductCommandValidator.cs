using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Products.Command.CreateProduct
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
	{
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithName("Baslik");
            
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithName("Aciklama");

            RuleFor(p => p.BrandId)
                .GreaterThan(0)
				.WithName("Marka");
            
            RuleFor(p => p.Price)
                .GreaterThan(0)
				.WithName("Fiyat");

			RuleFor(p => p.Discount)
			   .GreaterThanOrEqualTo(0)
			   .WithName("Indirim");

            RuleFor(p => p.CategoryIds)
                .NotEmpty()
                .Must(categories => categories.Any())
                .WithName("Kategoriler");
		}
    }
}
