using Application.CrossCuttingConcerns.Exceptions.Types;
using Application.Utils.JWT;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filters.Authorization
{
    public class AuthorizationFilter : IAsyncActionFilter
    {
        private readonly ITokenHelper _tokenHelper;

        public AuthorizationFilter(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            bool hasIgnoreAttribute = descriptor.MethodInfo.CustomAttributes
                .Where(ca => ca.AttributeType == typeof(IgnoreAuthAttribute)).Any();

            if (!hasIgnoreAttribute)
            {
                string token = _tokenHelper.GetCurrentToken();

                if (!_tokenHelper.ValidateToken(token))
                    throw new AuthorizationException("Your token is not valid");


                List<string> claims = _tokenHelper.GetPermissionClaims().ToList();


                CustomAttributeData claimAttribute = descriptor.MethodInfo.CustomAttributes
                    .Where(ca => ca.AttributeType == typeof(ClaimAttribute))
                    .FirstOrDefault();

                if (claimAttribute is not null)
                {
                    string claimNameFromAttribute = claimAttribute.ConstructorArguments[0].Value as string;
                    if (!claims.Contains(claimNameFromAttribute))
                        throw new AuthorizationException("You don't have any permission for this action.");
                }
            }           

            await next();
        }
    }
}
