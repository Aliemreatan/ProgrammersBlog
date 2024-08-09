
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.WebUi.Areas.Admin.Models;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace MyProject.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService; 
        private readonly Microsoft.AspNetCore.Identity.RoleManager<Role> _roleManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;

        public EventController(IEventService eventService, Microsoft.AspNetCore.Identity.RoleManager<Role> roleManager, Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            _eventService = eventService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

       

        public async Task<IActionResult> Index()
        {
            var result = await _eventService.GetAllByNonDeleted();
            return View(result.Data);

        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_EventAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(EventAddDto eventAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _eventService.Add(eventAddDto, "Alper Tunga");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var eventAddAjaxModel = JsonSerializer.Serialize(new EventAddAjaxViewModel
                    {
                        EventDto = result.Data,
                        EventAddPartial = await this.RenderViewToStringAsync("_EventAddPartial", eventAddDto)
                    });
                    return Json(eventAddAjaxModel);
                }
            }
            var eventAddAjaxErrorModel = JsonSerializer.Serialize(new EventAddAjaxViewModel
            {
                EventAddPartial = await this.RenderViewToStringAsync("_EventAddPartial", eventAddDto)
            });
            return Json(eventAddAjaxErrorModel);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int eventId)
        {
            var result = await _eventService.GetEventUpdateDto(eventId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_EventUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(EventUpdateDto eventUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _eventService.Update(eventUpdateDto, "Alper Tunga");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var eventUpdateAjaxModel = JsonSerializer.Serialize(new EventUpdateAjaxViewModel
                    {
                        EventDto = result.Data,
                        EventUpdatePartial = await this.RenderViewToStringAsync("_EventUpdatePartial", eventUpdateDto)
                    });
                    return Json(eventUpdateAjaxModel);
                }
            }
            var eventUpdateAjaxErrorModel = JsonSerializer.Serialize(new EventUpdateAjaxViewModel
            {
                EventUpdatePartial = await this.RenderViewToStringAsync("_EventUpdatePartial", eventUpdateDto)
            });
            return Json(eventUpdateAjaxErrorModel);

        }

        public async Task<JsonResult> GetAllEvents()
        {
            var result = await _eventService.GetAllByNonDeleted();
            var etkinli = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(etkinli);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int eventId)
        {
            var result = await _eventService.Delete(eventId, "Alper Tunga");
            var deletedEvent = JsonSerializer.Serialize(result.Data);
            return Json(deletedEvent);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Role role = new Role
                {
                    Name = model.Name
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        public IActionResult UserRoleList()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();
            TempData["UserId"] = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);

            List<RoleAssignViewModel> model = new List<RoleAssignViewModel>();
            foreach (var item in roles)
            {
                RoleAssignViewModel m = new RoleAssignViewModel();
                m.RoleID = item.Id;
                m.Name = item.Name;
                m.Exists = userRoles.Contains(item.Name);
                model.Add(m);
            }



            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
        {
            var userId = (int)TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            foreach (var item in model)
            {
                if (item.Exists)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }
            return RedirectToAction("UserRoleList");
        }
    }
}
