using Application.Abstractions.Persistence.Repositories.Products;
using Application.CrossCuttingConcerns.Exceptions.Types;
using Application.Features.Auths.Commands.Login;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.DeleteById;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetAll;
using Application.Features.Products.Queries.GetById;
using Application.Filters.Authorization;
using Azsoft.ProductManagement.Controllers.Commons;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Azsoft.ProductManagement.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        [Claim("Product.Get")]
        public IActionResult GetAll()
        {
            GetAllProductsQuery query = new();
            var result = Mediatr.Send(query).Result;
            return Ok(result);
        }

        [HttpGet]
        [Claim("Product.Get")]
        public IActionResult GetById([FromQuery] GetProductByIdQuery query)
        {
            var result = Mediatr.Send(query).Result;
            return Ok(result);
        }

        [HttpPost]
        [Claim("Product.Create")]
        public IActionResult Create([FromBody] CreateProductCommand command)
        {
            var result = Mediatr.Send(command).Result;
            return Ok(result);
        }

        [HttpDelete]
        [Claim("Product.Delete")]
        public IActionResult Delete([FromQuery] DeleteProductByIdCommand command)
        {
            var result = Mediatr.Send(command).Result;
            return Ok(result);
        }

        [HttpPut]
        [Claim("Product.Update")]
        public IActionResult Update([FromBody] UpdateProductCommand command)
        {
            var result = Mediatr.Send(command).Result;
            return Ok(result);
        }
    }
}
