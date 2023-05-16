using Api.Entities;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StartAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext context;

    public CategoriesController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<List<CreateCategoryDto>> Getlist()
    {
        var ss = await context.Categories.Where(p=>p.ParentId == null).ToListAsync();
        return await MapToDo(ss);
    }

    private async Task<CreateCategoryDto> MapTo(Category dto)
    {
        await context.Entry(dto).Collection(p=>p.Children).LoadAsync();
        return new CreateCategoryDto()
        {
            Id = dto.Id,
            Name = dto.Name,
            Children = await MapToDo(dto.Children),
            ParentId = dto.ParentId
        };
    }

    private async Task<List<CreateCategoryDto>> MapToDo(List<Category> categories)
    {
        List<CreateCategoryDto> yangi = new List<CreateCategoryDto>();
        foreach (var item in categories)
        {
            yangi.Add(await MapTo(item));
        }
        return yangi;
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