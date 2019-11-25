using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;

namespace actualizer.Authorization {

    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {


        //IHttpContextAccessor _httpContextAccessor = null;

        //public PermissionRequirementHandler(IHttpContextAccessor httpContextAccessor) {
        //    _httpContextAccessor = httpContextAccessor;
        //}


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement) {

            //HttpContext httpContext = _httpContextAccessor.HttpContext; // Access context here


            if (context.Resource is AuthorizationFilterContext authContext) {
                Console.WriteLine(authContext);

                Console.WriteLine(context);

                var uid = context.User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

                var x = context.User.Claims.FirstOrDefault(s => s.Type == "uid")?.Value;
            }


            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email)) {

                Console.WriteLine("AuthHandler*****************************************AuthHandler");


                Console.WriteLine("AuthHandler*****************************************AuthHandler");


                return Task.CompletedTask;

            }

            var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email);
            var domain = email.Value.Split('@')[1];
            if (domain == requirement.Permission) {
                context.Succeed(requirement);
            }


            return Task.CompletedTask;
        }
    }
}