using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Web;
using Xunit;

namespace Test {
    public class CustomWebAppFactory : IClassFixture<WebApplicationFactory<Startup>> {

        private WebApplicationFactory<Startup> _webHost;
        protected readonly WebApplicationFactory<Startup> _factory;

        public CustomWebAppFactory(WebApplicationFactory<Startup> factory) {
            _factory = factory;
            _webHost = _factory.WithWebHostBuilder(builder=> {});
        }

        protected virtual IServiceCollection ConfigureService() => new ServiceCollection();

        protected void ConfigureWebHost() {
            _webHost = _factory.WithWebHostBuilder(builder => {
                builder.UseEnvironment("Test");
                builder.ConfigureTestServices((service)=> ConfigureService());
            });
        }
    }
}
