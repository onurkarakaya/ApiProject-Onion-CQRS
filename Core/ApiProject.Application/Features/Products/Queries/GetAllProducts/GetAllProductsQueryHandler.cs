using ApiProject.Application.Interfaces.UnitOfWorks;
using ApiProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiProject.Application.DTOs;
using ApiProject.Application.Interfaces.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Application.Features.Products.Queries.GetAllProducts
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
		{
			var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(include: x => x.Include(p => p.Brand));

			var brand = _mapper.Map<BrandDTO, Brand>(new Brand());

			var map = _mapper.Map<GetAllProductsQueryResponse, Product>(products);

			foreach (var m in map)
			{
				m.Price -= (m.Price * m.Discount / 100);
			}

			return map;
		}
	}
}
