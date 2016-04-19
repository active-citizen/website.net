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
    class PollSection : BaseSection
    {
        public DateTime PollStartDate { get; set; }
        public DateTime PollEndDate { get; set; }
    }
}
