﻿using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Cards.Entities;

namespace KanbanAPI.DataAccess.Columns.Entities;

public class Column
{
    public Guid ColumnId { get; set; }
    public required string Name { get; set; }
    public int Index { get; set; }

    public Guid BoardId { get; set; }
    public required Board Board { get; set; }
    public ICollection<Card> Cards { get; set; } = new List<Card>();
}