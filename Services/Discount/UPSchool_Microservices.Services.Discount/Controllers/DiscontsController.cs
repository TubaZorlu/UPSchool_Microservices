using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.ControllerBases;
using UpSchollECommerce.Shared.Serrvices;
using UPSchool_Microservices.Services.Discount.Services;

namespace UPSchool_Microservices.Services.Discount.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiscontsController : CustomBaseController
	{
		private readonly IDiscountService _discountService;
		private readonly IsharedIdentityService ısharedIdentityService;

		public DiscontsController(IDiscountService discountService, IsharedIdentityService ısharedIdentityService)
		{
			_discountService = discountService;
			this.ısharedIdentityService = ısharedIdentityService;

		}
		[HttpGet]
		public async Task<IActionResult> GetAll() 
		{
			return CreateActionResultInstance(await _discountService.GetAll());
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var discout = await _discountService.GetById(id);
			return CreateActionResultInstance(discout);
		}

		[HttpPost]
		public async Task<IActionResult> Save(Models.Discount discount)
		{
			return CreateActionResultInstance(await _discountService.Save(discount));
		}


		[HttpPut]
		public async Task<IActionResult> Update(Models.Discount discount)
		{
			return CreateActionResultInstance(await _discountService.Update(discount));
		}



		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			return CreateActionResultInstance(await _discountService.Delete(id));
		}
	}
}
