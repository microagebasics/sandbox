using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunApi.Data
{
  public class Attendee : ShaunDTO.Attendee
  {
    public virtual ICollection<ConferenceAttendee> ConferenceAttendees { get; set; }

    public virtual ICollection<Session> Sessions { get; set; }
  }
}
