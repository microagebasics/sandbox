using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaroldToDo.Models;
using HaroldToDo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HaroldToDo.Controllers
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
            var todoItems = await _todoItemService.GetIncompleteItemsAsync();

            var model = new TodoViewModel()
            {
                Items = todoItems
            };

            return View(model);
        }
        public async Task<IActionResult> AddItem(NewTodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var successful = await _todoItemService.AddItemAsync(newitem);
            if (!successful)
            {
                return BadRequest(new { error = "Could not add item" });
            }

            return Ok();
        }
    }
}