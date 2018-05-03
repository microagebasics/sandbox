using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunToDoProject.Models
{
  public class TodoItem
  {

    public string OwnerId { get; set; }

    public Guid Id { get; set; }

    public bool IsDone { get; set; }

    public string Title { get; set; }

    public DateTimeOffset? DueAt { get; set; }

  }
}
