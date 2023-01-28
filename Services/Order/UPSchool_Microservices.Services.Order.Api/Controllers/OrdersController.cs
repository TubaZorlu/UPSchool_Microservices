using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.ControllerBases;
using UpSchollECommerce.Shared.Serrvices;
using UPSchool_Microservices.Order.Application.Commands;
using UPSchool_Microservices.Order.Application.Queries;

namespace UPSchool_Microservices.Services.Order.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : CustomBaseController
	{
		private readonly IMediator _mediator;
		private readonly IsharedIdentityService _sharedIdentityService;

		public OrdersController(IMediator mediator, IsharedIdentityService sharedIdentityService)
		{
			_mediator = mediator;
			_sharedIdentityService = sharedIdentityService;
		}


		[HttpGet]
		public async Task<IActionResult> GetOrders()
		{
			var response = await _mediator.Send(new GetOrdersByUerIDQuery
			{
				UserId = _sharedIdentityService.GetUserId
			});

			return CreateActionResultInstance(response);
		}


		[HttpPost]
		public async Task<IActionResult> SaveOrders(CreateOrderCommand createOrderCommand)
		{
			var response = await _mediator.Send(createOrderCommand);
			return CreateActionResultInstance(response);

		}
	}
}
