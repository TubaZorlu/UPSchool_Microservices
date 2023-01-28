using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Dtos;
using UPSchool_Microservices.Order.Application.Dtos;

namespace UPSchool_Microservices.Order.Application.Queries
{
	public class GetOrdersByUerIDQuery : IRequest<ResponseDto<List<OrderDto>>>
	{
		public string UserId { get; set; }
	}
}
