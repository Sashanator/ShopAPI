using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Products.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Orders.RequestHandling.Requests
{
    public class AddProductsToOrderRequest : Request<Response>
    {
        public AddProductsToOrderRequest(List<Guid> products, Guid orderId)
        {
            ProductIds = products;
            OrderId = orderId;
        }
        public List<Guid> ProductIds { get; set; }

        public Guid OrderId { get; set; }
    }
}
