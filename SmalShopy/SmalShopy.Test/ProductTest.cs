using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmalShopy.Models;

namespace SmalShopy.Test
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public async Task SortProductIncorrectSortOptionsTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/sort?sortOption=loow");
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("Parameter SortOption is incorrect"));
        }

        [TestMethod]
        public async Task SortProductLowTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/sort?sortOption=low");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<IEnumerable<Product>>(data).ToList();
            Assert.IsTrue(resp.First().Price < resp.Last().Price);
        }

        [TestMethod]
        public async Task SortProductHighTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/sort?sortOption=high");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<IEnumerable<Product>>(data).ToList();
            Assert.IsTrue(resp.First().Price > resp.Last().Price);
        }

        [TestMethod]
        public async Task SortProductAscTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/sort?sortOption=ascending");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<IEnumerable<Product>>(data).ToList();
            Assert.IsTrue(string.Compare(resp.First().Name, resp.Last().Name) < 0);
        }

        [TestMethod]
        public async Task SortProductDescTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/sort?sortOption=descending");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<IEnumerable<Product>>(data).ToList();
            Assert.IsTrue(string.Compare(resp.First().Name,resp.Last().Name) > 0 );
        }

        [TestMethod]
        public async Task SortProductRecomendTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("/sort?sortOption=recommended");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<IEnumerable<Product>>(data).ToList();
            Assert.IsTrue(string.Compare(resp.First().Name, "Test Product A") == 0);
            Assert.IsTrue(string.Compare(resp.Last().Name, "Test Product D") == 0);
        }
    }
}
