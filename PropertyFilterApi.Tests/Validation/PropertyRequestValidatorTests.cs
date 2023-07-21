using FluentAssertions;
using FluentValidation.TestHelper;
using PropertyFilterApi.Controllers;

namespace PropertyFilterApi.Tests.Validation
{
    [TestClass]
    public class PropertyRequestValidatorTests
    {
        [TestMethod]
        public void IsInvalid_MinPriceSmallerThanZero()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = -1
            };

            var validator = new PropertyResquestValidator();
            var validateResult = validator.Validate(request);

            validateResult.Errors[0].ErrorMessage.Should().Be("Min Price needs to greater than 0.");
        }

        [TestMethod]
        public void IsInvalid_MaxPriceSmallerThanZero()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = 0,//因為MaxPrice的Rule中包含了MinPrice所以MinPrice也一定要設值
                MaxPrice = -1
            };

            var validator = new PropertyResquestValidator();
            var validateResult = validator.TestValidate(request);

            validateResult.ShouldHaveValidationErrorFor(s => s.MaxPrice).WithErrorMessage("Max Price needs to greater than 0.");
            //validateResult.Errors[0].ErrorMessage.Should().Be("Max Price needs to greater than 0.");
        }

        [TestMethod]
        public void IsInvalid_MaxPriceSmallerThanMinPrice()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = 86,
                MaxPrice = 84
            };

            var validator = new PropertyResquestValidator();
            var validateResult = validator.TestValidate(request);

            validateResult.ShouldHaveValidationErrorFor(s => s.MaxPrice).WithErrorMessage("The Max Price must be greater than Min Price.");
        }

        [TestMethod]
        public void Isvalid()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = 11,
                MaxPrice = 21
            };

            var validator = new PropertyResquestValidator();
            var validateResult = validator.TestValidate(request);

            validateResult.ShouldNotHaveAnyValidationErrors();
        }
    }
}