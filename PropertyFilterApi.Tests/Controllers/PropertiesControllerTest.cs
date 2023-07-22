using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyFilterApi.Controllers;

namespace PropertyFilterApi.Tests.Controllers
{
    [TestClass]//testc快捷鍵
    public class PropertiesControllerTest
    {
        [TestMethod]//testm快捷鍵
        public void Filt_Keyword_In_PropertyName_And_Address()
        {
            var propertiesControllers=new PropertiesController();

            var propertyRequest = new PropertyRequest()
            {
                Keyword = "南港"
            };

            var expectation = new List<PropertyResponse>()//集合的型別和前面一樣看前面
            {
            new PropertyResponse("富邦南港大樓", 3500000000m, new Address { City = "台北市", District = "南港區", Road = "經貿二路", Number = "188號" }),
            new PropertyResponse("三創數位生活園區", 2000000000m, new Address { City = "台北市", District = "南港區", Road = "市民大道六段", Number = "133號" })
            };

            var response = propertiesControllers.Get(propertyRequest);//when
            var result = response as OkObjectResult;//從測試總管的結果知道輸出的內容，型別是OkObjectResult，因此要轉型

            result?.Value.Should().BeEquivalentTo(expectation);//集合用.BeEquivalentTo()

        }
    }
}
