
namespace Api.Models;

public class ProductDto
{
    public Guid Id { get; set;}
    public string Name { get; set; }
    public IFormFile PhotoUrl { get; set; }
    public int Price { get; set; }
    public Guid CategoryId { get; set; }
}