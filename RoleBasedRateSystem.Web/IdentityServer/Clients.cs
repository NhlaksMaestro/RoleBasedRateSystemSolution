﻿using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace RoleBasedRateSystem.Web.IdentityServer
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44300/"
                    }
                },
                //new Client 
                //{
                //    ClientName = "Role Based Rate System",
                //    ClientId = "mvc",
                //    Flow = Flows.Implicit,

                //    RedirectUris = new List<string>
                //    {
                //        "https://localhost:44300/"
                //    },
                //    PostLogoutRedirectUris = new List<string>
                //    {
                //        "https://localhost:44300/"
                //    },
                //    AllowedScopes = new List<string>
                //    {
                //        "openid",
                //        "profile",
                //        "roles"
                //    }
                //},
                //new Client
                //{
                //    ClientName = "MVC Client (service communication)",   
                //    ClientId = "mvc_service",
                //    Flow = Flows.ClientCredentials,

                //    ClientSecrets = new List<Secret>
                //    {
                //        new Secret("secret".Sha256())
                //    },
                //    AllowedScopes = new List<string>
                //    {
                //        "sampleApi"
                //    }
                //}
            };
        }
    }
}