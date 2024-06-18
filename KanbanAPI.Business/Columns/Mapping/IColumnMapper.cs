using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.DataAccess.Columns.Entities;

namespace KanbanAPI.Business.Columns.Mapping;

public interface IColumnMapper
{
    Column ToColumn(UpdateColumnDto columnDto);
}