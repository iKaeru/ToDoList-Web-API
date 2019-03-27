using System;
using Model = Models.ToDoItems;
using View = Client.Models.ToDoItems;

namespace Converters
{
    public class ToDoItemBuildInfoConverter
    {
        public static Model.ToDoItemBuildInfo Convert(View.ToDoItemBuildInfo viewItem)
        {
            if (viewItem == null)
            {
                throw new ArgumentNullException(nameof(viewItem));
            }

            var modelItem = new Model.ToDoItemBuildInfo()
            {
                Name = viewItem.Name,
                IsComplete = viewItem.IsComplete
            };

            return modelItem;
        }
    }
}