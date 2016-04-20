using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCitizen.Model.StaticContent.FAQ;
using System.Web.Mvc;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.FAQ
{
    public class QuestionVM
    {
        public int Id { get; set; }

        public string Question { get; set; }

        [AllowHtml]
        public string Answer { get; set; }

        public int Order { get; set; }

        public FaqListCategory Category { get; set; }

        public List<SelectListItem> CategoryNames { get; set; }
    }
}