using IdentityServer4.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("Event.Api", "Event.Api"),
            new ApiScope("Bet.Api", "Bet.Api"),
            new ApiScope("Outcome.Api", "Outcome.Api")
        };

    //public static IEnumerable<IdentityResource> IdentityResources =>
    //        new List<IdentityResource>
    //        {
    //            new IdentityResources.OpenId(),
    //            new IdentityResources.Profile()
    //        };

    //public static IEnumerable<ApiResource> ApiResources =>
    //        new List<ApiResource>
    //        {

    //        };

    public static IEnumerable<Client> Clients =>
    new List<Client>
    {
        new Client
        {
            ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // scopes that client has access to
            AllowedScopes = { "Event.Api" }
        }
    };
}
