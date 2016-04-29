namespace ActiveCitizen.LDAP.IdentityProvider
{
    public interface ILdapConnectionSettings
    {
        string Server { get; }

        string BasePathDN { get; }

        string ServiceUserDN { get; }

        string ServiceUserPassword { get; }

        string UserIdAttributeKey { get; }

        string UserNameAttributeKey { get; }

        string UserNameTransformTemplate { get; }
    }
}