using System.ComponentModel.DataAnnotations;

namespace MyProject.WebUi.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen rol adı giriniz.")]
        public string Name { get; set; }
    }
}
