using System.Threading.Tasks;
using SmalShopy.Models;

namespace SmalShopy.Services
{
    public interface ITrolleyCalculatorService
    {
        Task<string> TrolleyCalculate(Trolley trolley);
        string TrolleyCalculateLocally(Trolley trolley);
    }
}