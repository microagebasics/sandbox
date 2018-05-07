using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShaunFrontEnd.Services;

namespace ShaunFrontEnd
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
      services.AddMvc()
        .AddRazorPagesOptions(options =>
        {
          options.Conventions.AuthorizeFolder("/Admin", "Admin");
        });

      services.AddHttpClient<IApiClient, ApiClient>(client =>
      {
        client.BaseAddress = new Uri(Configuration["serviceUrl"]);
      });

      var authBuilder = services
        .AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
          options.LoginPath = "/Login";
          options.AccessDeniedPath = "/Denied";
        });

      var twitterConfig = Configuration.GetSection("twitter");
      if (twitterConfig["consumerKey"] != null)
      {
        authBuilder.AddTwitter(options => twitterConfig.Bind(options));
      }

      var googleConfig = Configuration.GetSection("google");
      if (googleConfig["clientID"] != null)
      {
        authBuilder.AddGoogle(options => googleConfig.Bind(options));
      }

      services.AddAuthorization(options =>
      {
        options.AddPolicy("Admin", policy =>
        {
          policy.RequireAuthenticatedUser()
                .RequireUserName(Configuration["admin"]);
        });
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvc(routes => 
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
