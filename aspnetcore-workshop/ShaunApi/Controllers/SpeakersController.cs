﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShaunApi.Data;
using ShaunApi.Infrastructure;

namespace ShaunApi.Controllers
{
  [Route("api/[controller]")]
  public class SpeakersController : Controller
  {
    private readonly ApplicationDbContext _db;

    public SpeakersController(ApplicationDbContext db)
    {
      _db = db;
    }

    // GET: api/Speakers
    [HttpGet]
    public async Task<IActionResult> GetSpeakers()
    {
      var speakers = await _db.Speakers.AsNoTracking()
                                  .Include(s => s.SessionSpeakers)
                                  .ThenInclude(ss => ss.Session)
                                  .ToListAsync();

      var result = speakers.Select(s => s.MapSpeakerResponse());
      return Ok(result);
    }

    // GET: api/Speakers/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSpeaker([FromRoute]int id)
    {
      var speaker = await _db.Speakers.AsNoTracking()
                                      .Include(s => s.SessionSpeakers)
                                          .ThenInclude(ss => ss.Session)
                                      .SingleOrDefaultAsync(s => s.ID == id);
      if (speaker == null)
      {
        return NotFound();
      }
      var result = speaker.MapSpeakerResponse();
      return Ok(result);
    }

    // POST: api/Speakers
    [HttpPost]
    public async Task<IActionResult> CreateSpeaker([FromBody]ShaunDTO.Speaker input)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var speaker = new Speaker
      {
        Name = input.Name,
        Website = input.Website,
        Bio = input.Bio
      };

      _db.Speakers.Add(speaker);
      await _db.SaveChangesAsync();

      var result = speaker.MapSpeakerResponse();

      return CreatedAtAction(nameof(GetSpeaker), new { id = speaker.ID }, result);
    }

    // PUT: api/Speakers/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSpeaker([FromRoute]int id, [FromBody]ShaunDTO.Speaker input)
    {
      var speaker = await _db.FindAsync<Speaker>(id);

      if (speaker == null)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      speaker.Name = input.Name;
      speaker.Website = input.Website;
      speaker.Bio = input.Bio;

      // TODO: Handle exceptions, e.g. concurrency
      await _db.SaveChangesAsync();

      var result = speaker.MapSpeakerResponse();

      return Ok(result);
    }

    // DELETE: api/Speakers/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSpeaker([FromRoute]int id)
    {
      var speaker = await _db.FindAsync<Speaker>(id);

      if (speaker == null)
      {
        return NotFound();
      }

      _db.Remove(speaker);

      // TODO: Handle exceptions, e.g. concurrency
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool SpeakerExists(int id)
    {
      return _db.Speakers.Any(e => e.ID == id);
    }
  }
}