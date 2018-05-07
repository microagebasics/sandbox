using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShaunDTO;
using ShaunFrontEnd.Services;

namespace ShaunFrontEnd.Pages
{
  public class MyAgendaModel : IndexModel
  {

    public MyAgendaModel(IApiClient client, IAuthorizationService authzService) : base(client, authzService)
    {

    }

    protected override Task<List<SessionResponse>> GetSessionsAsync()
    {
      return _apiClient.GetSessionsByAttendeeAsync(User.Identity.Name);
    }

  }
}