using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaasKit.Multitenancy;
using aspnetcoreapp.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace aspnetcoreapp
{
  public class CachingTenantResolver : MemoryCacheTenantResolver<Tenant>
  {

    IEnumerable<Tenant> tenants = new List<Tenant>(new[]
{
        new Tenant {
            Name = "Huron-Perth Lakers",
            Domain = "huronperthlakers.ca",
            AltDomains = new[] { "localhost:6000", "localhost:6001" }
        },
        new Tenant {
            Name = "Kitchener Minor Hockey",
            Domain = "kitchenerminorhockey.com",
            AltDomains = new[] { "localhost:6002" }
        }
    });


    private readonly AppDbContext _context;

    public CachingTenantResolver(AppDbContext context, IMemoryCache cache, ILoggerFactory loggerFactory) : base(cache, loggerFactory)
    {
      _context = context;
    }

    // Resolver runs on cache misses
    protected override async Task<TenantContext<Tenant>> ResolveAsync(HttpContext context)
    {
      var subdomain = context.Request.Host.Host.ToLower();

      var tenant = await _context.Tenants
          .FirstOrDefaultAsync(t => t.Domain == subdomain);

      if (tenant == null) // check the alt domain names
      {
        tenant = await _context.Tenants
          .FirstOrDefaultAsync(t => t.AltDomains.Any(h => h.Equals(context.Request.Host.Value.ToLower())));
      }

      if (tenant == null) return null; // todo: this should check the subdomain if the domain is mbsportsweb.ca

      return new TenantContext<Tenant>(tenant);
    }

    protected override MemoryCacheEntryOptions CreateCacheEntryOptions()
        => new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));

    protected override string GetContextIdentifier(HttpContext context)
        => context.Request.Host.Host.ToLower();

    protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<Tenant> context)
        => new string[] { context.Tenant.Domain };

  }
}
