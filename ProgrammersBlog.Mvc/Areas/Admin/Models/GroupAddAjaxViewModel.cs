using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class GroupAddAjaxViewModel
    {
        public GroupAddDto GroupAddDto { get; set; }
        public string GroupAddPartial { get; set; }
        public GroupDto GroupDto { get; set; }
    }
}
