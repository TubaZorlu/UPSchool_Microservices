using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Dtos;
using UPSchoolECommerce.Services.Catalog.Dtos;

namespace UPSchoolECommerce.Services.Catalog.Services
{
    public interface IProductService
    {
        Task<ResponseDto<List<ProductDto>>> GetAllAsync();
        Task<ResponseDto<ProductDto>> GetByIdAsync(string Id);
        Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto createProductDto);
        Task<ResponseDto<NoContent>> UpdateAsync(UpdateProductDto updateProductDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string Id);


    }
}
