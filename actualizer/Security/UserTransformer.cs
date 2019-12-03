using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using actualizer.Policy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Okta.Sdk;

namespace actualizer.Security.claims.transformation {
    public class UserTransformer : IClaimsTransformation {

        private readonly IOktaClient _oktaClient;
        IHttpContextAccessor _httpContextAccessor;

        public UserTransformer(IHttpContextAccessor httpContextAccessor, IOktaClient oktaClient) {
            _httpContextAccessor = httpContextAccessor;
            this._oktaClient = oktaClient;

        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal p) {
            var claimsIdentity = p.Identity as ClaimsIdentity;
            string _CanMakeAnalyticsRequests = "CanMakeAnalyticsRequests";

            var uid = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value;
            var user = await _oktaClient.Users.GetUserAsync(uid);

            var permissions = user.Profile["permissions"];

            if (permissions.ToString() == _CanMakeAnalyticsRequests) {
                claimsIdentity.AddClaim(new Claim(Claims.CanMakeAnalyticsRequests, string.Empty));
            }
            return p;

        }


    }
}
