using System;
using Api.Entities;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StartAPI.Controllers;

[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext context;

    public ProductController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<List<Product>> Getlist()
    {
        return await context.Products.ToListAsync();
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Getbyid(Guid id)
    {
        var product = await context.Products.FirstOrDefaultAsync(p=>p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductDto dto)
    {
        if(!await context.Categories.AnyAsync(p=>p.Id == dto.CategoryId))
        {
            return NotFound();
        }
        var product = new Product()
        {
            Name = dto.Name,
            Price = dto.Price,
            Photo_url = await FileHelper.SaveProductFile(dto.PhotoUrl),
            CategoryId = dto.CategoryId
        };
        context.Add(product);
         await context.SaveChangesAsync();
        return Ok(product);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] ProductDto dto)
    {
         if(!await context.Products.AnyAsync(p=>p.Id == id))
         {
             return NotFound();
         }
         var product = await context.Products.FirstOrDefaultAsync(p=>p.Id == id);
         product.Name = dto.Name;
         product.Price = dto.Price;
         product.Photo_url = await FileHelper.SaveProductFile(dto.PhotoUrl);
         await context.SaveChangesAsync();
         return Ok(product);
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await context.Products.FirstOrDefaultAsync(p=>p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return Ok();
    }

}