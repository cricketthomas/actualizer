using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using actualizer.Policy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Okta.Sdk;
using Okta.AspNetCore;
using Okta.Sdk.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;

namespace actualizer.Security.claims.transformation {
    public class UserTransformer : IClaimsTransformation {

        IHttpContextAccessor _httpContextAccessor;
        private readonly IOktaClient _oktaClient;
        private readonly IMemoryCache _cache;

        public UserTransformer(IHttpContextAccessor httpContextAccessor, IMemoryCache cache) {//, IOktaClient oktaClient) {
            _httpContextAccessor = httpContextAccessor;
            //this._oktaClient = oktaClient;
            _cache = cache;

        }


        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal p) {

            var cacheKey = p.FindFirstValue(ClaimTypes.NameIdentifier);
            ClaimsPrincipal cached_claims = p;

            if (_cache.TryGetValue(cacheKey, out cached_claims)) {

                ((ClaimsIdentity)p.Identity).AddClaims(cached_claims.Claims);


            } else {

                var client = new OktaClient(new OktaClientConfiguration {
                    OktaDomain = "https://dev-839928.okta.com/",
                    Token = "00jwXF0-ir7_Vn7HbUWb7LEeArC3MpJJEDVkRbVQwn"
                });

                var claimsIdentity = p.Identity as ClaimsIdentity;
                string _CanMakeAnalyticsRequests = "CanMakeAnalyticsRequests";
                string _CanMakeAnalyticsAPIRequests = "CanMakeAnalyticsAPIRequests";

                var uid = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value;
                var user = await client.Users.GetUserAsync(uid);

                var permissions = user.Profile["permissions"];

                if (permissions.ToString() == _CanMakeAnalyticsRequests || permissions.ToString() == _CanMakeAnalyticsAPIRequests) {
                    claimsIdentity.AddClaim(new Claim(Claims.CanMakeAnalyticsRequests, string.Empty));
                }
                //todo make configuarble.
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(200));

                _cache.Set(cacheKey, p, cacheEntryOptions);

            }


            return p;

        }


    }
}
