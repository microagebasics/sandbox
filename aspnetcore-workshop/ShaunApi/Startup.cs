using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShaunApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace ShaunApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddDbContext<ApplicationDbContext>(options =>
      {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          // The official workshop calls for SqlExpress, howver, we'll use SqlLite for both
          options.UseSqlite(Configuration.GetConnectionString("SqlLite"));
        }
        else
        {
          options.UseSqlite(Configuration.GetConnectionString("SqlLite"));
        }
      }
      );

      services.AddMvc();

      services.AddSwaggerGen(options =>
        options.SwaggerDoc("v1", new Info { Title = "Conference Planner API", Version = "v1" })
      );

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      app.UseSwaggerUI(options =>
          options.SwaggerEndpoint("/swagger/v1/swagger.json", "Conference Planner API v1")
      );

      app.UseMvc();
    }
  }
}
