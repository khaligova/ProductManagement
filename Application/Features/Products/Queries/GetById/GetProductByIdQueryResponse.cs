using Application.Features.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryResponse
    {
        public ProductDto ProductDto { get; set; }
    }
}
