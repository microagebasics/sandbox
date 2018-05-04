using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ShaunToDoProject.IntegrationTests
{
  public class TestFixture : IDisposable
  {

    private readonly TestServer _server;

    public TestFixture()
    {
      var builder = new WebHostBuilder()
        .UseStartup<ShaunToDoProject.Startup>()
        .ConfigureAppConfiguration((context, configBuilder) =>
        {
          configBuilder.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\ShaunToDoProject"));
          configBuilder.AddJsonFile("appsettings.json");
          // don't need the Facebook stuff as we didn't connect to it
        });

      _server = new TestServer(builder);

      Client = _server.CreateClient();
      Client.BaseAddress = new Uri("http://localhost:5001");
    }

    public HttpClient Client { get; }

    public void Dispose()
    {
      Client.Dispose();
      _server.Dispose();
    }
  }
}
