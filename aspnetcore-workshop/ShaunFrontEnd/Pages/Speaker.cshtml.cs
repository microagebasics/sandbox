using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShaunDTO;
using ShaunFrontEnd.Services;

namespace ShaunFrontEnd.Pages
{
  public class SpeakerModel : PageModel
  {
    private readonly IApiClient _apiClient;

    public SpeakerModel(IApiClient apiClient)
    {
      _apiClient = apiClient;
    }

    public SpeakerResponse Speaker { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
      Speaker = await _apiClient.GetSpeakerAsync(id);

      if (Speaker == null)
      {
        return NotFound();
      }

      return Page();
    }

  }
}