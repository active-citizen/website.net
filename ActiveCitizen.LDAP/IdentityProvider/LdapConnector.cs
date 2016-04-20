using System;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    public class LdapConnector
    {
        private readonly LdapConnectionSettings connectionSettings;
        private readonly string basePath = string.Empty;

        public LdapConnector(LdapConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings;
            basePath = string.Format("LDAP://{0}/{1}", connectionSettings.Server, connectionSettings.BaseDN);
        }

        /*
        string ldapfilter = "(&(objectclass=person)({0}={1}))";

            try
            {
                string DN = "";
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + connectionSettings.Server + "/" + connectionSettings.BaseDN, connectionSettings.ServiceUserDN, connectionSettings.ServiceUserPassword, AuthenticationTypes.None))
                {
                    DirectorySearcher ds = new DirectorySearcher(entry);
    ds.SearchScope = SearchScope.Subtree;
                    ds.Filter = string.Format(ldapfilter, connectionSettings.UserIdAttributeName == "DN" ? "entryDN" : connectionSettings.UserIdAttributeName, userId);
    SearchResult result = ds.FindOne();
                    if (result != null)
                    {
                        DN = result.Path.Replace("LDAP://" + connectionSettings.Server + "/", "");

                        user = new ApplicationUser
                        {
                            Id =
                            connectionSettings.UserIdAttributeName == "DN"
                            ? DN
                            : (string)result.Properties[connectionSettings.UserIdAttributeName][0],
                            UserName = (string)result.Properties[connectionSettings.UserNameAttributeName][0]
};
                        this.CreateAsync(user).Wait();
                        return base.FindByIdAsync(userId);
}
                }
            }
            catch (Exception ex) { Task.FromException(ex); }

            return Task.FromResult<ApplicationUser>(null);

                         user = new ApplicationUser
                        {
                            Id =
                            connectionSettings.UserIdAttributeName == "DN"
                            ? DN
                            : (string)result.Properties[connectionSettings.UserIdAttributeName][0],
                            UserName = (string)result.Properties[connectionSettings.UserNameAttributeName][0]
                        };

                        base.CreateAsync(user).Wait();
                        return base.FindByNameAsync(userName);

             */
        public LdapEntry SearchByUserName(string userName)
        {
            string filterTemplate = "(&(objectclass=person)({0}={1}))";
            string searchFilter = string.Format(filterTemplate, connectionSettings.UserNameAttributeName, userName);
            return SearchDirectory(searchFilter);
        }

        public LdapEntry SearchByUserId(string userName)
        {
            string filterTemplate = "(&(objectclass=person)({0}={1}))";
            string searchFilter = string.Format(filterTemplate, connectionSettings.UserIdAttributeName, userName);
            return SearchDirectory(searchFilter);
        }

        private string GetEntryDistinguishedName(SearchResult entry)
        {
            return entry.Path.Replace("LDAP://" + connectionSettings.Server + "/", "");
        }
        
        private string PropertyToString(SearchResult entry, string propertyName)
        {
            var prop = entry.Properties[propertyName];

            if (prop.Count == 0) return null;

            var value = prop[0];

            if (value == null) return null;

            if (value.GetType() == typeof(byte[]))
            {
                return ((byte[])value).Aggregate(new StringBuilder(), (acc, b) => acc.Append(char.ConvertFromUtf32(b))).ToString();
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
                ds.PropertiesToLoad.AddRange(new[]{ connectionSettings.UserIdAttributeName, connectionSettings.UserNameAttributeName });
                var result = ds.FindOne();

                if (result == null) return null;

                var resultEntry = new LdapEntry
                {
                    UserId = PropertyToString(result, connectionSettings.UserIdAttributeName),
                    UserName = PropertyToString(result, connectionSettings.UserNameAttributeName),
                    DistinguishedName = GetEntryDistinguishedName(result)
                };

                return resultEntry;
            }
        }
    }
}
