using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Orders.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Orders.RequestHandling.Requests
{
    public class UpdateOrderRequest : Request<Response>
    {
        public UpdateOrderRequest(UpdateOrderDto dto)
        {
            Dto = dto;
        }
        public UpdateOrderDto Dto { get; set; }
    }
}
