using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunApi.Data
{
  public class Session : ShaunDTO.Session
  {
    public Conference Conference { get; set; }

    public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; }

    public Track Track { get; set; }

    public virtual ICollection<SessionTag> SessionTags { get; set; }
  }
}
