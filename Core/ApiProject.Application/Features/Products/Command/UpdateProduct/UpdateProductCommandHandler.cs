﻿using ApiProject.Application.Interfaces.AutoMapper;
using ApiProject.Application.Interfaces.UnitOfWorks;
using ApiProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Products.Command.UpdateProduct
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id & !p.IsDeleted);

			var map = _mapper.Map<Product, UpdateProductCommandRequest>(request);

			var productCategories = await _unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(pc => pc.ProductId == product.Id);

			await _unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);

			foreach (var categoryId in request.CategoryIds)
			{
				await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new() { ProductId = product.Id, CategoryId = categoryId });
			}

			await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);

			await _unitOfWork.SaveAsync();
		}
	}
}
