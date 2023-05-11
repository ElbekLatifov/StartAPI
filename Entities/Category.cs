using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities;

public class Category
{
   public Guid Id { get; set; }
   public string Name { get; set; }
   public Guid? ParentId { get; set; }
   public Category? Parent { get; set; }
   public List<Category>? Children { get; set; }
   public List<Product>? Products { get; set; }
}