using Api.Entities;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StartAPI.Controllers;

[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext context;

    public CategoryController(AppDbContext context)
    {
        this.context = context;
    }

        [HttpGet]
    public async Task<List<Category>> Getlist()
    {
        return await context.Categories.ToListAsync();
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Getbyid(Guid id)
    {
        var product = await context.Categories.FirstOrDefaultAsync(p=>p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDto dto)
    {
        if(dto.ParentId != null && !await context.Categories.AnyAsync(p=>p.Id == dto.ParentId))
        {
            return NotFound();
        }
        var category = new Category()
        {
            Name = dto.Name,
            ParentId = dto.ParentId
        };
        context.Add(category);
         await context.SaveChangesAsync();
        return Ok();
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto dto)
    {
         if(!await context.Categories.AnyAsync(p=>p.Id == id))
         {
             return NotFound();
         }
         var product = await context.Categories.FirstOrDefaultAsync(p=>p.Id == id);
         product.Name = dto.Name;
         await context.SaveChangesAsync();
         return Ok();
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await context.Categories.FirstOrDefaultAsync(p=>p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        context.Categories.Remove(product);
        await context.SaveChangesAsync();
        return Ok();
    }

    
}