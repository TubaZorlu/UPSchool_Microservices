using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPSchool_Microservices.Order.Application.Dtos;

namespace UPSchool_Microservices.Order.Application.Mapping
{
	public class CustomMapping:Profile
	{
		public CustomMapping()
		{
			CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
			CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
			CreateMap<Domain.OrderAggregate.Adress, AdressDto>().ReverseMap();

		}
	}
}
