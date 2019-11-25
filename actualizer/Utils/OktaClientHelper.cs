using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Okta.Sdk;
using System.Linq;
using Okta.Sdk.Configuration;

namespace actualizer.Utils {
    public class OktaClientHelper {

        const string domain = "https://dev-839928.okta.com";

        const string oktaToken = "00v9TMD0d5oAq7tssPHRFtlmRwQ7wh2V1FLOrU9g2C";

        public static async Task<dynamic> GetProfileDetails(string uid) {
            var client = new OktaClient(new OktaClientConfiguration {
                OktaDomain = "https://dev-839928.okta.com",
                Token = "00v9TMD0d5oAq7tssPHRFtlmRwQ7wh2V1FLOrU9g2C"
            });

            var user = await client.Users.GetUserAsync(userId: uid);

            var role = user.Profile["permissions"];
            return role;
        }
    }

}
