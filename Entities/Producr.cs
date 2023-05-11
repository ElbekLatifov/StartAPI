

using System.ComponentModel.DataAnnotations;

namespace Api.Entities;

public class Product
{
   [Key]
   public Guid Id { get; set; }
   public string Name { get; set; }
   public long Price { get; set; }
   public string Photo_url { get; set; }
   public Guid CategoryId { get; set; }
   public Category Category { get; set; }
}