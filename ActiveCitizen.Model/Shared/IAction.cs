using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizen.Model.Shared
{
    interface IAction
    {
        //whether a user complete voting, or checked in, or marked novelty and etc.
        bool IsActionComplete { get; set; }
        bool IsActionPassed { get; set; }

        DateTime? ActionCompleteDateTime { get; set; }
        IPoint CreditedWithPoints { get; set; }
    }
}
