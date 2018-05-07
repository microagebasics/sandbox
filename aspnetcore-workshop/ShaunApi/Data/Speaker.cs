
using System.Collections.Generic;

namespace ShaunApi.Data
{
  public class Speaker : ShaunDTO.Speaker
  {
    public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
  }

}
