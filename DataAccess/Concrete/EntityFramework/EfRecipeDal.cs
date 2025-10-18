using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRecipeDal : EfEntityRepositoryBase<Recipe, RecipesContext>, IRecipeDal
    {
        public List<RecipeDetailDto> GetRecipeDetails()
        {
            using (var context = new RecipesContext())
            {
                var result = from r in context.Recipes
                             select new RecipeDetailDto
                             {
                                 Id = r.Id,
                                 Name = r.Name,
                                 Calories = r.Calories,
                                 Servings = r.Servings,
                                 Source = r.Source
                             };
                return result.ToList();
            }
        }
    }
}
