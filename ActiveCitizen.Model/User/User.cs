using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security;
using ActiveCitizen.Model.User.SocialNetwork;

namespace ActiveCitizen.Model.User
{
    class User
    {
        [Key]
        [Required]
        public SecureString AG_ID { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public SecureString Password { get; set; }

        #region UserProfile
        public string Name { get; set; }
        public string SirName { get; set; }
        public string Patronymic { get; set; }

        public bool? Sex { get; set; }
        public DateTime? Birthday { get; set; }

        public string EMail { get; set; }
        public bool? MeritalStatus { get; set; }
        public short? Childrens { get; set; }

        public Address RegistrationAddress { get; set; }
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }

        //TODO: value from directory
        //TODO: it will be spletted into two properties at the future
        public string Occupation { get; set; }

        public Moscow.Link2MPGU Link2MPGU { get; set; }

        public List<BaseSocialNetwork> SocialNetworks { get; set; }
        public bool OfferShareToSocialNetwork { get; set; }

        public List<Badge.BaseBadge> Badges { get; set; }
        #endregion UserProfile


    }
}
