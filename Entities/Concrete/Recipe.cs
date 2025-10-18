using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Entities.Concrete
{
    [Table("recipes")]
    public class Recipe : IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("source")]
        public string Source { get; set; }

        [Column("preptime")]
        public int? PrepTime { get; set; }

        [Column("waittime")]
        public int? WaitTime { get; set; }

        [Column("cooktime")]
        public int? CookTime { get; set; }

        [Column("servings")]
        public int? Servings { get; set; }

        [Column("comments")]
        public string Comments { get; set; }

        [Column("calories")]
        public int? Calories { get; set; }

        [Column("fat")]
        public int? Fat { get; set; }

        [Column("satfat")]
        public int? SatFat { get; set; }

        [Column("carbs")]
        public int? Carbs { get; set; }

        [Column("fiber")]
        public int? Fiber { get; set; }

        [Column("sugar")]
        public int? Sugar { get; set; }

        [Column("protein")]
        public int? Protein { get; set; }

        [Column("instructions")]
        public string Instructions { get; set; }

        // JSON sütunlarını EF Core 7+ string olarak map edebiliriz
        [Column("ingredients", TypeName = "jsonb")]
        public string Ingredients { get; set; }

        [Column("tags", TypeName = "jsonb")]
        public string Tags { get; set; }
    }
}
