using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreapp.Models
{
  public class Tenant
  {

    public Int16 ID { get; set; }

    public string Name { get; set; }

    public string Domain { get; set; }

    public string Subdomain { get; set; }

    public string[] AltDomains { get; set; }

    public string Sport { get; set; }

    public string Description { get; set; }

    public string ConnectionString { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

  }
}
