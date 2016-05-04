using ActiveCitizen.LDAP.IdentityProvider;
using System.Configuration;

namespace ActiveCitizenWeb.StaticContentCMS.Configuration
{
    public class LdapConnectionSettingsInitializer
    {
        public ILdapConnectionSettings Initialize()
        {
            var section = (LdapConnectionsConfigurationSection)ConfigurationManager.GetSection(LdapConnectionsConfigurationSection.SectionName);

            return section.GetConnectionSettings();
        }
    }
}