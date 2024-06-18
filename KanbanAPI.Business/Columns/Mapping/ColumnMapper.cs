using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.DataAccess.Columns.Entities;
using Riok.Mapperly.Abstractions;

namespace KanbanAPI.Business.Columns.Mapping;

[Mapper]
public partial class ColumnMapper : IColumnMapper
{
    public partial Column ToColumn(UpdateColumnDto columnDto);
}