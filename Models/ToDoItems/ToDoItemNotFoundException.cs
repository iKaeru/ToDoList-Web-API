using System;

namespace Models.ToDoItems
{
    public class ToDoItemNotFoundException : Exception
    {
        public ToDoItemNotFoundException(Guid id) : base($"ToDo item \"{id}\" not found.")
        {
        }
    }
}