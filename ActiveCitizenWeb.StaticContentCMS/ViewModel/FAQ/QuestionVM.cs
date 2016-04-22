using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCitizen.Model.StaticContent.FAQ;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.FAQ
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
        public FaqListCategory Category { get; set; }

        public List<SelectListItem> CategoryNames { get; set; }
    }
}