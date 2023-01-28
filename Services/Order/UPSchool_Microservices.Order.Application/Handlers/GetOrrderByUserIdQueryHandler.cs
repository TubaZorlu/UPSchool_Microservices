using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Dtos;
using UPSchool_Microservices.Order.Application.Dtos;
using UPSchool_Microservices.Order.Application.Mapping;
using UPSchool_Microservices.Order.Application.Queries;
using UPSchool_Microservices.Order.Infrastructure;

namespace UPSchool_Microservices.Order.Application.Handlers
{
	public class GetOrrderByUserIdQueryHandler : IRequestHandler<GetOrdersByUerIDQuery, ResponseDto<List<OrderDto>>>
	{
		private readonly OrderDbContext _orderDbContext;
		
		public GetOrrderByUserIdQueryHandler(OrderDbContext orderDbContext  )
		{
			_orderDbContext = orderDbContext;
		
		}

		public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrdersByUerIDQuery request, CancellationToken cancellationToken)
		{
			var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

			if (!orders.Any())
			{
				return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(), 200);
			}

			var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

			return ResponseDto<List<OrderDto>>.Success(ordersDto, 200);

		}



	}
}
