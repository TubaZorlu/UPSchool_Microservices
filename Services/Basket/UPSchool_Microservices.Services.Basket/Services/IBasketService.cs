using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Dtos;
using UPSchool_Microservices.Services.Basket.Dtos;

namespace UPSchool_Microservices.Services.Basket.Services
{
	public interface IBasketService
	{
		Task<ResponseDto<BasketDto>> GetBasket(string userId);

		Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);

		Task<ResponseDto<bool>> Delete(string userId);
		

		
	}
}
