using System.Collections.Generic;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        public Order Order { get; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, Order order)
        {
            Items = items;
            Order = order;
            TodoListId = todoListId;
            Title = title;
        }
    }
}