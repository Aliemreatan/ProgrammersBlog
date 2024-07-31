using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class RoleUpdateAjaxViewModel
    {
        public RoleUpdateDto RoleUpdateDto { get; set; }
        public string RoleUpdatePartial { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}
