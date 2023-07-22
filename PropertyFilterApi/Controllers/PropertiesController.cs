using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace PropertyFilterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private IPropertiesService _propertiesService;

        public PropertiesController(IPropertiesService propertiesService)//這裡也就開了洞，讓單元測試可以放測試自己建的另外一組假資料進來
        {
            _propertiesService=propertiesService;
            //接著要建立builder.Services.AddScoped<IPropertiesService, PropertiesService>();在Program
            //才會知道我們new介面也要newPropertiesService
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PropertyRequest propertyRequest)
        {
            var propertyRequestValidator = new PropertyResquestValidator();
            var validationResult = propertyRequestValidator.Validate(propertyRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            IEnumerable<PropertyResponse> allProperties = _propertiesService.GetProperties();
            //這裡就要使用在Controller new的 _propertiesService

            var result = allProperties;

            if (!string.IsNullOrEmpty(propertyRequest.Keyword))
            {
                result = result.Where(c =>
                c.PropertyName.Contains(propertyRequest.Keyword) ||
                c.Address.ToString().Contains(propertyRequest.Keyword));
            }

            if (propertyRequest.MinPrice >= 0)
            {
                result = result.Where(c => c.AskingPrice >= propertyRequest.MinPrice);
            }

            if (propertyRequest.MaxPrice >= 0)
            {
                result = result.Where(c => c.AskingPrice <= propertyRequest.MaxPrice);
            }
            return Ok(result);
        }

        public class PropertiesService : IPropertiesService
        {

            public IEnumerable<PropertyResponse> GetProperties()
            {
                return new List<PropertyResponse>
            {
                new PropertyResponse("廣達科技", 3000000000m, new Address { City = "台北市", District = "內湖區", Road = "基湖路", Number = "30號" }),
                new PropertyResponse("松智路101號辦公室", 2500000000m, new Address { City = "台北市", District = "信義區", Road = "松智路", Number = "101號" }),
                new PropertyResponse("富邦南港大樓", 3500000000m, new Address { City = "台北市", District = "南港區", Road = "經貿二路", Number = "188號" }),
                new PropertyResponse("微風台北車站", 5000000000m, new Address { City = "台北市", District = "中正區", Road = "忠孝西路一段", Number = "49號" }),
                new PropertyResponse("三創數位生活園區", 2000000000m, new Address { City = "台北市", District = "南港區", Road = "市民大道六段", Number = "133號" })
            }.AsEnumerable();
            }

            
        }
    }

    public interface IPropertiesService
    {
        IEnumerable<PropertyResponse> GetProperties();
    }
}
