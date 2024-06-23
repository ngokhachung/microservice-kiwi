using Kiwi.Web.Models;
using Kiwi.Web.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kiwi.Web.Controllers
{
	public class ProductController(IProductService productService) : Controller
	{
		private readonly IProductService _productService = productService;

		// GET: ProductController
		public async Task<IActionResult> ProductIndex()
		{
			var response = await _productService.GetAllProductAsync();
			if(response != null && response.IsSuccess)
			{
				var list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Data));
				return View(list);
			}
			else
			{
				TempData["error"] = response?.Message;
				return View();
			}
		}


		// GET: ProductController/Create
		public IActionResult ProductCreate()
		{
			return View();
		}

		// POST: ProductController/Create
		[HttpPost]
		public async Task<IActionResult> ProductCreate(ProductDto product)
		{
			try
			{
				var response = await _productService.CreateProductsAsync(product);
				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Product create successfull";
					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = response?.Message;
					return View();
				}
				
			}
			catch(Exception ex)
			{
				TempData["error"] = ex?.Message;
				return View();
			}
		}

		// GET: ProductController/Edit/5
		public async Task<IActionResult> ProductEdit(int productId)
		{
			try
			{
				var response = await _productService.GetProductByIdAsync(productId);
				if (response != null && response.IsSuccess)
				{
					var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Data));
					return View(product);
				}
				else {
					TempData["error"] = response?.Message;
					return View();
				}
			}
			catch (Exception ex) 
			{
				TempData["error"] = ex?.Message;
				return View();
			}
		}

		// POST: ProductController/Edit/5
		[HttpPost]
		public async Task<IActionResult> ProductEdit(ProductDto product)
		{
			try
			{
				var response = await _productService.UpdateProductsAsync(product);
				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Product edit successfull";
					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = response?.Message;
					return View();
				}
			}
			catch(Exception ex)
			{
				TempData["error"] = ex?.Message;
				return View();
			}
		}

		// GET: ProductController/Delete/5
		public async Task<IActionResult> ProductDelete(int productId)
		{
			try
			{
				var response = await _productService.GetProductByIdAsync(productId);
				if (response != null && response.IsSuccess)
				{
					var result = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Data));
					return View(result);
				}
				else
				{
					TempData["error"] = response?.Message;
					return View();
				}
			}
			catch (Exception ex)
			{
				TempData["error"] = ex?.Message;
				return View();
			}
		}

		// POST: ProductController/Delete/5
		[HttpPost]
		public async Task<IActionResult> ProductDelete(ProductDto productDto)
		{
			try
			{
				var response = await _productService.DeleteProductsAsync(productDto.ProductId);
				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Product delete successfull";
					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = response?.Message;
					return View();
				}
			}
			catch (Exception ex)
			{
				TempData["error"] = ex?.Message;
				return View();
			}
		}
	}
}
