using AutoMapper;
using Kiwi.Service.ProductAPI.Data;
using Kiwi.Service.ProductAPI.Models.Dto;
using Kiwi.Service.ProductAPI.Models;
using Kiwi.Service.ProductAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Kiwi.Service.ProductAPI.Controllers
{
	[Route("api/product")]
	[Authorize]
	public class ProductAPIController(AppDbContext db, IMapper mapper) : Controller
	{
		private readonly AppDbContext _db = db;
		private ResponseDto _response = new();
		private IMapper _mapper = mapper;

		[HttpGet]
		public ResponseDto Index()
		{
			try
			{
				IEnumerable<Product> objList = [.. _db.Products];
				_response.Data = _mapper.Map<IEnumerable<ProductDto>>(objList);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPost]
		[Route("{productId}")]
		public ResponseDto GetById(int productId)
		{
			try
			{
				var product = _db.Products.First(x => x.ProductId == productId);
				_response.Data = _mapper.Map<ProductDto>(product);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPost]
		[Route("create")]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Create([FromBody] ProductDto productDto)
		{
			try
			{
				Product product = _mapper.Map<Product>(productDto);
				_db.Products.Add(product);
				_db.SaveChanges();
				_response.Data = product;
			}
			catch (Exception ex)
			{
				_response.Message = ex.Message;
				_response.IsSuccess = false;
			}
			return _response;
		}

		[HttpPost]
		[Route("delete/{productId:int}")]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Delete(int productId)
		{

			try
			{
				var product = _db.Products.First(x => x.ProductId == productId);
				_db.Products.Remove(product);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				_response.Message = ex.Message;
				_response.IsSuccess = false;
			}
			return _response;
		}

		[HttpPost]
		[Route("update")]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Update([FromBody] ProductDto productDto)
		{
			try
			{
				var product = _db.Products.First(x => x.ProductId == productDto.ProductId);
				product.ImageLocalPath = productDto.ImageLocalPath;
				product.Price = productDto.Price;
				product.Description = productDto.Description;
				product.CategoryName = productDto.CategoryName;
				product.ImageUrl = productDto.ImageUrl;
				product.Name = productDto.Name;
				_db.SaveChanges();
			}
			catch (Exception ex) 
			{
				_response.Message = ex.Message;
				_response.IsSuccess = false;
			}
			return _response;
		}
	}
}
