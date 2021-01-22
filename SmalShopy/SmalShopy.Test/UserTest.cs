using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmalShopy.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public async Task GetUserTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/user");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("1234-455662-22233333-3333"));
        }
    }
}
