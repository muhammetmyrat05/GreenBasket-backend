using Core.Entities;

namespace Entities.DTOs
{
    public class RecipeDetailDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Calories { get; set; }
        public int? Servings { get; set; }
        public string Source { get; set; }
    }
}
