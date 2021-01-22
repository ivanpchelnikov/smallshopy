using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmalShopy.Models;

namespace SmalShopy.Test
{
    [TestClass]
    public class TrolleyTest
    {
        [TestMethod]
        public async Task TrolleyCalculateLocallySet1Test()
        {
            var trolley = "{\"products\":[{\"name\":\"Product 0\",\"quantity\":0,\"price\":14.185590741311}],\"specials\":[{\"total\":6.35914770862194,\"TotalSavings\":0,\"quantities\":[{\"name\":\"Product 0\",\"quantity\":3,\"price\":0}],\"Includes\":0},{\"total\":4.63390843039085,\"TotalSavings\":0, \"quantities\":[{\"name\":\"Product 0 \",\"quantity\":9,\"price\":0}],\"Includes\":0}],\"quantities\":[{\"name\":\"Product 0\",\"quantity\":5,\"price\":0}]}";
            var body = new StringContent(trolley, Encoding.UTF8, "application/json");
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.PostAsync("/trolleyTotal", body);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("34.73032"));
        }

        [TestMethod]
        public async Task TrolleyCalculateLocallySet2Test()
        {
            var trolley = "{\"products\":[{\"name\":\"1\",\"quantity\":0,\"price\":2.0},{\"name\":\"2\",\"quantity\":0,\"price\":5.0}],\"specials\":[{\"total\":5,\"TotalSavings\":0,\"quantities\":[{\"name\":\"1\",\"quantity\":3,\"price\":0},{\"name\":\"2\",\"quantity\":0,\"price\":0}],\"Includes\":0},{\"total\":10,\"TotalSavings\":0,\"quantities\":[{\"name\":\"1\",\"quantity\":1,\"price\":0},{\"name\":\"2\",\"quantity\":2,\"price\":0}],\"Includes\":0}],\"quantities\":[{\"name\":\"1\",\"quantity\":3,\"price\":0},{\"name\":\"2\",\"quantity\":2,\"price\":0}]}";
            var body = new StringContent(trolley, Encoding.UTF8, "application/json");
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.PostAsync("/trolleyTotal", body);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("14"));
        }
        [TestMethod]
        public async Task TrolleyCalculateLocallySet3Test()
        {
            var trolley = "{\"products\":[{\"name\":\"Product 0\",\"quantity\":0,\"price\":1.60165933501053},{\"name\":\"Product 1\",\"quantity\":0,\"price\":5.82286862229131},{\"name\":\"Product 2\",\"quantity\":0,\"price\":9.40127499373689},{\"name\":\"Product 3\",\"quantity\":0,\"price\":3.82882463458405},{\"name\":\"Product 4\",\"quantity\":0,\"price\":6.67348821958224},{\"name\":\"Product 5\",\"quantity\":0,\"price\":3.06940084233386}],\"specials\":[{\"total\":16.594090500506,\"TotalSavings\":0,\"quantities\":[{\"name\":\"Product 0\",\"quantity\":3,\"price\":0},{\"name\":\"Product 1\",\"quantity\":0,\"price\":0},{\"name\":\"Product 2\",\"quantity\":5,\"price\":0},{\"name\":\"Product 3\",\"quantity\":0,\"price\":0},{\"name\":\"Product 4\",\"quantity\":5,\"price\":0},{\"name\":\"Product 5\",\"quantity\":9,\"price\":0}],\"Includes\":0}],\"quantities\":[{\"name\":\"Product 0\",\"quantity\":5,\"price\":0},{\"name\":\"Product 1\",\"quantity\":4,\"price\":0},{\"name\":\"Product 2\",\"quantity\":4,\"price\":0},{\"name\":\"Product 3\",\"quantity\":6,\"price\":0},{\"name\":\"Product 4\",\"quantity\":3,\"price\":0},{\"name\":\"Product 5\",\"quantity\":7,\"price\":0}]}";
            var body = new StringContent(trolley, Encoding.UTF8, "application/json");
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.PostAsync("/trolleyTotal", body);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("133.384"));
        }
        [TestMethod]
        public async Task TrolleyCalculateLocallyTest()
        {
            var trolley = new Trolley();

            trolley.Products = new List<Product>()
            {
                new Product()
                {
                    Name = "Test Product A",
                    Price = 99.99M
                }
            };

            trolley.Quantities = new List<Product>()
            {
                new Product()
                {
                    Name = "Test Product A",
                    Quantity = 3
                }
            };

            trolley.Specials = new List<Special>()
            {
                new Special()
                {
                    Total = 1,
                    Quantities = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Test Product A",
                            Quantity = 2
                        }
                    }
                }
            };

            var jsonData = JsonSerializer.Serialize(trolley);
            var body = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.PostAsync("/trolleyTotal", body);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("100.99"));
        }
        [TestMethod]
        public async Task TrolleyCalculateTest()
        {
            // Act
            var trolley = new Trolley();

            trolley.Products = new List<Product>()
            {
                new Product()
                {
                    Name = "Test Product A",
                    Price = 99.99M
                }
            };

            trolley.Quantities = new List<Product>()
            {
                new Product()
                {
                    Name = "Test Product A",
                    Quantity = 3
                }
            };

            trolley.Specials = new List<Special>()
            {
                new Special()
                {
                    Total = 1,
                    Quantities = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Test Product A",
                            Quantity = 2
                        }
                    }
                }
            };

            var jsonData = JsonSerializer.Serialize(trolley);
            var body = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.PostAsync("/trolleyTotal", body);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("100.99"));
        }
    }
}
