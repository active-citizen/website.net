using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizen.Model.Shared
{
    //amount of point for some action
    interface IPoint
    {
        int PointsAmount { get; set; }
    }
}
