using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using asp_mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace tests
{
    public class IntegrationTest
    {
        public HttpClient _client;
        public IntegrationTest()
        {
                  string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            // TODO :  better way to get TodoApp path?
            var parent = Directory.GetParent(wanted_path).Parent;

             IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(parent.ToString())
            .AddJsonFile("app\\appsettings.json")
            .Build();
       
            var server = new TestServer(new WebHostBuilder().UseConfiguration(configuration).UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task HelloWorld_NoCondition_Success()        {
            
            var response = await _client.GetAsync("HelloWorld");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Hello World", responseString);
        }

         [Fact]
        public async Task Home_NoCondition_Success()        {
            
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var headers = response.Content.Headers.ContentType.ToString();

            Assert.Equal("text/html; charset=utf-8", headers);
        }

        [Fact]
        public async Task Movies_NoCondition_Success()        {
            
            var response = await _client.GetAsync("Movies");
            response.EnsureSuccessStatusCode();

            var headers = response.Content.Headers.ContentType.ToString();

            Assert.Equal("text/html; charset=utf-8", headers);
        }
    }
}
