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
  }
}