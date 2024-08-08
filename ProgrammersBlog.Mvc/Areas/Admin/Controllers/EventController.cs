
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

namespace MyProject.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
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


    }
}
