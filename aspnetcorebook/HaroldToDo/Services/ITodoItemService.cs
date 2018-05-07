using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaroldToDo.Models;

namespace HaroldToDo.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync();
    }
}
