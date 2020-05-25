using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotNetCoreTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        private string _baseUrl;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
            _baseUrl = "https://api-test.afterpay.dev/api/v3/validate/bank-account";
        }

        [HttpPost]
        public async Task<BankApiTestResponse> BankApiTestRequest(BankApiTestRequest parameters)
        {
            BankApiTestResponse result = new BankApiTestResponse();

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl);
                _logger.LogInformation("Request Endpoint: " + _baseUrl);

                request.Headers.Add("X-Auth-Key", parameters.XAuthHeader);
                _logger.LogInformation("Request Token: " + parameters.XAuthHeader);

                var content = new JObject
                {
                    { "bankAccount", parameters.BankAccount }
                };
                _logger.LogInformation("Request BankAccount: " + parameters.BankAccount);

                request.Content = new StringContent(
                    content.ToString(),
                    Encoding.UTF8,
                    parameters.ContentTypeHeader
                );
                _logger.LogInformation("Request: " + request);

                var response = await client.SendAsync(request);
                _logger.LogInformation("Response From Server: " + response);

                string responseConvertToText = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<BankApiTestResponse>(responseConvertToText);
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}
