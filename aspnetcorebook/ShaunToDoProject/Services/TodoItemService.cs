using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShaunToDoProject.Data;
using ShaunToDoProject.Models;

namespace ShaunToDoProject.Services
{
  public class TodoItemService : ITodoItemService
  {

    private readonly ApplicationDbContext _context;

    public TodoItemService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
    {

      return await _context.Items
        .Where(x => x.IsDone == false)
        .ToArrayAsync();

    }

    public async Task<bool> AddItemAsync(NewTodoItem newItem)
    {
      var entity = new TodoItem
      {
        Id = Guid.NewGuid(),
        IsDone = false,
        Title = newItem.Title,
        DueAt = newItem.DueAt // DateTimeOffset.Now.AddDays(3)
      };

      _context.Items.Add(entity);

      var saveResult = await _context.SaveChangesAsync();

      return saveResult == 1;

    }

    public async Task<bool> MarkDoneAsync(Guid id)
    {
      var item = await _context.Items
        .Where(x => x.Id == id)
        .SingleOrDefaultAsync();

      if (item == null) return false;

      item.IsDone = true;

      var saveResult = await _context.SaveChangesAsync();

      return saveResult == 1; // only one entity should have been updated, so this should be a 1 if successful
    }
  }
}
