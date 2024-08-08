using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class EventAddAjaxViewModel
    {
        public EventAddDto EventAddDto { get; set; }
        public string EventAddPartial { get; set; }
        public EventDto EventDto { get; set; }
    }
}
