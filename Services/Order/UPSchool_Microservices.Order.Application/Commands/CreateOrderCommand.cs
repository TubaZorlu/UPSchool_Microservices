using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Dtos;
using UPSchool_Microservices.Order.Application.Dtos;

namespace UPSchool_Microservices.Order.Application.Commands
{
	public class CreateOrderCommand:IRequest<ResponseDto<CreatedOrderDto>>
	{
		public string BuyerId { get; set; }
		public AdressDto Adress { get; set; }
		public List<OrderItemDto> OrderItems { get; set; }
	}
}
