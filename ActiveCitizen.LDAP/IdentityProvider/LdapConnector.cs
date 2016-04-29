using System;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    public class LdapConnector : ILdapConnector
    {
        public const string filterTemplate = "(&(objectclass=person)({0}={1}))";
        private readonly ILdapConnectionSettings connectionSettings;
        private readonly string basePath;
        private readonly string serverPath;

        public LdapConnector(ILdapConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings;
            basePath = string.Format("LDAP://{0}/{1}", connectionSettings.Server, connectionSettings.BasePathDN);
            serverPath = string.Format("LDAP://{0}/", connectionSettings.Server);
        }

        public LdapEntry SearchByUserName(string userName)
        {
            string searchFilter = string.Format(filterTemplate, connectionSettings.UserNameAttributeKey, userName);
            return SearchDirectory(searchFilter);
        }

        public LdapEntry SearchByUserId(string id)
        {
            string searchFilter = string.Format(filterTemplate, connectionSettings.UserIdAttributeKey, id);
            return SearchDirectory(searchFilter);
        }

        private string GetEntryDistinguishedName(SearchResult entry)
        {
            return entry.Path.Replace(serverPath, "");
        }
        
        private string PropertyToString(SearchResult entry, string propertyName)
        {
            var prop = entry.Properties[propertyName];

            if (prop.Count == 0) return null;

            var value = prop[0];

            if (value == null) return null;

            // Special handling for byte sequence property type to fetch IDs/GUIDs
            if (value.GetType() == typeof(byte[]))
            {
                return ((byte[])value).Aggregate(new StringBuilder(), (acc, b) => acc.AppendFormat("\\{0:X2}", b)).ToString();
            }

            return value.ToString();
        }
        
        public bool Authenticate(string userId, string password)
        {
            var userEntry = SearchByUserId(userId);

            if (userEntry == null) return false;

            try
            {
                userEntry = SearchDirectory(string.Empty, userEntry.DistinguishedName, password);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LdapEntry SearchDirectory(string searchFilter, string username = null, string password = null)
        { 
            using (var entry = new DirectoryEntry(basePath, username ?? connectionSettings.ServiceUserDN, password ?? connectionSettings.ServiceUserPassword, AuthenticationTypes.None))
            {
                var ds = new DirectorySearcher(entry);
                ds.SearchScope = SearchScope.Subtree;
                ds.Filter = searchFilter;
                ds.PropertiesToLoad.AddRange(new[]{ connectionSettings.UserIdAttributeKey, connectionSettings.UserNameAttributeKey });
                var result = ds.FindOne();

                if (result == null) return null;

                var userName = PropertyToString(result, connectionSettings.UserNameAttributeKey);
                var resultEntry = new LdapEntry
                {
                    UserId = PropertyToString(result, connectionSettings.UserIdAttributeKey),
                    UserName = userName,
                    DistinguishedName = GetEntryDistinguishedName(result),
                };

                return resultEntry;
            }
        }

        public string GetUserUniqueName(string userName)
        {
            return string.Format(connectionSettings.UserNameTransformTemplate, userName);
        }
    }
}
