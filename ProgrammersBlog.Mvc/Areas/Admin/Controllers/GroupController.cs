using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _groupService.GetAllByNonDeleted();
            return View(result.Data);

        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_GroupAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(GroupAddDto groupAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _groupService.Add(groupAddDto, "Alper Tunga");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var groupAddAjaxModel = JsonSerializer.Serialize(new GroupAddAjaxViewModel
                    {
                        GroupDto = result.Data,
                        GroupAddPartial = await this.RenderViewToStringAsync("_GroupAddPartial", groupAddDto)
                    });
                    return Json(groupAddAjaxModel);
                }
            }
            var groupAddAjaxErrorModel = JsonSerializer.Serialize(new GroupAddAjaxViewModel
            {
                GroupAddPartial = await this.RenderViewToStringAsync("_GroupAddPartial", groupAddDto)
            });
            return Json(groupAddAjaxErrorModel);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int groupId)
        {
            var result = await _groupService.GetGroupUpdateDto(groupId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_GroupUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(GroupUpdateDto groupUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _groupService.Update(groupUpdateDto, "Alper Tunga");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var groupUpdateAjaxViewModel = JsonSerializer.Serialize(new GroupUpdateAjaxViewModel
                    {
                        GroupDto = result.Data,
                        GroupUpdatePartial = await this.RenderViewToStringAsync("_GroupUpdatePartial", groupUpdateDto)
                    });
                    return Json(groupUpdateAjaxViewModel);
                }
            }
            var groupUpdateAjaxErrorModel = JsonSerializer.Serialize(new GroupUpdateAjaxViewModel
            {
                GroupUpdatePartial = await this.RenderViewToStringAsync("_GroupUpdatePartial", groupUpdateDto)
            });
            return Json(groupUpdateAjaxErrorModel);

        }

        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _groupService.GetAllByNonDeleted();
            var groups = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(groups);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int groupId)
        {
            var result = await _groupService.Delete(groupId, "Alper Tunga");
            var deletedGroup = JsonSerializer.Serialize(result.Data);
            return Json(deletedGroup);
        }
    }
}
