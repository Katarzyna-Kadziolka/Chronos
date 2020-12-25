using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Chronos.Data;
using Chronos.Models.Category;
using Chronos.Models.Category.Requests;
using Chronos.Models.ToDoTasks;

namespace Chronos.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper) {
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<Category> PostCategory(CategoryPost category) {
            var t = _mapper.Map<Category>(category);
            ChronosDb.Categories.Add(t);
            return Ok(t);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id) {
            var categoryToRemove = ChronosDb.Categories.Find(o => o.Id == id);
            if (categoryToRemove == null) return NotFound();
            ChronosDb.Categories.Remove(categoryToRemove);
            return Ok(categoryToRemove);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() {
            var categories = ChronosDb.Categories;
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(Guid id) {
            var category = ChronosDb.Categories.Find(o => o.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchCategory(Guid id, Category category) {
            var categoryFromDb = ChronosDb.Categories.Find(o => o.Id == id);
            if (categoryFromDb == null) return NotFound();
            var newCategory = _mapper.Map(category, categoryFromDb);
            return Ok(newCategory);
        }
    }
}