using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.ControllerBases;
using UpSchollECommerce.Shared.Serrvices;
using UPSchool_Microservices.Services.Basket.Dtos;
using UPSchool_Microservices.Services.Basket.Services;

namespace UPSchool_Microservices.Services.Basket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketsController : CustomBaseController
	{
		private readonly IBasketService _basketService; 
		private readonly IsharedIdentityService _sharedIdentityService;

		public BasketsController(IBasketService basketService, IsharedIdentityService sharedIdentityService)
		{
			_basketService = basketService;
			_sharedIdentityService = sharedIdentityService;
		}

		[HttpGet]
		public async Task<IActionResult> GetBasket() 
		{
			return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
		}
		[HttpPost]
		public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto) 
		{
			basketDto.UserId = _sharedIdentityService.GetUserId;
			var response = await _basketService.SaveOrUpdate(basketDto);
			return CreateActionResultInstance(response);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
		}
		//portainer için dene
		//http://localhost:9000/#!/2/out

	}
}
