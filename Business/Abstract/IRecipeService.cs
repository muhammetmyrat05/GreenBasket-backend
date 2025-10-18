using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRecipeService
    {
        IDataResult<List<Recipe>> GetAll();
        IDataResult<Recipe> GetById(int recipeId);
        IDataResult<List<RecipeDetailDto>> GetRecipeDetails();
        IResult Add(Recipe recipe);
        IResult Update(Recipe recipe);
        IResult Delete(Recipe recipe);

        IDataResult<List<Recipe>> GetAllFiltered(
            int? minCalories, int? maxCalories,
            int? minPrepTime, int? maxPrepTime,
            int? minCookTime, int? maxCookTime,
            int? minServings, int? maxServings,
            string name, string tag, string ingredient);
    }
}