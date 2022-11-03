using CalculatorService;

namespace movie_app_graduation_api.Helper
{
    public interface ICalculatorServiceHelper
    {
        public CalculatorSoapClient GetClient();
        public Task<int> AddAsync(int n1 = 0, int n2 = 0);
        public Task<int> Subtract(int n1 = 0, int n2 = 0);


    }
}
