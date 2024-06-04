using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practic_API.DAL;
using Practic_API.DTOs.CategoryDTO;
using Practic_API.Entities;

namespace Practic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("id")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (category == null) return BadRequest();
            return StatusCode(StatusCodes.Status202Accepted,category);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var category=_context.Categories.ToList();
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult>Create([FromForm]CreateCategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryDTO categoryDTO)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(p => p.Id == categoryDTO.Id);
            if (category is null) return BadRequest();

            category.Name= categoryDTO.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, category);

        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromForm]int id)
        {
            var category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category is null) return BadRequest();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK,category);
        }
        
    }
}
