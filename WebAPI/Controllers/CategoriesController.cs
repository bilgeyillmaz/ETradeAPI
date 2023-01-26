using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _categoryService.GetByNameAsync(name);
            if (category == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, "There is no any data."));
            }
            return CreateActionResult(CustomResponseDto<Category>.Success(200, category));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<IEnumerable<Category>>.Success(200, categories));
        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Category>.Success(200, category));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Category category)
        {
            await _categoryService.AddAsync(category);
            return CreateActionResult(CustomResponseDto<Category>.Success(201, category));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            await _categoryService.UpdateAsync(category);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item with this Id"));
            }
            await _categoryService.RemoveAsync(category);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
