using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShaunToDoProject.Models;

namespace ShaunToDoProject.Services
{
  public class FakeTodoItemService : ITodoItemService
  {
    public Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
    {

      IEnumerable<TodoItem> items = new[]
      {
        new TodoItem
        {
          Title = "Learn ASP.NET Core",
          DueAt = DateTimeOffset.Now.AddDays(1)
        },
        new TodoItem
        {
          Title = "Build Awesome Apps",
          DueAt = DateTimeOffset.Now.AddDays(2)
        }
      };

      return Task.FromResult(items);
    }
  }
}
