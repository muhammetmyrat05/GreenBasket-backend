using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class RecipeManager : IRecipeService
    {
        private readonly IRecipeDal _recipeDal;

        public RecipeManager(IRecipeDal recipeDal)
        {
            _recipeDal = recipeDal;
        }

        public IDataResult<List<Recipe>> GetAll()
        {
            return new SuccessDataResult<List<Recipe>>(_recipeDal.GetAll());
        }

        public IDataResult<Recipe> GetById(int recipeId)
        {
            var recipe = _recipeDal.Get(r => r.Id == recipeId);
            if (recipe == null)
                return new ErrorDataResult<Recipe>("Tarif bulunamadı");
            return new SuccessDataResult<Recipe>(recipe);
        }

        public IDataResult<List<RecipeDetailDto>> GetRecipeDetails()
        {
            return new SuccessDataResult<List<RecipeDetailDto>>(_recipeDal.GetRecipeDetails());
        }

        public IResult Add(Recipe recipe)
        {
            _recipeDal.Add(recipe);
            return new SuccessResult("Tarif eklendi");
        }

        public IResult Update(Recipe recipe)
        {
            _recipeDal.Update(recipe);
            return new SuccessResult("Tarif güncellendi");
        }

        public IResult Delete(Recipe recipe)
        {
            _recipeDal.Delete(recipe);
            return new SuccessResult("Tarif silindi");
        }

        public IDataResult<List<Recipe>> GetAllFiltered(
            int? minCalories, int? maxCalories,
            int? minPrepTime, int? maxPrepTime,
            int? minCookTime, int? maxCookTime,
            int? minServings, int? maxServings,
            string name, string tag, string ingredient)
        {
            var recipes = _recipeDal.GetAll();

            // Filtreleme
            if (minCalories.HasValue)
                recipes = recipes.Where(r => r.Calories >= minCalories.Value).ToList();
            if (maxCalories.HasValue)
                recipes = recipes.Where(r => r.Calories <= maxCalories.Value).ToList();
            if (minPrepTime.HasValue)
                recipes = recipes.Where(r => r.PrepTime >= minPrepTime.Value).ToList();
            if (maxPrepTime.HasValue)
                recipes = recipes.Where(r => r.PrepTime <= maxPrepTime.Value).ToList();
            if (minCookTime.HasValue)
                recipes = recipes.Where(r => r.CookTime >= minCookTime.Value).ToList();
            if (maxCookTime.HasValue)
                recipes = recipes.Where(r => r.CookTime <= maxCookTime.Value).ToList();
            if (minServings.HasValue)
                recipes = recipes.Where(r => r.Servings >= minServings.Value).ToList();
            if (maxServings.HasValue)
                recipes = recipes.Where(r => r.Servings <= maxServings.Value).ToList();
            if (!string.IsNullOrEmpty(name))
                recipes = recipes.Where(r => r.Name != null && r.Name.ToLower().Contains(name.ToLower())).ToList();
            if (!string.IsNullOrEmpty(tag))
                recipes = recipes.Where(r => r.Tags != null && r.Tags.ToLower().Contains(tag.ToLower())).ToList();
            if (!string.IsNullOrEmpty(ingredient))
                recipes = recipes.Where(r => r.Ingredients != null && r.Ingredients.ToLower().Contains(ingredient.ToLower())).ToList();

            return new SuccessDataResult<List<Recipe>>(recipes, $"Toplam {recipes.Count} tarif bulundu");
        }
    }
}