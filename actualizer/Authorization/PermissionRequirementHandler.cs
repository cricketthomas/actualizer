using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using actualizer.Utils;
using Microsoft.AspNetCore.Authentication;

namespace actualizer.Authorization {

    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement) {
            if (context.Resource is AuthorizationFilterContext authContext) {
                var uid = context.User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
                if (string.IsNullOrWhiteSpace(uid)) {
                    return Task.CompletedTask;
                } else {
                    var x = await OktaClientHelper.GetProfileDetails(uid);

                    context.Succeed(requirement);
                }

            }
            return Task.CompletedTask;
        }


        //protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement) {

        //    if (context.Resource is AuthorizationFilterContext authContext) {
        //        if (string.IsNullOrWhiteSpace(context.User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value)) {
        //            return Task.CompletedTask;
        //        }

        //        var uid = context.User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        //        var AdminRights = await OktaClientHelper.GetProfileDetails(uid);

        //        if (AdminRights == requirement) {
        //            context.Succeed(requirement);
        //        }

        //    }
        //    return Task.CompletedTask;
        //}


    }
}