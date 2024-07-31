using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using Microsoft.AspNet.Identity;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<Role> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public RoleController(Microsoft.AspNetCore.Identity.RoleManager<Role> roleManager, IWebHostEnvironment env, IMapper mapper)
        {
            _roleManager = roleManager;
            _env = env;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(new RoleListDto
            {
                Roles = roles,
                ResultStatus = ResultStatus.Success
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleListDto = JsonSerializer.Serialize(new RoleListDto
            {
                Roles = roles,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(roleListDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_RoleAddPartial");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(RoleAddDto roleAddDto)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<Role>(roleAddDto);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    var roleAddAjaxModel = JsonSerializer.Serialize(new RoleAddAjaxViewModel
                    {
                        RoleDto = new RoleDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{role.Name} adlı rol başarıyla eklenmiştir.",
                            Role = role
                        },
                        RoleAddPartial = await this.RenderViewToStringAsync("_RoleAddPartial", roleAddDto)
                    });
                    return Json(roleAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    var roleAddAjaxErrorModel = JsonSerializer.Serialize(new RoleAddAjaxViewModel
                    {
                        RoleAddDto = roleAddDto,
                        RoleAddPartial = await this.RenderViewToStringAsync("_RoleAddPartial", roleAddDto)
                    });
                    return Json(roleAddAjaxErrorModel);
                }

            }
            var roleAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new RoleAddAjaxViewModel
            {
                RoleAddDto = roleAddDto,
                RoleAddPartial = await this.RenderViewToStringAsync("_RoleAddPartial", roleAddDto)
            });
            return Json(roleAddAjaxModelStateErrorModel);

        }
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                var deletedRole = JsonSerializer.Serialize(new RoleDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{role.Name} adlı rol başarıyla silinmiştir.",
                    Role = role
                });
                return Json(deletedRole);
            }
            else
            {
                string errorMessages = String.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessages = $"*{error.Description}\n";
                }

                var deletedRoleErrorModel = JsonSerializer.Serialize(new RoleDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message =
                        $"{role.Name} adlı role silinirken bazı hatalar oluştu.\n{errorMessages}",
                    Role = role
                });
                return Json(deletedRoleErrorModel);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int roleId)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            var roleUpdateDto = _mapper.Map<RoleUpdateDto>(role);
            return PartialView("_RoleUpdatePartial", roleUpdateDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(RoleUpdateDto roleUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldRole = await _roleManager.FindByIdAsync(roleUpdateDto.Id.ToString());
                var updatedRole = _mapper.Map<RoleUpdateDto, Role>(roleUpdateDto, oldRole);
                var result = await _roleManager.UpdateAsync(updatedRole);
                if (result.Succeeded)
                {

                    var roleUpdateViewModel = JsonSerializer.Serialize(new RoleUpdateAjaxViewModel
                    {
                        RoleDto = new RoleDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{updatedRole.Name} adlı rol başarıyla güncellenmiştir.",
                            Role = updatedRole
                        },
                        RoleUpdatePartial = await this.RenderViewToStringAsync("_RoleUpdatePartial", roleUpdateDto)
                    });
                    return Json(roleUpdateViewModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var roleUpdateErorViewModel = JsonSerializer.Serialize(new RoleUpdateAjaxViewModel
                    {
                        RoleUpdateDto = roleUpdateDto,
                        RoleUpdatePartial = await this.RenderViewToStringAsync("_RoleUpdatePartial", roleUpdateDto)
                    });
                    return Json(roleUpdateErorViewModel);
                }

            }
            else
            {
                var roleUpdateModelStateErrorViewModel = JsonSerializer.Serialize(new RoleUpdateAjaxViewModel
                {
                    RoleUpdateDto = roleUpdateDto,
                    RoleUpdatePartial = await this.RenderViewToStringAsync("_RoleUpdatePartial", roleUpdateDto)
                });
                return Json(roleUpdateModelStateErrorViewModel);
            }
        }




    }
}
