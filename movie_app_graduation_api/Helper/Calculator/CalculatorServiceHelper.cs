using CalculatorService;
using System.ServiceModel;

namespace MoviesApi.Helper
{
    public class CalculatorServiceHelper : ICalculatorServiceHelper
    {
        private const string domain = "http://www.dneonline.com";
        private const string strServiceURL = $"{domain}/calculator.asmx";
        private readonly CalculatorSoapClient client;

        public CalculatorServiceHelper()
        {
            client = GetClient();
        }

        #region Get Calculator Client from soap
        public CalculatorSoapClient GetClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            EndpointAddress address = new EndpointAddress(strServiceURL);
            binding.Security.Mode = BasicHttpSecurityMode.None;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReceiveTimeout = new TimeSpan(0, 180, 0);
            binding.SendTimeout = new TimeSpan(0, 180, 0);
            CalculatorSoapClient _client = new CalculatorSoapClient(binding, address);
            _client.ClientCredentials.UserName.UserName = "Admin";
            _client.ClientCredentials.UserName.Password = "Admin";

            return _client;

        }
        #endregion

        public Task<int> AddAsync(int n1 = 0, int n2 = 0)
        {
            return client.AddAsync(n1, n2);

        }
        public Task<int> Subtract(int n1 = 0, int n2 = 0)
        {
            return client.SubtractAsync(n1, n2);
        }


    }
}
