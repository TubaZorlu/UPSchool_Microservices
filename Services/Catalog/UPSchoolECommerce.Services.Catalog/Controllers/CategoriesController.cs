using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.ControllerBases;
using UPSchoolECommerce.Services.Catalog.Dtos;
using UPSchoolECommerce.Services.Catalog.Services;

namespace UPSchoolECommerce.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategorySerrvice _categorySerrvice;

        public CategoriesController(ICategorySerrvice categorySerrvice)
        {
            _categorySerrvice = categorySerrvice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var response = await _categorySerrvice.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        //localhost5011//api/categories/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categorySerrvice.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }


       
        [HttpPost]
        public async Task<IActionResult> GetById(CategoryDto categoryDto )
        {
            var response = await _categorySerrvice.CreateAsync(categoryDto);
            return CreateActionResultInstance(response);
        }
    }
}
