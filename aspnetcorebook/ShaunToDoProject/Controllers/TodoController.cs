using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShaunToDoProject.Models;
using ShaunToDoProject.Services;

namespace ShaunToDoProject.Controllers
{
  [Authorize]
  public class TodoController : Controller
  {

    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TodoController(ITodoItemService todoItemService,
      UserManager<ApplicationUser> userManager)
    {
      _todoItemService = todoItemService;
      _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
      var currentUser = await _userManager.GetUserAsync(User);
      if (currentUser == null) return Challenge();

      // Get to-do items from database
      var todoItems = await _todoItemService.GetIncompleteItemsAsync(currentUser);
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

      var currentUser = await _userManager.GetUserAsync(User);
      if (currentUser == null) return Unauthorized();

      var success = await _todoItemService.AddItemAsync(newItem, currentUser);

      if (!success)
      {
        return BadRequest(new { error = "Could not add Item" });
      }

      return Ok();
    }

    public async Task<IActionResult> MarkDone(Guid id)
    {
      if (id == Guid.Empty) return BadRequest();

      var currentUser = await _userManager.GetUserAsync(User);
      if (currentUser == null) return Unauthorized();

      var success = await _todoItemService.MarkDoneAsync(id, currentUser);

      if (!success) return BadRequest();

      return Ok();

    }

  }
}