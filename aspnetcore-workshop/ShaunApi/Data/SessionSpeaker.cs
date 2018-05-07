﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunApi.Data
{
  public class SessionSpeaker
  {
    public int SessionId { get; set; }

    public Session Session { get; set; }

    public int SpeakerId { get; set; }

    public Speaker Speaker { get; set; }
  }
}
