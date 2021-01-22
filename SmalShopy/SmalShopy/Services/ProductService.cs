using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SmalShopy.Models;
using SmalShopy.Utilities;

namespace SmalShopy.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> SortProductsByAsync(string sortOption)
        {
            if ("low".Equals(sortOption, System.StringComparison.OrdinalIgnoreCase))
            {
                var products = await GetProductListAsync();
                return SortByLow(products);
            }
            else if ("high".Equals(sortOption, System.StringComparison.OrdinalIgnoreCase))
            {
                var products = await GetProductListAsync();
                return SortByHigh(products);
            }
            else if ("Ascending".Equals(sortOption, System.StringComparison.OrdinalIgnoreCase))
            {
                var products = await GetProductListAsync();
                return SortByAscending(products);
            }
            else if ("Descending".Equals(sortOption, System.StringComparison.OrdinalIgnoreCase))
            {
                var products = await GetProductListAsync();
                return SortByDescending(products);
            }
            else if ("Recommended".Equals(sortOption, System.StringComparison.OrdinalIgnoreCase))
            {
                var products = await GetProductListAsync();
                var shopperHistory = await GetshopperHistoryListAsync();
                return SortByRecommended(products.ToList(), shopperHistory);
            }

            throw new Exception("Parameter SortOption is incorrect. It must be one of the type: low, high, ascending, descending or recommended");
        }

        private IEnumerable<Product> SortByLow(IEnumerable<Product> products) => products.OrderBy(product => product.Price);
        private IEnumerable<Product> SortByHigh(IEnumerable<Product> products) => products.OrderByDescending(product => product.Price);
        private IEnumerable<Product> SortByAscending(IEnumerable<Product> products) => products.OrderBy(product => product.Name);
        private IEnumerable<Product> SortByDescending(IEnumerable<Product> products) => products.OrderByDescending(product => product.Name);
        private IEnumerable<Product> SortByRecommended(List<Product> products, IEnumerable<Customer> shopperHistory)
        {
            var popularProduct = new Dictionary<string, int>(products.Count());
            foreach (var order in shopperHistory)
            {
                foreach (var prod in order.ProductList)
                {
                    if (popularProduct.ContainsKey(prod.Name))
                    {
                        popularProduct[prod.Name]++;
                    }
                    else
                    {
                        popularProduct.Add(prod.Name, 1);
                    }
                }
            }
            var mostpopular = popularProduct.OrderByDescending(prod => prod.Value);
            var productsByRecomend = new List<Product>(products.Count());
            foreach (var prod in mostpopular)
            {
                var product = products.Where(p => prod.Key.Equals(p.Name, StringComparison.OrdinalIgnoreCase)).First();
                productsByRecomend.Add(product);
                products.Remove(product);
            }
            productsByRecomend.AddRange(products);
            return productsByRecomend;
        }
        private async Task<IEnumerable<Product>> GetProductListAsync()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{Keys.SourceUrl}/{Keys.Product}{Keys.Token}");
            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Product>>(body);
        }
        private async Task<IEnumerable<Customer>> GetshopperHistoryListAsync()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"{Keys.SourceUrl}/{Keys.ShopperHistory}{Keys.Token}");
            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Customer>>(body);
        }
    }
}
