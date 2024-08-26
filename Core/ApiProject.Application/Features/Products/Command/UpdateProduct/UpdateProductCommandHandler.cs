using ApiProject.Application.Bases;
using ApiProject.Application.Interfaces.AutoMapper;
using ApiProject.Application.Interfaces.UnitOfWorks;
using ApiProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Products.Command.UpdateProduct
{
	public class UpdateProductCommandHandler : BaseHandler, IRequestHandler<UpdateProductCommandRequest, Unit>
	{
		public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
			: base(mapper, unitOfWork, httpContextAccessor)
		{
		}

		public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id & !p.IsDeleted);

			var map = mapper.Map<Product, UpdateProductCommandRequest>(request);

			var productCategories = await unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(pc => pc.ProductId == product.Id);

			await unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);

			foreach (var categoryId in request.CategoryIds)
			{
				await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new() { ProductId = product.Id, CategoryId = categoryId });
			}

			await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);

			await unitOfWork.SaveAsync();

			return Unit.Value;
		}
	}
}
