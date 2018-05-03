using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShaunToDoProject.Models;
using ShaunToDoProject.Services;

namespace ShaunToDoProject.Controllers
{
  public class TodoController : Controller
  {

    private readonly ITodoItemService _todoItemService;

    public TodoController(ITodoItemService todoItemService)
    {
      _todoItemService = todoItemService;
    }

    public async Task<IActionResult> Index()
    {
      // Get to-do items from database
      var todoItems = await _todoItemService.GetIncompleteItemsAsync();
      // Put items into a model
      var model = new TodoViewModel()
      {
        Items = todoItems
      };

      // Retnder view using the model

      return View(model);
    }

    public async Task<IActionResult> AddItem(NewTodoItem newItem)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var success = await _todoItemService.AddItemAsync(newItem);

      if (!success)
      {
        return BadRequest(new { error = "Could not add Item" });
      }

      return Ok();
    }

    public async Task<IActionResult> MarkDone(Guid id)
    {
      if (id == Guid.Empty) return BadRequest();

      var success = await _todoItemService.MarkDoneAsync(id);

      if (!success) return BadRequest();

      return Ok();

    }

  }
}