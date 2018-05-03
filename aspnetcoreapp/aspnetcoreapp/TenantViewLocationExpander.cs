using aspnetcoreapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreapp
{
  public class TenantViewLocationExpander : IViewLocationExpander
  {

    private const string SPORT_KEY = "sport", TENANT_KEY = "tenant";

    public void PopulateValues(ViewLocationExpanderContext context)
    {
      context.Values[SPORT_KEY]
        = context.ActionContext.HttpContext.GetTenant<Tenant>()?.Sport;

      context.Values[TENANT_KEY]
        = context.ActionContext.HttpContext.GetTenant<Tenant>()?.Domain;
    }

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
      string sport = null;

      if(context.Values.TryGetValue(SPORT_KEY, out sport))
      {
        IEnumerable<string> sportLocations = new[]
        {
          $"/Sports/{sport}/{{1}}/{{0}}.cshtml",
          $"/Sports/{sport}/Shared/{{0}}.cshtml"
        };

        string tenant;
        if (context.Values.TryGetValue(TENANT_KEY, out tenant))
        {
          sportLocations = ExpandTenantLocations(tenant, sportLocations);
        }

        viewLocations = sportLocations.Concat(viewLocations);
      }

      return viewLocations;
    }

    private IEnumerable<string> ExpandTenantLocations(string tenant, IEnumerable<string> defaultLocations)
    {
      foreach (var location in defaultLocations)
      {
        yield return location.Replace("{0}", $"{{0}}_{tenant}");
        yield return location;
      }
    }
  }
}
