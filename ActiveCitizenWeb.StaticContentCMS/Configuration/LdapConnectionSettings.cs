using ActiveCitizen.LDAP.IdentityProvider;

namespace ActiveCitizenWeb.StaticContentCMS.Configuration
{
    public class LdapConnectionSettings : ILdapConnectionSettings
    {
        public string Server { get; set; }
        public string BasePathDN { get; set; }
        public string ServiceUserDN { get; set; }
        public string ServiceUserPassword { get; set; }
        public string UserIdAttributeKey { get; set; }
        public string UserNameAttributeKey { get; set; }
        public string UserNameTransformTemplate { get; set; }

        //TODO rewrite to read from web.config 
        public LdapConnectionSettings(string section)
        {
            if (section == "AD")
            {
                // Microsoft Active Directory sample settings
                UserNameTransformTemplate = "{0}";
                Server = "AD.company";
                BasePathDN = "OU=Users,OU=Company,DC=AD,DC=company";
                ServiceUserDN = "CN=<User>,OU=Users,OU=Company,DC=AD,DC=company";
                ServiceUserPassword = "<password>";
                UserIdAttributeKey = "objectGUID"; //this must be a unique identifier of a user across the catalog, DN does not suites since it can be too long. MVC UserManager limits external id length to 128 unicode characters.
                UserNameAttributeKey = "userPrincipalName"; //can also use 'sAMAccountName', 'mail' or a bunch of others suitable attributes as a login name
                return;
            }

            // Apache Directory Server sample settings
            UserNameTransformTemplate = "{0}@localhost";
            Server = "localhost:10389"; //default Apache Directory Server port
            BasePathDN = "ou=system";
            ServiceUserDN = "uid=admin,ou=system";
            ServiceUserPassword = "secret"; //default Apache Directory Server admin password
            UserIdAttributeKey = "entryUUID"; //this must be a unique identifier of a user across the catalog
            UserNameAttributeKey = "cn"; //can also use 'uid' or a bunch of others suitable attributes as a login name depending on actual catalog schema
        }
    }
}