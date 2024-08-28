using ApiProject.Application.Interfaces.RedisCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Features.Products.Queries.GetAllProducts
{
	public class GetAllProductsQueryRequest : IRequest<IList<GetAllProductsQueryResponse>>, ICacheableQuery
	{
		public string CacheKey => "GetAllProducts";

		public double CacheTime => 60;
	}
}
