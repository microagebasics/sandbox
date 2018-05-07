﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShaunDTO;
using ShaunFrontEnd.Services;
using Microsoft.AspNetCore.Authorization;

namespace ShaunFrontEnd.Pages
{
  public class IndexModel : PageModel
  {

    protected readonly IApiClient _apiClient;
    private readonly IAuthorizationService _authzService;

    public IndexModel(IApiClient apiClient, IAuthorizationService authzService)
    {
      _apiClient = apiClient;
      _authzService = authzService;
    }

    public IEnumerable<IGrouping<DateTimeOffset?, SessionResponse>> Sessions { get; set; }
    public IEnumerable<int> UserSessions { get; set; }

    public IEnumerable<(int Offset, DayOfWeek? DayofWeek)> DayOffsets { get; set; }

    public int CurrentDayOffset { get; set; }

    public bool IsAdmin { get; set; }

    public async Task OnGet(int day = 0)
    {

      var authzResult = await _authzService.AuthorizeAsync(User, "Admin");
      IsAdmin = authzResult.Succeeded;

      CurrentDayOffset = day;

      var sessions = await GetSessionsAsync();

      var startDate = sessions.Min(s => s.StartTime?.Date);
      var endDate = sessions.Max(s => s.EndTime?.Date);

      var numberOfDays = ((endDate - startDate)?.Days) + 1;

      DayOffsets = Enumerable.Range(0, numberOfDays ?? 0)
          .Select(offset => (offset, (startDate?.AddDays(offset))?.DayOfWeek));

      var filterDate = startDate?.AddDays(day);

      Sessions = sessions.Where(s => s.StartTime?.Date == filterDate)
                         .OrderBy(s => s.TrackId)
                         .GroupBy(s => s.StartTime)
                         .OrderBy(g => g.Key);

      var usersessions = await _apiClient.GetSessionsByAttendeeAsync(User.Identity.Name);

      UserSessions = usersessions.Select(s => s.ID);


    }

    protected virtual Task<List<SessionResponse>> GetSessionsAsync()
    {
      return _apiClient.GetSessionsAsync();
    }

  }
}
