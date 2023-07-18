using FluentValidation;

namespace PropertyFilterApi.Controllers
{
    public class PropertyResquestValidator:AbstractValidator<PropertyRequest>
    {
        public PropertyResquestValidator()
        {
            RuleFor(c=>c.MinPrice).GreaterThanOrEqualTo(0).WithMessage("{PropertyName} needs to greater than 0.");

            RuleFor(c => c.MaxPrice).GreaterThanOrEqualTo(0).WithMessage("{PropertyName} needs to greater than 0.")
                .GreaterThanOrEqualTo(c => c.MinPrice).When(c => c.MinPrice.HasValue).WithMessage("The {PropertyName} must be greater than {ComparisonProperty}.");
        }
    }
}
