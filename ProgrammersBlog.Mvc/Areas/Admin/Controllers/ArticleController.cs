using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyProject.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeleted();
            return View(result.Data);

        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_ArticleAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _articleService.Add(articleAddDto, "Alper Tunga");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var articleAddAjaxModel = JsonSerializer.Serialize(new ArticleAddAjaxViewModel
                    {
                        ArticleDto = result.Data,
                        ArticleAddPartial = await this.RenderViewToStringAsync("_ArticleAddPartial", articleAddDto)
                    });
                    return Json(articleAddAjaxModel);
                }
            }
            var articleAddAjaxErrorModel = JsonSerializer.Serialize(new ArticleAddAjaxViewModel
            {
                ArticleAddPartial = await this.RenderViewToStringAsync("_ArticleAddPartial", articleAddDto)
            });
            return Json(articleAddAjaxErrorModel);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int articleId)
        {
            var result = await _articleService.GetArticleUpdateDto(articleId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_ArticleUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _articleService.Update(articleUpdateDto, "Alper Tunga");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var articleUpdateAjaxModel = JsonSerializer.Serialize(new ArticleUpdateAjaxViewModel
                    {
                        ArticleDto = result.Data,
                        ArticleUpdatePartial = await this.RenderViewToStringAsync("_ArticleUpdatePartial", articleUpdateDto)
                    });
                    return Json(articleUpdateAjaxModel);
                }
            }
            var articleUpdateAjaxErrorModel = JsonSerializer.Serialize(new ArticleUpdateAjaxViewModel
            {
                ArticleUpdatePartial = await this.RenderViewToStringAsync("_ArticleUpdatePartial", articleUpdateDto)
            });
            return Json(articleUpdateAjaxErrorModel);

        }

        public async Task<JsonResult> GetAllArticles()
        {
            var result = await _articleService.GetAllByNonDeleted();
            var articles = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int articleId)
        {
            var result = await _articleService.Delete(articleId, "Alper Tunga");
            var deletedArticle = JsonSerializer.Serialize(result.Data);
            return Json(deletedArticle);
        }
    }
}
