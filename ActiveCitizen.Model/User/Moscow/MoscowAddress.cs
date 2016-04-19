using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizen.Model.User.Moscow
{
    class MoscowAddress : Address
    {
        public override string Country { get { return @"Россия"; } }
        public override string FederalDistrict { get { return @"ЦФО"; } }

        //for Moscow FederalSubject is always Moscow, however City might be both Moscow, Zelenograd, Troitsk and etc.
        public override string FederalSubject { get { return @"Москва"; } }

        //https://en.wikipedia.org/wiki/Administrative_divisions_of_Moscow
        //TODO: these values should be from directory
        public string Okrug { get; set; }
        public string District { get; set; }
        public string Settlement { get; set; }
    }
}
