﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShaunDTO
{
  public class TagResponse : Tag
  {
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
  }
}
