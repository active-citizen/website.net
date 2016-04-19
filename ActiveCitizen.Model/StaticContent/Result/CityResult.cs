using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Common.Attributes;

namespace ActiveCitizen.Model.StaticContent.Result
{
    [Available(Device = DeviceType.WebSite)]
    class CityResult
    {
        public PollSection Poll { get; set; }
        public StatisticSection Statistic { get; set; }
        public SolutionSection Solution { get; set; }

        //get month and order
        public DateTime Date { get; set; }

        //whether to duplicate the result at the last sercion
        public bool IsAChoiseOfAC { get; set; }
    }
}
