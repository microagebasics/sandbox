using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ShaunApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://localhost:5010")
            .UseStartup<Startup>();

  }
}
