using System.Web.Mvc;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel
{
    public class BaseViewModel
    {
        //TODO: should display an error
        public HandleErrorInfo Error { get; set; }
    }
}