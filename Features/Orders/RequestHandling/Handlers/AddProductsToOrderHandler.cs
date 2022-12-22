using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShopAPI.Features.Orders.RequestHandling.Requests;
using ShopAPI.Features.Orders.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Orders.RequestHandling.Handlers
{
    public class AddProductsToOrderHandler : IRequestHandler<AddProductsToOrderRequest, Response>
    {
        private readonly IOrderService _orderService;
        public AddProductsToOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Response> Handle(AddProductsToOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _orderService.AddProductsToOrder(request.OrderId, request.ProductIds, cancellationToken);
                return Response.Ok(request.Id);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
