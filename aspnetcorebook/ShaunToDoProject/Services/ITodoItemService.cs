using ShaunToDoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunToDoProject.Services
{
  public interface ITodoItemService
  {
    Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync();

    Task<bool> AddItemAsync(NewTodoItem newItem);
  }
}
