using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizen.Common.Attributes
{
    public enum DeviceType { WebSite, MobileApp };

    [AttributeUsage(AttributeTargets.Class)]
    public class AvailableAttribute : Attribute
    {
        public DeviceType Device { get; set; }
    }
}
