using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizen.Model.User
{
    class Address
    {
        public virtual string Country { get; set; }

        //https://en.wikipedia.org/wiki/Federal_districts_of_Russia
        public virtual string FederalDistrict { get; set; }
        public virtual string FederalSubject { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Flat { get; set; }
    }
}
