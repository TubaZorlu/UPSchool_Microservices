using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Dtos;
using UPSchool_Microservices.Order.Application.Commands;
using UPSchool_Microservices.Order.Application.Dtos;
using UPSchool_Microservices.Order.Domain.OrderAggregate;
using UPSchool_Microservices.Order.Infrastructure;

namespace UPSchool_Microservices.Order.Application.Handlers
{
	public class CreateOrderCommandHadler : IRequestHandler<CreateOrderCommand, ResponseDto<CreatedOrderDto>>
	{
		private readonly OrderDbContext _orderDbContext;

		public CreateOrderCommandHadler(OrderDbContext orderDbContext)
		{
			_orderDbContext = orderDbContext;
		}

		public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var newAdress = new Adress(request.Adress.City, request.Adress.Street, request.Adress.District, request.Adress.ZipCode);

			Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAdress);

			request.OrderItems.ForEach(x =>
			{
				newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
			});


			await _orderDbContext.Orders.AddAsync(newOrder);
			await _orderDbContext.SaveChangesAsync();
			 return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto {OrderId=newOrder.Id }, 204);

		}
	}
}
