using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class EventUpdateAjaxViewModel
    {
        public EventUpdateDto EventUpdateDto { get; set; }
        public string EventUpdatePartial { get; set; }
        public EventDto EventDto { get; set; }
    }
}
