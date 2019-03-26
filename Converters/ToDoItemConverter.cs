using System;
using View = Client.Models.ToDoItems;
using Model = Models.ToDoItems;

namespace Converters
{
    public class ToDoItemConverter
    {
        public static Model.TodoItem ConvertViewToModel(View.TodoItem viewItem)
        {
            if (viewItem == null)
            {
                throw new ArgumentNullException(nameof(viewItem));
            }

            var modelItem = new Model.TodoItem()
            {
                Id = Guid.Parse( viewItem.Id),
                Name = viewItem.Name,
                IsComplete = viewItem.IsComplete
            };

            return modelItem;
        }
    }
}