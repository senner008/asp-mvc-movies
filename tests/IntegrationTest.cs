using System;
using System.Net.Http;
using System.Threading.Tasks;
using asp_mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace tests
{
    public class IntegrationTest
    {
        public HttpClient _client;
        public IntegrationTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task HelloWorld_NoCondition_Success()        {
            
            var response = await _client.GetAsync("HelloWorld");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Hello World", responseString);
        }
    }
}
