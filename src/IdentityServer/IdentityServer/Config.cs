using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                 ClientId = "tutorialClient",
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 ClientSecrets =
                 {
                   new Secret("secret".Sha256())
                 },
                 AllowedScopes = { "tutorialAPI" }
            },
            new Client
            {
                 ClientId = "cartClient",
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 ClientSecrets =
                 {
                   new Secret("secret".Sha256())
                 },
                 AllowedScopes = { "cartAPI" }
            },
            new Client
                   {
                       ClientId = "shopping_web",
                       ClientName = "Shopping Web",
                       AllowedGrantTypes = GrantTypes.Code,
                       RequirePkce = true,
                       AllowRememberConsent = false,
                       RequireClientSecret = false,
                       AllowedCorsOrigins = new List<string>
                       {
                           "https://localhost:4200"
                       },
                       RedirectUris = new List<string>()
                       {
                         "https://localhost:4200",
                         "https://localhost:4200/silent-renew.html"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:4200"
                       },
                       //ClientSecrets = new List<Secret>
                       //{
                       //    new Secret("secret".Sha256())
                       //},
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           //IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           "cartAPI"
                           //"roles"
                       },
                       AllowAccessTokensViaBrowser = true,
                       RequireConsent = false
                   }
                   //     new Client
                   //{
                   //    ClientId = "shopping_web",
                   //    ClientName = "Shopping Web",
                   //    AllowedGrantTypes = GrantTypes.Hybrid,
                   //   // AllowedGrantTypes = GrantTypes.Hybrid,
                   //    RequirePkce = false,
                   //    AllowRememberConsent = false,
                   //    ClientSecrets = new List<Secret>
                   //    {
                   //        new Secret("secret".Sha256())
                   //    },
                   //    AllowedCorsOrigins = new List<string>
                   //    {
                   //        "https://localhost:4200"
                   //    },
                   //    RedirectUris = new List<string>()
                   //    {
                   //      "https://localhost:4200",
                   //      "https://localhost:4200/silent-renew.html"
                   //    },
                   //    PostLogoutRedirectUris = new List<string>()
                   //    {
                   //        "https://localhost:4200"
                   //    },
                   //    //ClientSecrets = new List<Secret>
                   //    //{
                   //    //    new Secret("secret".Sha256())
                   //    //},
                   //    AllowedScopes = new List<string>
                   //    {
                   //        IdentityServerConstants.StandardScopes.OpenId,
                   //        IdentityServerConstants.StandardScopes.Profile,
                   //        //IdentityServerConstants.StandardScopes.Address,
                   //        //IdentityServerConstants.StandardScopes.Email,
                   //        "cartAPI"
                   //        //"roles"
                   //    }
                   //}

        };
        public static IEnumerable<ApiScope> ApiScopes =>
         new ApiScope[]
         {
             new ApiScope("tutorialAPI", "Tutorial API"),
             new ApiScope("cartAPI", "Cart API"),
         };
        public static IEnumerable<ApiResource> ApiResources =>
         new ApiResource[]
         {
         };
        public static IEnumerable<IdentityResource> IdentityResources =>
         new IdentityResource[]
         {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
         };
        public static List<TestUser> TestUsers =>
            
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "mehmet",
                    Password = "swn",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "mehmet"),
                        new Claim(JwtClaimTypes.FamilyName, "ozkaya")
                    }
                }
            };
    }
}
