namespace ActiveCitizen.LDAP.IdentityProvider
{
    public interface ILdapConnector
    {
        bool Authenticate(string userId, string password);
        string GetUserUniqueName(string userName);
        LdapEntry SearchByUserId(string userName);
        LdapEntry SearchByUserName(string userName);
        LdapEntry SearchDirectory(string searchFilter, string username = null, string password = null);
    }
}