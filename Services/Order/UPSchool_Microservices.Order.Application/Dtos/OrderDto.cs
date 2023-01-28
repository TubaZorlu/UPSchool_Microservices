using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPSchool_Microservices.Order.Application.Dtos
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public DateTime CreatedDate { get; set; }
		public AdressDto Adress { get; set; }
		public string BuyerId { get; set; }

		public  List<OrderItemDto> _orderItems;
	}
}
