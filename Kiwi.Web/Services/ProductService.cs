using Kiwi.Web.Models;
using Kiwi.Web.Services.IServices;
using Kiwi.Web.Utilites;

namespace Kiwi.Web.Services
{
	public class ProductService(IBaseService baseService) : IProductService
	{
		private readonly IBaseService _baseService = baseService;

		public async Task<ResponseModel?> CreateProductsAsync(ProductDto ProductDto)
		{
			return await _baseService.SendAsync(new RequestModel()
			{
				ApiType = SD.ApiType.POST,
				Data = ProductDto,
				Url = SD.ProductAPIBase + "/api/product/create"
			});
		}

		public async Task<ResponseModel?> DeleteProductsAsync(int productId)
		{
			return await _baseService.SendAsync(new RequestModel()
			{
				ApiType = SD.ApiType.POST,
				Url = SD.ProductAPIBase + "/api/product/delete/" + productId
			});
		}

		public async Task<ResponseModel?> GetAllProductAsync()
		{
			return await _baseService.SendAsync(new RequestModel()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/api/product"
			});
		}

		public async Task<ResponseModel?> GetProductByIdAsync(int productId)
		{
			return await _baseService.SendAsync(new RequestModel()
			{
				ApiType = SD.ApiType.POST,
				Url = SD.ProductAPIBase + "/api/product/" + productId
			});
		}

		public async Task<ResponseModel?> UpdateProductsAsync(ProductDto ProductDto)
		{
			return await _baseService.SendAsync(new RequestModel()
			{
				ApiType = SD.ApiType.POST,
				Data = ProductDto,
				Url = SD.ProductAPIBase + "/api/product/update"
			});
		}
	}
}
