using FluentValidation;

namespace PropertyFilterApi.Controllers
{
    public class PropertyResquestValidator:AbstractValidator<PropertyRequest>
    {
        public PropertyResquestValidator()
        {
            RuleFor(c=>c.MinPrice).GreaterThanOrEqualTo(0).WithMessage("MinPrice needs to greater than 0.");

            RuleFor(c => c.MaxPrice).GreaterThanOrEqualTo(0).WithMessage("MaxPrice needs to greater than 0.")
                .GreaterThanOrEqualTo(c => c.MinPrice).When(c => c.MinPrice.HasValue).WithMessage("The MaxPrice must be greater than MinPrice.");
        }
    }
}
