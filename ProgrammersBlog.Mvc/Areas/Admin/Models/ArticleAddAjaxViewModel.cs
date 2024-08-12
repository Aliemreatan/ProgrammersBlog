using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class ArticleAddAjaxViewModel
    {
        public ArticleAddDto ArticleAddDto { get; set; }
        public string ArticleAddPartial { get; set; }
        public ArticleDto ArticleDto { get; set; }
    }
}
