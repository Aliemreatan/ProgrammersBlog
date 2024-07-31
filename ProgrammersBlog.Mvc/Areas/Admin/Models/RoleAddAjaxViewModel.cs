using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class RoleAddAjaxViewModel
    {
        public RoleAddDto RoleAddDto { get; set; }
        public string RoleAddPartial { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}
