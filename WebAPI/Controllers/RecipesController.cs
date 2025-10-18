using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("getall")]
        [CacheAspect]
        [SecuredOperation("User")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(
            int? minCalories, int? maxCalories,
            int? minPrepTime, int? maxPrepTime,
            int? minCookTime, int? maxCookTime,
            int? minServings, int? maxServings,
            string name, string tag, string ingredient)
        {
            var result = await Task.Run(() => _recipeService.GetAllFiltered(
                minCalories, maxCalories,
                minPrepTime, maxPrepTime,
                minCookTime, maxCookTime,
                minServings, maxServings,
                name, tag, ingredient));
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{recipeId}")]
        [SecuredOperation("User")]
        public async Task<IActionResult> GetById(int recipeId)
        {
            var result = await Task.Run(() => _recipeService.GetById(recipeId));
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet("details")]
        [CacheAspect]
        [SecuredOperation("User")]
        public async Task<IActionResult> GetRecipeDetails()
        {
            var result = await Task.Run(() => _recipeService.GetRecipeDetails());
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        [ValidationAspect(typeof(RecipeValidator))]
        [SecuredOperation("Admin")]
        public async Task<IActionResult> Add(Recipe recipe)
        {
            var result = await Task.Run(() => _recipeService.Add(recipe));
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPut("update")]
        [ValidationAspect(typeof(RecipeValidator))]
        [SecuredOperation("Admin")]
        public async Task<IActionResult> Update(Recipe recipe)
        {
            var result = await Task.Run(() => _recipeService.Update(recipe));
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{recipeId}")]
        [SecuredOperation("Admin")]
        public async Task<IActionResult> Delete(int recipeId)
        {
            var recipe = await Task.Run(() => _recipeService.GetById(recipeId));
            if (!recipe.Success)
                return BadRequest(recipe.Message);

            var result = await Task.Run(() => _recipeService.Delete(recipe.Data));
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpGet("search")]
        [CacheAspect]
        [SecuredOperation("User")]
        public async Task<IActionResult> Search(
            string name, string tag = null, string ingredient = null,
            int? minPrepTime = null, int? maxPrepTime = null,
            int? minServings = null, int? maxServings = null)
        {
            var result = await Task.Run(() => _recipeService.GetAllFiltered(
                null, null,
                minPrepTime, maxPrepTime,
                null, null,
                minServings, maxServings,
                name, tag, ingredient));
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPost("admin/approve/{recipeId}")]
        [SecuredOperation("Admin")]
        public async Task<IActionResult> ApproveRecipe(int recipeId)
        {
            var recipe = await Task.Run(() => _recipeService.GetById(recipeId));
            if (!recipe.Success)
                return BadRequest(recipe.Message);

            recipe.Data.Comments += " (Admin tarafından onaylandı)";
            var result = await Task.Run(() => _recipeService.Update(recipe.Data));
            if (result.Success)
                return Ok("Tarif başarıyla onaylandı.");
            return BadRequest(result.Message);
        }
    }
}