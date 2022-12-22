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
        public AddProductsToOrderRequest(List<CreateProductDto> products, Guid orderId)
        {
            Products = products;
            OrderId = orderId;
        }
        public List<CreateProductDto> Products { get; set; }

        public Guid OrderId { get; set; }
    }
}
