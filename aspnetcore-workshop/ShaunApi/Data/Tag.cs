using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunApi.Data
{
  public class Tag : ShaunDTO.Tag
  {
    public virtual ICollection<SessionTag> SessionTags { get; set; }
  }
}
