

namespace ActiveCitizen.Model.StaticContent.FAQ
{
    public class FaqListItem
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public int Order { get; set; }

        public virtual FaqListCategory Category { get; set; }
    }
}
