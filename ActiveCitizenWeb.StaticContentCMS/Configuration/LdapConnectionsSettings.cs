using ActiveCitizen.LDAP.IdentityProvider;
using System;
using System.Configuration;
using System.Linq;

namespace ActiveCitizenWeb.StaticContentCMS.Configuration
{
    public class LdapConnectionsConfigurationSection : ConfigurationSection
    {
        public const string SectionName = "ldapConnections";

        [ConfigurationProperty("useConnection")]
        public string UseConnection
        {
            get { return (string)base["useConnection"]; }
            set { base["useConnection"] = value; }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public LdapConnectionConfigurationElementCollection LdapConnections
        {
            get { return (LdapConnectionConfigurationElementCollection)base[""]; }
        }

        public ILdapConnectionSettings GetConnectionSettings()
        {
            return !string.IsNullOrEmpty(UseConnection)
                ? LdapConnections
                    .Cast<LdapConnectionConfigurationElement>()
                    .First(element => element.Name.Equals(UseConnection, StringComparison.OrdinalIgnoreCase))
                : LdapConnections[0];
        }
    }

    public class LdapConnectionConfigurationElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LdapConnectionConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LdapConnectionConfigurationElement)element).Name;
        }

        public LdapConnectionConfigurationElement this[int idx]
        {
            get { return (LdapConnectionConfigurationElement)BaseGet(idx); }
        }
    }

    public class LdapConnectionConfigurationElement : ConfigurationElement, ILdapConnectionSettings
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("server", DefaultValue = "", IsRequired = true)]
        public string Server
        {
            get { return (string)base["server"]; }
            set { base["server"] = value; }
        }

        [ConfigurationProperty("basePathDN", DefaultValue = "", IsRequired = true)]
        public string BasePathDN
        {
            get { return (string)base["basePathDN"]; }
            set { base["basePathDN"] = value; }
        }

        [ConfigurationProperty("serviceUserDN", DefaultValue = "", IsRequired = true)]
        public string ServiceUserDN
        {
            get { return (string)base["serviceUserDN"]; }
            set { base["serviceUserDN"] = value; }
        }

        [ConfigurationProperty("serviceUserPassword", DefaultValue = "", IsRequired = true)]
        public string ServiceUserPassword
        {
            get { return (string)base["serviceUserPassword"]; }
            set { base["serviceUserPassword"] = value; }
        }

        [ConfigurationProperty("userIdAttributeKey", DefaultValue = "", IsRequired = true)]
        public string UserIdAttributeKey
        {
            get { return (string)base["userIdAttributeKey"]; }
            set { base["userIdAttributeKey"] = value; }
        }

        [ConfigurationProperty("userNameAttributeKey", DefaultValue = "", IsRequired = true)]
        public string UserNameAttributeKey
        {
            get { return (string)base["userNameAttributeKey"]; }
            set { base["userNameAttributeKey"] = value; }
        }

        [ConfigurationProperty("userNameTransformTemplate", DefaultValue = "", IsRequired = true)]
        public string UserNameTransformTemplate
        {
            get { return (string)base["userNameTransformTemplate"]; }
            set { base["userNameTransformTemplate"] = value; }
        }
    }
}