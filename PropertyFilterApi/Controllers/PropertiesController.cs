using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PropertyFilterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        /// <summary>
        /// 需求是GET: api/Properties?keyWord={keyWord}&minPrice={minPrice}&maxPrice={maxPrice}
        /// 第一步先建立Get方法，★Get方法內放參數
        /// 方法裡面先放題目給的假資料，先放入並建立假資料類別。
        /// 第二步寫參數的邏輯。
        /// 1. 若keyword不是"null或空字串(string.IsNullOrEmpty)"，就會從allProperties的PropertyName、Adress篩選符合的大樓資訊並條列
        /// 2. 若minPrice、maxPrice大於等於零，就從allProperties的AskingPrice篩選符合的大樓資訊並條列
        /// 3. 注意：★若參數都沒輸入，就會全部篩選出來。→result=allProperties
        /// 4. ★一開始先設定result=allProperties，如果參數都沒輸入，就會全部篩選出來。
        ///      如果有符合的參數就被篩選過的list覆蓋！
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string keyword, decimal minPrice, decimal maxPrice) 
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

            if(!string.IsNullOrEmpty(keyword))
            {
                result = allProperties.Where(c => 
                c.PropertyName.Contains(keyword)||
                c.Address.City.Contains(keyword)||
                c.Address.District.Contains(keyword)||
                c.Address.Road.Contains(keyword)||
                c.Address.Number.Contains(keyword));
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
}
