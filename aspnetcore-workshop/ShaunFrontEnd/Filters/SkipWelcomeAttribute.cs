using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShaunFrontEnd.Filters
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class SkipWelcomeAttribute : Attribute, IFilterMetadata
  {

  }
}
