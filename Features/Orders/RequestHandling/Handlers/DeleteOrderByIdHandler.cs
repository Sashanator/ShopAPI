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
    public class DeleteOrderByIdHandler : IRequestHandler<DeleteOrderByIdRequest, Response>
    {
        private readonly IOrderService _orderService;
        public DeleteOrderByIdHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Response> Handle(DeleteOrderByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _orderService.DeleteOrderById(request.OrderId, cancellationToken);
                return Response.Ok(request.Id);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
