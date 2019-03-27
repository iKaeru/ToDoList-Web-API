namespace Client.Models.ToDoItems
{
    /// <summary>
    /// Информация для создания списка дел
    /// </summary>
    public sealed class ToDoItemBuildInfo
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}