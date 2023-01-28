using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UpSchollECommerce.Shared.Serrvices
{
	public class SharedIdentityService : IsharedIdentityService
	{
		private IHttpContextAccessor _httpContextAccessor;

		public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		//token ın içersindeki sub'ı almış olduk //yani Id yi
		public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
	}
}
