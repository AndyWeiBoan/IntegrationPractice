using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Web;
using Xunit;

namespace Test {
    public abstract class CustomWebFactory
        : IClassFixture<WebApplicationFactory<Startup>> {

        //public WebApplicationFactory<Startup> _webHost;
        private readonly WebApplicationFactory<Startup> _factory;

        public CustomWebFactory(WebApplicationFactory<Startup> factory) {
            _factory = factory;
            //_webHost = _factory;
        }
        //protected abstract IServiceCollection ConfigureService();
        //protected HttpClient CreateClient() {
        //    return _factory.WithWebHostBuilder(builder => {
        //        builder.UseEnvironment("Testing");
        //        builder.ConfigureTestServices(s=> ConfigureService());
        //    }).CreateClient();

        //}

        protected HttpClient CreateClient(Action<IServiceCollection> s) {
            return _factory.WithWebHostBuilder(builder => {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(s);
            }).CreateClient();
        }
    }
}
