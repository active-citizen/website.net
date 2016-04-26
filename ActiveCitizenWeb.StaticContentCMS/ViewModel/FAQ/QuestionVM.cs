using System.Collections.Generic;
using ActiveCitizen.Model.StaticContent.Faq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.Faq
{
    public class QuestionVM : BaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [AllowHtml]
        [Required]
        public string Answer { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoryNames { get; set; }
    }
}