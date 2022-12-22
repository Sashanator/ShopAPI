using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Common;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Orders.RequestHandling.Requests
{
    public class GetOrdersRequest : Request<Response>
    {
        public GetOrdersRequest(PaginationDto dto)
        {
            Dto = dto;
        }
        public PaginationDto Dto { get; set; }
    }
}
