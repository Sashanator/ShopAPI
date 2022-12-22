using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Features.Orders.RequestHandling.Dto
{
    public class ProductsDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
