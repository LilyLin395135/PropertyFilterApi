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
            var validateResult=validator.Validate(request);

            validateResult.Errors[0].ErrorMessage.Should().Be("Min Price needs to greater than 0.");
        }

        [TestMethod]
        public void IsInvalid_MaxPriceSmallerThanZero()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = 0,
                MaxPrice = -1
            };

            var validator = new PropertyResquestValidator();
            var validateResult = validator.Validate(request);

            //validateResult.ShouldHaveValidationErrorFor(s => s.MaxPrice).WithErrorMessage("Max Price needs to greater than 0.");
            validateResult.Errors[0].ErrorMessage.Should().Be("Max Price needs to greater than 0.");


        }
    }
}