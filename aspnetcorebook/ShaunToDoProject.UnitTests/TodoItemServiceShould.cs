using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShaunToDoProject.Data;
using ShaunToDoProject.Models;
using ShaunToDoProject.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ShaunToDoProject.UnitTests
{
  public class TodoItemServiceShould
  {

    [Fact]
    public async Task AddNewItem()
    {

      var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

      using (var inMemoryContext = new ApplicationDbContext(options))
      {
        var services = new TodoItemService(inMemoryContext);

        var fakeUser = new ApplicationUser
        {
          Id = "fake-000",
          UserName = "fake@fake"
        };

        await services.AddItemAsync(new NewTodoItem { Title = "Testing?", DueAt = DateTimeOffset.Now.AddDays(3) }, fakeUser);

      }

      using (var inMemoryContext = new ApplicationDbContext(options))
      {
        Assert.Equal(1, await inMemoryContext.Items.CountAsync());

        var item = await inMemoryContext.Items.FirstAsync();
        Assert.Equal("Testing?", item.Title);
        Assert.False(item.IsDone);
        Assert.True(DateTimeOffset.Now.AddDays(3) - item.DueAt < TimeSpan.FromSeconds(1));

      }

    }

    [Fact]
    public async Task MarkDone()
    {

      var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

        var fakeUser = new ApplicationUser
        {
          Id = "fake-000",
          UserName = "fake@fake"
        };

      Guid realitem_id;
      Guid fakeitem_id = Guid.NewGuid();

      using (var inMemoryContext = new ApplicationDbContext(options))
      {
        var services = new TodoItemService(inMemoryContext);

        await services.AddItemAsync(new NewTodoItem { Title = "Testing?", DueAt = DateTimeOffset.Now.AddDays(3) }, fakeUser);

        realitem_id = inMemoryContext.Items.FirstAsync().Result.Id;

      }

      using (var inMemoryContext = new ApplicationDbContext(options))
      {
        var services = new TodoItemService(inMemoryContext);

        var fakeresult = services.MarkDoneAsync(fakeitem_id, fakeUser);

        Assert.False(fakeresult.Result);

        var realresult = services.MarkDoneAsync(realitem_id, fakeUser);

        Assert.True(realresult.Result);

      }


    }

  }
}
