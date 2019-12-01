using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace actualizer.Security.Policy.Policies {


    public class CanMakeAnalyticsRequests : AuthorizationHandler<CanMakeAnalyticsRequests>, IAuthorizationRequirement {

        public CanMakeAnalyticsRequests() {

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanMakeAnalyticsRequests requirement) {

            if (context.User.Claims.Any(c => c.Type == Claims.CanMakeAnalyticsRequests)) {
                context.Succeed(requirement);
            } else {
                Console.WriteLine($"{context.User.Claims} tried to call analytics api");
                context.Fail();
            }

            return Task.CompletedTask;

        }
    }
}
