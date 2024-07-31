using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class GroupUpdateAjaxViewModel
    {
        public GroupUpdateDto GroupUpdateDto { get; set; }
        public string GroupUpdatePartial { get; set; }
        public GroupDto GroupDto { get; set; }
    }
}
