using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchollECommerce.Shared.Dtos;
using UPSchoolECommerce.Services.Catalog.Dtos;
using UPSchoolECommerce.Services.Catalog.Models;
using UPSchoolECommerce.Services.Catalog.Settings;

namespace UPSchoolECommerce.Services.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;

        public ProductService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }
        public async Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(product);
            return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string Id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == Id);
            if (result.DeletedCount>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            else
            {
                return ResponseDto<NoContent>.Fail("Silinecek ürün bulunamadı", 404);
            }
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(prodyuct => true).ToListAsync();
            return ResponseDto<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string Id)
        {
            var product = await _productCollection.Find<Product>(x => x.Id == Id).FirstOrDefaultAsync();
            if (product==null)
            {
                return ResponseDto<ProductDto>.Fail("Girilen Id ye ait bir ürün bulunamadı", 404);
            }
            else
            {
                return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
            }
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var uptatedProduct = _mapper.Map<Product>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == updateProductDto.Id, uptatedProduct);

            if (result==null)
            {
                return ResponseDto<NoContent>.Fail("Güncellencek Id değeri bulunamadı", 404);

            }
            else
            {
                return ResponseDto<NoContent>.Success(204);
            }
        }
    }
}
