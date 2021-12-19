using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services;
using Xunit;

namespace Todo.Tests
{
    public class WhenUserIsOwnerOrHasItemsInList : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture fixture;

        public WhenUserIsOwnerOrHasItemsInList(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void WhenUserHasNoItemsInOtherListsOnlyHisListsAreShown()
        {
            var user1Lists = this.fixture.DbContext.RelevantTodoLists(this.fixture.User1.Id).ToList();
            Assert.Single(user1Lists);
        }

        [Fact]
        public void WhenUserHasItemsInOhterListsOtherListsAreShownToo()
        {
            var user2Lists = this.fixture.DbContext.RelevantTodoLists(this.fixture.User2.Id).ToList();
            Assert.Equal(2, user2Lists.Count);
        }
    }

    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext DbContext { get; set; }
        public IdentityUser User1 { get; set; }
        public IdentityUser User2 { get; set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "todoDatabase")
                .Options;

            DbContext = new ApplicationDbContext(options);
            User1 = new IdentityUser("user1");
            User2 = new IdentityUser("user2");
            DbContext.Users.Add(User1);
            DbContext.Users.Add(User2);

            var list1 = new TodoList(User1, "firstList");
            var list2 = new TodoList(User2, "secondList");
            DbContext.TodoLists.Add(list1);
            DbContext.TodoLists.Add(list2);

            DbContext.TodoItems.Add(new TodoItem(list1.TodoListId, User1.Id, "firstItem", Importance.High));
            DbContext.TodoItems.Add(new TodoItem(list1.TodoListId, User1.Id, "secondItem", Importance.High));
            DbContext.TodoItems.Add(new TodoItem(list1.TodoListId, User2.Id, "thirdItem", Importance.High));
            DbContext.TodoItems.Add(new TodoItem(list2.TodoListId, User2.Id, "fourthItem", Importance.High));

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
