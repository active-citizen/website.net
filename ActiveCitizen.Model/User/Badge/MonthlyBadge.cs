using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizen.Model.User.Badge
{
    class MonthlyBadge : BaseBadge
    {
        public short Year { get; set; }
        public short Month { get; set; }

    }
}
