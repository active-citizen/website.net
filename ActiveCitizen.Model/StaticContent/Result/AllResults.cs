using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Common.Attributes;

namespace ActiveCitizen.Model.StaticContent.Result
{
    [Available(Device = DeviceType.WebSite)]
    class AllResults
    {
        public List<CityResult> CityRelusts { get; set; }
        public List<LocalResult> LocalResults { get; set; }
    }
}
