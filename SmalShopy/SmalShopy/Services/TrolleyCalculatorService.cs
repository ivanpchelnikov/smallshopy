using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SmalShopy.Models;
using SmalShopy.Utilities;

namespace SmalShopy.Services
{
    public class TrolleyCalculatorService : ITrolleyCalculatorService
    {
        private readonly HttpClient _httpClient;

        public TrolleyCalculatorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string TrolleyCalculateLocally(Trolley trolley)
        {
            decimal totalAmount =0;
            var tempQuantities = new List<Product>(trolley.Quantities);
            var specialsSorted = FindAllPossibleSpecialsAndSortByMaxSavings(trolley, tempQuantities);
            while (specialsSorted.Any())
            {
                var topSavings = specialsSorted.FirstOrDefault();
                foreach (var sale in topSavings.Quantities)
                {
                    var found = tempQuantities.FirstOrDefault(p => p.Name.Equals(sale.Name, StringComparison.OrdinalIgnoreCase));
                    found.Quantity -= topSavings.Includes * sale.Quantity;
                }
                totalAmount += topSavings.Total * topSavings.Includes;

                specialsSorted = FindAllPossibleSpecialsAndSortByMaxSavings(trolley, tempQuantities);
            }

            foreach (var rest in tempQuantities)
            {
                var product = trolley.Products.FirstOrDefault(p => p.Name.Equals(rest.Name, StringComparison.OrdinalIgnoreCase));
                totalAmount += rest.Quantity * product.Price;
            }

            return totalAmount.ToString();
        }
        public IEnumerable<Special> FindAllPossibleSpecialsAndSortByMaxSavings(Trolley trolley, IEnumerable<Product> tempQuantities)
        {
            var foundSpecials = new List<Special>(trolley.Specials.Count());
            foreach (var specialSale in trolley.Specials)
            {
                var isOnSpecial = false;
                decimal fullPrice = 0;
                int minIncludes = 0;
                foreach (var sale in specialSale.Quantities)
                {
                    if (sale.Quantity <= 0) continue;
                    isOnSpecial = false;
                    var found = tempQuantities.FirstOrDefault(p => p.Name.Equals(sale.Name, StringComparison.OrdinalIgnoreCase)
                                                                        && p.Quantity >= sale.Quantity);
                    if (found == null) break;

                    var includes = (int)(found.Quantity / sale.Quantity);
                    minIncludes = minIncludes != 0 && minIncludes < includes ? minIncludes : includes;
                    var prod = trolley.Products.FirstOrDefault(p => p.Name.Equals(sale.Name, StringComparison.OrdinalIgnoreCase));
                    if (prod != null)
                    {
                        fullPrice += prod.Price * sale.Quantity;
                    }
                    isOnSpecial = true;
                }
                if (isOnSpecial)
                {
                    specialSale.TotalSavings = fullPrice - specialSale.Total;
                    specialSale.Includes = minIncludes;
                    foundSpecials.Add(specialSale);
                }
            }
            return foundSpecials.OrderByDescending(sale => sale.TotalSavings);
        }

        public async Task<string> TrolleyCalculate(Trolley trolley)
        {
            return await CallCalculateTrolleyAsync(trolley);
        }
        private async Task<string> CallCalculateTrolleyAsync(Trolley trolley)
        {
            var jsonData = JsonSerializer.Serialize(trolley);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.PostAsync($"{Keys.SourceUrl}/{Keys.TrolleyCalculator}{Keys.Token}", content);
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
