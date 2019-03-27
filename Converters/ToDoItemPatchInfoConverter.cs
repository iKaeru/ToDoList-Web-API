using System;
using Model = Models.ToDoItems;
using View = Client.Models.ToDoItems;

namespace Converters
{
    public class ToDoItemPatchInfoConverter
    {
        public static Model.ToDoItemPatchInfo Convert(View.ToDoItemPatchInfo viewItem)
        {
            if (viewItem == null)
            {
                throw new ArgumentNullException(nameof(viewItem));
            }

            var modelItem = new Model.ToDoItemPatchInfo()
            {
                Name = viewItem.Name,
                IsComplete = viewItem.IsComplete
            };

            return modelItem;
        }
    }
}