﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunToDoProject.Models
{
  public class NewTodoItem
  {

    [Required]
    public string Title { get; set; }

    public DateTimeOffset? DueAt { get; set; }

  }
}
