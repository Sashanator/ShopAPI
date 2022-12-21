namespace ShopAPI.Features.Products.RequestHandling.Dto;

public class UpdateProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public long Count { get; set; }
}