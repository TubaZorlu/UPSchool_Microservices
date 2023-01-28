using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.ControllerBases;
using UpSchollECommerce.Shared.Dtos;
using UPSchool_Microservices.Services.PhotoStock.Dtos;

namespace UPSchool_Microservices.Services.PhotoStock.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhotosController : CustomBaseController
	{
		[HttpPost]
		public async Task<IActionResult> PhotoSave(IFormFile formFile) 
		{
			if (formFile!= null && formFile.Length>0)
			{
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", formFile.FileName);
				var stream = new FileStream(path, FileMode.Create);
				await formFile.CopyToAsync(stream);
				var returnPath = "photos/"+ formFile.FileName;

				PhotoDto photoDto = new() {

					stringUrl = returnPath
				};

				return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(photoDto, 200));
			}

			return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail("hata oluştu", 400));
		}
	}
}
