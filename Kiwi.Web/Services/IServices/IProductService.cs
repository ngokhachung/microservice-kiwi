using Kiwi.Web.Models;

namespace Kiwi.Web.Services.IServices
{
	public interface IProductService
	{
		Task<ResponseModel?> GetAllProductAsync();
		Task<ResponseModel?> GetProductByIdAsync(int id);
		Task<ResponseModel?> CreateProductsAsync(ProductDto ProductDto);
		Task<ResponseModel?> UpdateProductsAsync(ProductDto ProductDto);
		Task<ResponseModel?> DeleteProductsAsync(int id);
	}
}
