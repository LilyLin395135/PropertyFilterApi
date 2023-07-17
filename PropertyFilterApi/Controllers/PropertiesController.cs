using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PropertyFilterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get(PropertyRequest propertyRequest) 
        {
            var allProperties = new List<PropertyResponse>
            {
            new PropertyResponse("廣達科技大樓", 3000000000m, new Address { City = "台北市", District = "內湖區", Road = "基湖路", Number = "30號" }),
            new PropertyResponse("松智路101號辦公室", 2500000000m, new Address { City = "台北市", District = "信義區", Road = "松智路", Number = "101號" }),
            new PropertyResponse("富邦南港大樓", 3500000000m, new Address { City = "台北市", District = "南港區", Road = "經貿二路", Number = "188號" }),
            new PropertyResponse("微風台北車站", 5000000000m, new Address { City = "台北市", District = "中正區", Road = "忠孝西路一段", Number = "49號" }),
            new PropertyResponse("三創數位生活園區", 2000000000m, new Address { City = "台北市", District = "南港區", Road = "市民大道六段", Number = "133號" })
            }.AsEnumerable();

            var result = allProperties;

            if(!string.IsNullOrEmpty(propertyRequest.Keyword))
            {
                result = result.Where(c => 
                c.PropertyName.Contains(propertyRequest.Keyword)||
                c.Address.City.Contains(propertyRequest.Keyword)||
                c.Address.District.Contains(propertyRequest.Keyword)||
                c.Address.Road.Contains(propertyRequest.Keyword)||
                c.Address.Number.Contains(propertyRequest.Keyword));
            }

            if(propertyRequest.MinPrice >= 0)
            {
                result = result.Where(c => c.AskingPrice >= propertyRequest.MinPrice);
            }

            if(propertyRequest.MaxPrice >= 0) 
            {
                result= result.Where(c=>c.AskingPrice<= propertyRequest.MaxPrice);
            }

            return Ok(result);
        }

    }

    public class PropertyResponse
    {
        public PropertyResponse(string propertyName, decimal askingPrice, Address address)
        {
            PropertyName = propertyName;
            AskingPrice = askingPrice;
            Address = address;
        }

        public string PropertyName { get; set; }
        public decimal AskingPrice { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; internal set; }
        public string District { get; internal set; }
        public string Road { get; internal set; }
        public string Number { get; internal set; }
    }
    /// <summary>
    /// 驗證
    /// 1. NuGet下載FluentValidation
    /// 2. 要驗證輸入參數，minPrice必大於零、maxPrice必大於零且必大於minPrice。把參數包進PropertyRequest一起做驗證規則。
    /// 3. 繼承AbstractValidator
    /// 4. PropertyResquestValidator方法，RuleFor(對象).驗證動作
    /// </summary>
    /// <returns></returns>
    public class PropertyResquestValidator:AbstractValidator<PropertyRequest>
    {
        public PropertyResquestValidator()
        {
            RuleFor(c=>c.MinPrice).GreaterThanOrEqualTo(0).WithMessage("MinPrice needs to greater than 0.");

            RuleFor(c => c.MaxPrice).GreaterThanOrEqualTo(0).WithMessage("MaxPrice needs to greater than 0.")
                .GreaterThanOrEqualTo(c => c.MinPrice).When(c => c.MinPrice.HasValue).WithMessage("The MaxPrice must be greater than MinPrice.");
        }
    }

    public class PropertyRequest
    //string? keyword, decimal? minPrice, decimal? maxPrice
    {
        public string? Keyword { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
