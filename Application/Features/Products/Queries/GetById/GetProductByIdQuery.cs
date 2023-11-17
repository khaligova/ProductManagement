using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQuery:IRequest<GetProductByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
