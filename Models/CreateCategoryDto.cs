using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Entities;

namespace Api.Models;

public class CreateCategoryDto
{
   public Guid Id { get; set; }
   public string Name { get; set; }
   public Guid? ParentId { get; set; }
   public List<CreateCategoryDto>? Children { get; set; }
   public List<Product> Products { get; set; }
}
