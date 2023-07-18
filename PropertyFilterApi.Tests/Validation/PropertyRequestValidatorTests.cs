using FluentAssertions;
using PropertyFilterApi.Controllers;

namespace PropertyFilterApi.Tests.Validation
{
    [TestClass]
    public class PropertyRequestValidatorTests
    {
        [TestMethod]
        public void IsInvalid_MinPriceSmallerThanZero()
        {
            var request = new PropertyRequest()//«Ø¤@­ÓModel
            {
                MinPrice = -1
            };

            var validator = new PropertyResquestValidator();
            var validateResult=validator.Validate(request);

            validateResult.Errors[0].ErrorMessage.Should().Be("Min Price needs to greater than 0.");
            

        }
    }
}