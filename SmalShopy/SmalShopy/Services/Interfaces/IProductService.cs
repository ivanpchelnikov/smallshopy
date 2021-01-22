using System.Collections.Generic;
using System.Threading.Tasks;
using SmalShopy.Models;

namespace SmalShopy.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> SortProductsByAsync(string sortOption);
    }
}