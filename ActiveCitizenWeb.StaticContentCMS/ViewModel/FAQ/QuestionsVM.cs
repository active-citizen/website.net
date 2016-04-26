using System.Collections.Generic;
using ActiveCitizen.Model.StaticContent.Faq;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.Faq
{
    public class QuestionsVM : BaseViewModel
    {
        public List<FaqListItem> Questions { get; set; }
        public List<FaqListCategory> EmptyCategories { get; set; }
        public bool CanAddQuestions { get; set; }
    }
}