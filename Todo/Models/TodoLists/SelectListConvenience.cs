using Microsoft.AspNetCore.Mvc.Rendering;
using Todo.Data.Entities;

namespace Todo.Models.TodoLists
{
    public static class SelectListConvenience
    {
        public static readonly SelectListItem[] OrderSelectListItems =
        {
            new SelectListItem {Text = "Importance", Value = Order.Importance.ToString()},
            new SelectListItem {Text = "Rank", Value = Order.Rank.ToString()},
        };
    }
}
