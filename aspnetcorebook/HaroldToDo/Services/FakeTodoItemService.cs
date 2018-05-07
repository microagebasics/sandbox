﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaroldToDo.Models;

namespace HaroldToDo.Services
{
    public class FakeTodoItemService :  ITodoItemService
    {
        public Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()

        {
            //Return an array of TodoItems
            IEnumerable<TodoItem> items = new[]
            {
                    new TodoItem
                    {
                        Title = "Learn ASP.NET Core",
                        DueAt = DateTimeOffset.Now.AddDays(2)
                        },
                    new TodoItem
                    {
                        Title = "Build awesome apps",
                        DueAt = DateTimeOffset.Now.AddDays(2)
                    }
            };

            return Task.FromResult(items);

        }   
    }
}
