using System;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    public class LdapConnectionSettings
    {
        public string Server { get; set; }
        public string BaseDN { get; set; }
        public string ServiceUserDN { get; set; }
        public string ServiceUserPassword { get; set; }
        public string UserIdAttributeName { get; set; }
        public string UserNameAttributeName { get; set; }

        private static readonly Lazy<LdapConnectionSettings> _defaults = new Lazy<LdapConnectionSettings>(()=>
        {
            var connectionSettings = new LdapConnectionSettings { Server = "localhost:10389", BaseDN = "ou=system", ServiceUserDN = "uid=admin,ou=system", ServiceUserPassword = "<password goes here>", UserIdAttributeName = "DN", UserNameAttributeName = "cn" };

            return connectionSettings;
        });

        public static LdapConnectionSettings GetDefaults()
        {
            return _defaults.Value;
        }
    }
}