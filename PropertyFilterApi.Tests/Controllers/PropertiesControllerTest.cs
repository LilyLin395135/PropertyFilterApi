using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PropertyFilterApi.Controllers;

namespace PropertyFilterApi.Tests.Controllers
{
    [TestClass]
    public class PropertiesControllerTest
    {
        private PropertiesController? _propertiesController;
        private OkObjectResult? _result;

        [TestInitialize]
        public void Setup()
        {
            var propertiesService = Substitute.For<IPropertiesService>();//用propertiesService替代"Substitute.For<>()"放資料的介面IPropertiesService
            propertiesService.GetProperties().Returns(GetPropertiesInTaipei());
            //NSubstitute的方法Returns()會指定前面情況的回傳結果
            //這裡就是回傳GetPropertiesInTaipei

            _propertiesController = new PropertiesController(propertiesService);//這裡放入Substitute和Returns後的propertiesService

            IEnumerable<PropertyResponse> GetPropertiesInTaipei()//介面實作
            {
                return new List<PropertyResponse>
                {
                    new PropertyResponse("廣達科技大樓", 3000000000m, new Address { City = "台北市", District = "內湖區", Road = "基湖路", Number = "30號" }),
                    new PropertyResponse("松智路101號辦公室", 2500000000m, new Address { City = "台北市", District = "信義區", Road = "松智路", Number = "101號" }),
                    new PropertyResponse("富邦南港大樓", 3500000000m, new Address { City = "台北市", District = "南港區", Road = "經貿二路", Number = "188號" }),
                    new PropertyResponse("微風台北車站", 5000000000m, new Address { City = "台北市", District = "中正區", Road = "忠孝西路一段", Number = "49號" }),
                    new PropertyResponse("三創數位生活園區", 2000000000m, new Address { City = "台北市", District = "南港區", Road = "市民大道六段", Number = "133號" })
                }.AsEnumerable();
            }
        }

        [TestMethod]
        public void Filt_Keyword_In_PropertyName_And_Address()
        {
            var propertyRequest = new PropertyRequest()
            {
                Keyword = "南港"
            };

            var expectation = new List<PropertyResponse>()//集合的型別和前面一樣看前面
            {
            new PropertyResponse("富邦南港大樓", 3500000000m, new Address { City = "台北市", District = "南港區", Road = "經貿二路", Number = "188號" }),
            new PropertyResponse("三創數位生活園區", 2000000000m, new Address { City = "台北市", District = "南港區", Road = "市民大道六段", Number = "133號" })
            };

            WhenSearchProperties(propertyRequest);

            ThenPropertiesShouldBe(expectation);

        }

        [TestMethod]
        public void Filt_MinPrice()
        {
            var propertyRequest = new PropertyRequest()
            {
                MinPrice = 3000000000
            };

            var expectation = new List<PropertyResponse>()
            {
                new PropertyResponse("廣達科技大樓", 3000000000m, new Address { City = "台北市", District = "內湖區", Road = "基湖路", Number = "30號" }),
                new PropertyResponse("富邦南港大樓", 3500000000m, new Address { City = "台北市", District = "南港區", Road = "經貿二路", Number = "188號" }),
                new PropertyResponse("微風台北車站", 5000000000m, new Address { City = "台北市", District = "中正區", Road = "忠孝西路一段", Number = "49號" }),
            };

            WhenSearchProperties(propertyRequest);

            ThenPropertiesShouldBe(expectation);
        }

        [TestMethod]
        public void Filt_MaxPrice()
        {
            var propertyRequest = new PropertyRequest()
            {
                MaxPrice = 3000000000
            };

            var expectation = new List<PropertyResponse>()
            {
                new PropertyResponse("廣達科技大樓", 3000000000m, new Address { City = "台北市", District = "內湖區", Road = "基湖路", Number = "30號" }),
                new PropertyResponse("松智路101號辦公室", 2500000000m, new Address { City = "台北市", District = "信義區", Road = "松智路", Number = "101號" }),
                new PropertyResponse("三創數位生活園區", 2000000000m, new Address { City = "台北市", District = "南港區", Road = "市民大道六段", Number = "133號" })
            };

            WhenSearchProperties(propertyRequest);

            ThenPropertiesShouldBe(expectation);
        }
        [TestMethod]
        public void Filt_All_Three_RequestElements()
        {
            var propertyRequest = new PropertyRequest()
            {
                Keyword = "大樓",
                MinPrice = 3000000000,
                MaxPrice = 5000000000
            };

            var expectation = new List<PropertyResponse>()
            {
                new PropertyResponse("廣達科技大樓", 3000000000m, new Address { City = "台北市", District = "內湖區", Road = "基湖路", Number = "30號" }),
                new PropertyResponse("富邦南港大樓", 3500000000m, new Address { City = "台北市", District = "南港區", Road = "經貿二路", Number = "188號" }),
            };

            WhenSearchProperties(propertyRequest);

            ThenPropertiesShouldBe(expectation);
        }




        private void ThenPropertiesShouldBe(List<PropertyResponse> expectation)
        {
            _result?.Value.Should().BeEquivalentTo(expectation);//集合用.BeEquivalentTo()
        }

        private OkObjectResult? WhenSearchProperties(PropertyRequest propertyRequest)
        {
            var response = _propertiesController?.Get(propertyRequest);//when
            var _result = response as OkObjectResult;//從測試總管的結果知道輸出的內容，型別是OkObjectResult，因此要轉型
            return _result;
        }
    }
}
