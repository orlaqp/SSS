using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace SSS.Api.IdentityServer
{
    public static class ClientConfig
    {
        /// <summary>
        /// 设置Identity资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var list = new IdentityResource[] { new IdentityResources.OpenId() };
            return list;
        }

        /// <summary>
        /// 设置Api资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            var list = new List<ApiResource> { new ApiResource("api_test1", "sss api") };
            return list;
        }

        /// <summary>
        /// 设置Client资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            var list = new List<Client>
            {
                //客户端授权模式
                new Client
                {
                    ClientId = "client1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api_test1" }
                },

                //资源所有者密码授权模式
                new Client
                {
                    ClientId = "client2",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api_test1" }
                }
            };
            return list;
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            var list = new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId="1",
                    Username = "wbs",
                    Password = "123"
                },
                new TestUser()
                {
                    SubjectId="2",
                    Username = "wqy",
                    Password = "456"
                }
            };

            return list;
        }
    }
}
