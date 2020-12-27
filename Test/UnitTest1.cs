using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using Web.Services;
using Xunit;

namespace Test {
    public class UnitTest1 : CustomWebAppFactory {

        private readonly IService _service;

        public UnitTest1(WebApplicationFactory<Web.Startup> factory)  : base(factory) {
            _service = Substitute.For<IService>();
        }

        protected override IServiceCollection ConfigureService() {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(p=> _service);
            return serviceCollection;
        }

        [Theory]
        [InlineData(-1, HttpStatusCode.BadRequest)]
        [InlineData(1, HttpStatusCode.OK)]
        public async Task Test2(int TemperatureC, HttpStatusCode expected) {
            _service.GetSomethings().Returns("456");
            var payload = new Web.WeatherForecast { TemperatureC = TemperatureC, Date = DateTime.Now };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var client = base._factory.CreateClient();
            var response = await client.PostAsync("/weatherForecast", content);
            Assert.True(response.StatusCode == expected);
            _service.DidNotReceive().GetSomethings();
        }
    }
}
