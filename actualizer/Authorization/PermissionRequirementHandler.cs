using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Okta.Sdk;
using System;
namespace actualizer.Authorization {

    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {


        private IHttpContextAccessor _contextAccessor;

        public PermissionRequirementHandler(IHttpContextAccessor contextAccessor) {
            _contextAccessor = contextAccessor;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement) {
            //if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email)) {
            //    return Task.CompletedTask;
            //}


            Console.WriteLine(_contextAccessor);

            //var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email);
            //var domain = email.Value.Split('@')[1];
            //if (domain == requirement.Permission) {
            //    context.Succeed(requirement);
            //}
            if (true) {
                context.Succeed(requirement);
            }


            return Task.CompletedTask;
        }
    }
}