using FluentValidation.TestHelper;
using PropertyFilterApi.Controllers;
using System.Linq.Expressions;

namespace PropertyFilterApi.Tests.Validation
{
    [TestClass]
    public class PropertyRequestValidatorTests
    {
        private PropertyResquestValidator _validator;
        private TestValidationResult<PropertyRequest> _validationResult;

        [TestInitialize] 
        public void Setup() 
        {
            _validator = new PropertyResquestValidator();
        }
        
        [TestMethod]
        public void IsInvalid_MinPriceSmallerThanZero()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = -1
            };

            WhenStartValidation(request);

            ThenPriceShouldHaveValidationErrorFor(s => s.MinPrice, "Min Price needs to greater than 0.");
        }

        

        [TestMethod]
        public void IsInvalid_MaxPriceSmallerThanZero()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = 0,//因為MaxPrice的Rule中包含了MinPrice所以MinPrice也一定要設值
                MaxPrice = -1
            };

            WhenStartValidation(request);

            ThenPriceShouldHaveValidationErrorFor(s => s.MaxPrice, "Max Price needs to greater than 0.");
        }

        [TestMethod]
        public void IsInvalid_MaxPriceSmallerThanMinPrice()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = 86,
                MaxPrice = 84
            };

            WhenStartValidation(request);

            ThenPriceShouldHaveValidationErrorFor(s => s.MaxPrice, "The Max Price must be greater than Min Price.");
        }

        [TestMethod]
        public void Isvalid()
        {
            var request = new PropertyRequest()//建一個Model
            {
                MinPrice = 11,
                MaxPrice = 21
            };

            WhenStartValidation(request);
            ThenAllValidationIsValid();
        }

        private void ThenAllValidationIsValid()
        {
            _validationResult.ShouldNotHaveAnyValidationErrors();
        }

        private void ThenPriceShouldHaveValidationErrorFor(
            Expression<Func<PropertyRequest, decimal?>> memberAccessor,
            string errorMessage)//s => s.MaxPrice這段的型別Expression<Func<PropertyRequest, decimal?>>。要學會看
        {
            _validationResult.ShouldHaveValidationErrorFor(memberAccessor).WithErrorMessage(errorMessage);
        }

        private void WhenStartValidation(PropertyRequest request)
        {
            _validationResult = _validator.TestValidate(request);
        }
    }
}