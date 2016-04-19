using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;
using ActiveCitizen.Common.Attributes;

namespace ActiveCitizen.Model.StaticContent.Result
{
    [Available(Device = DeviceType.WebSite)]
    abstract class BaseSection
    {
        public virtual string Title { get; set; }
        public HtmlBody Body { get; set; }
    }
}
