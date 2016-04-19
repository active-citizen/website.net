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
    class SolutionSection : BaseSection
    {
        public override string Title
        {
            get
            {
                return "Решение";
            }
        }

        public List<Image> Images { get; set; }
    }
}
