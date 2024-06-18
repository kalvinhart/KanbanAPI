namespace KanbanAPI.Business.Boards.Exceptions;

public class ColumnNotFoundException : InvalidOperationException
{
    public ColumnNotFoundException(string columnName) : base($"Column \"{columnName}\" not found.")
    {}
}