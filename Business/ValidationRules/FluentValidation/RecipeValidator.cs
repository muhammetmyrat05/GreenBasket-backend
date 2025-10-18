using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Recipe name cannot be empty.");
            RuleFor(r => r.Name).MinimumLength(2).WithMessage("Recipe name must be at least 2 characters long.");

            RuleFor(r => r.Calories).GreaterThanOrEqualTo(0).WithMessage("Calories cannot be negative.");
            RuleFor(r => r.Servings).GreaterThan(0).WithMessage("Servings must be at least 1.");

            RuleFor(r => r.Ingredients).NotNull().WithMessage("Ingredients cannot be null.");
            RuleFor(r => r.Instructions).NotEmpty().WithMessage("Instructions cannot be empty.");

            RuleFor(r => r.Name).Must(StartWithCapital).WithMessage("Recipe name must start with a capital letter.");
        }

        private bool StartWithCapital(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return char.IsUpper(name[0]);
        }
    }
}
