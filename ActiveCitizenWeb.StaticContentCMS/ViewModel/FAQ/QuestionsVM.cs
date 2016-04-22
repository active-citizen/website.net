using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCitizen.Model.StaticContent.FAQ;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.FAQ
{
    public class QuestionsVM : BaseViewModel
    {
        public List<FaqListItem> Questions { get; set; }
        public List<FaqListCategory> Categories { get; set; }
    }
}