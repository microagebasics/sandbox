using ShaunApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunApi.Infrastructure
{
    public static class EntityExtensions
    {

    public static ShaunDTO.SessionResponse MapSessionResponse(this Session session) =>
      new ShaunDTO.SessionResponse
      {
        ID = session.ID,
        Title = session.Title,
        StartTime = session.StartTime,
        EndTime = session.EndTime,
        Tags = session.SessionTags?
                        .Select(st => new ShaunDTO.Tag
                        {
                          ID = st.TagID,
                          Name = st.Tag.Name
                        })
                         .ToList(),
        Speakers = session.SessionSpeakers?
                            .Select(ss => new ShaunDTO.Speaker
                            {
                              ID = ss.SpeakerId,
                              Name = ss.Speaker.Name
                            })
                             .ToList(),
        TrackId = session.TrackId,
        Track = new ShaunDTO.Track
        {
          TrackID = session?.TrackId ?? 0,
          Name = session.Track?.Name
        },
        ConferenceID = session.ConferenceID,
        Abstract = session.Abstract
      };

    public static ShaunDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
       new ShaunDTO.SpeakerResponse
       {
         ID = speaker.ID,
         Name = speaker.Name,
         Bio = speaker.Bio,
         Website = speaker.Website,
         Sessions = speaker.SessionSpeakers?
               .Select(ss =>
                   new ShaunDTO.Session
                   {
                     ID = ss.SessionId,
                     Title = ss.Session.Title
                   })
               .ToList()
       };

    public static ShaunDTO.AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
     new ShaunDTO.AttendeeResponse
     {
       ID = attendee.ID,
       FirstName = attendee.FirstName,
       LastName = attendee.LastName,
       UserName = attendee.UserName,
       Sessions = attendee.Sessions?
             .Select(s =>
                 new ShaunDTO.Session
                 {
                   ID = s.ID,
                   Title = s.Title,
                   StartTime = s.StartTime,
                   EndTime = s.EndTime
                 })
             .ToList(),
       Conferences = attendee.ConferenceAttendees?
             .Select(ca =>
                 new ShaunDTO.Conference
                 {
                   ID = ca.ConferenceID,
                   Name = ca.Conference.Name
                 })
             .ToList(),
     };

  }
}
