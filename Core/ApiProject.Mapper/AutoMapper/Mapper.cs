using ApiProject.Application.Interfaces.AutoMapper;
using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMapper = AutoMapper.IMapper;

namespace ApiProject.Mapper.AutoMapper
{
	public class Mapper : Application.Interfaces.AutoMapper.IMapper
	{
		public static List<TypePair> TypePairs = new();
		private IMapper _mapperContainer;



		public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
		{
			Config<TDestination, TSource>(5, ignore);
			return _mapperContainer.Map<TSource, TDestination>(source);
		}

		public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
		{
			Config<TDestination, TSource>(5, ignore);
			//_mapperContainer.Map<>()
			return _mapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
		}

		public TDestination Map<TDestination>(object source, string? ignore = null)
		{
			Config<TDestination, object>(5, ignore);
			return _mapperContainer.Map<TDestination>(source);
		}

		public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
		{
			Config<TDestination, IList<object>>(5, ignore);
			return _mapperContainer.Map<IList<TDestination>>(source);
		}

		protected void Config<TDestination, TSource>(int depth = 5, string? ignore = null)
		{
			var typePair = new TypePair(typeof(TSource), typeof(TDestination));

			if (TypePairs.Any(tp => tp.DestinationType == typePair.DestinationType && tp.SourceType == typePair.SourceType) && ignore is null)
			{
				return;
			}

			TypePairs.Add(typePair);

			var config = new MapperConfiguration(cfg =>
			{
				foreach (var tp in TypePairs)
				{
					if (ignore is not null)
					{
						cfg.CreateMap(tp.SourceType, tp.DestinationType).MaxDepth(depth)
							.ForMember(ignore, x => x.Ignore()).ReverseMap();
					}
					else
					{
						cfg.CreateMap(tp.SourceType, tp.DestinationType).MaxDepth(depth).ReverseMap();
					}
				}
			});

			_mapperContainer = config.CreateMapper();
		}
	}
}
