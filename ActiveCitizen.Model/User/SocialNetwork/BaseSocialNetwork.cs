using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;
using System.Security;

namespace ActiveCitizen.Model.User.SocialNetwork
{
    abstract class BaseSocialNetwork
    {
        public string Name { get; set; }

        //how to display social network icon
        public Image ViewImage { get; set; }

        public SecureString Token { get; set; }
    }
}
