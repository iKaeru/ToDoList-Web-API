namespace Client.Models.ToDoItems
{
    /// <summary>
    /// Информация для редактирования записи
    /// </summary>
    public class ToDoItemPatchInfo
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}