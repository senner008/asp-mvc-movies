using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using asp_identity.Data;
using asp_mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using MvcMovie.Models;
using Xunit;

namespace tests
{
    public class MockGetKeys : IGetKeys
    {
        public string Key1 { get; set; } = Environment.GetEnvironmentVariable("AES_KEY1");
        public string Key2 { get; set; } = Environment.GetEnvironmentVariable("AES_KEY2");
    }
    public class IntegrationTest
    {
        public HttpClient _client;
        public IntegrationTest()
        {
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            // TODO :  better way to get app path?
            var parent = Directory.GetParent(wanted_path).Parent;
            string env = Environment.GetEnvironmentVariable("DB");

            var webhost = new WebHostBuilder();
            if (!String.IsNullOrEmpty(env)) {       
                webhost.ConfigureServices(services => {
                    services.AddDbContext<MvcMovieContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("UNOEURO_DB")));
                    services.AddDbContext<ApplicationDbContext> (options => options.UseMySql(Environment.GetEnvironmentVariable("DB")));
                    services.AddSingleton<IGetKeys, MockGetKeys>();
                });
            } else {
                 IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(parent.ToString())
                    .AddJsonFile("app/appsettings.json")
                    .Build();
                 webhost.UseConfiguration(configuration);
            }
            _client = new TestServer(webhost.UseStartup<Startup>()).CreateClient();
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
