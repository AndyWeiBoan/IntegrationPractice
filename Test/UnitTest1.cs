using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using Web;
using Web.Services;
using Xunit;

namespace Test {
    public class UnitTest1 : CustomWebFactory {

        private readonly IService _service;
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;
        public UnitTest1(WebApplicationFactory<Startup> factory) :base(factory)  { 
            _factory = factory;
            _service = Substitute.For<IService>();
            _client = CreateClient(s=> s.AddScoped(a => _service));
        }

        public HttpClient GetClient() {
            return _factory.WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    services.AddScoped(a => _service);
                });
            }).CreateClient();
        }

            [Fact]
        public async Task Test2() {
            _service.GetSomethings().Returns("456");
            var payload = new WeatherForecast { TemperatureC = 1, Date = DateTime.Now };
            HttpContent contentPost = new StringContent(
                JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/weatherForecast", contentPost);
            var json = await response.Content.ReadAsStringAsync();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            _service.DidNotReceive().GetSomethings();
        }
    }
}
