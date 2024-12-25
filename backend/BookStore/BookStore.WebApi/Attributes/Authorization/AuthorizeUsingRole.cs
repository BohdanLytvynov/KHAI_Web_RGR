using BookStore.DAL.Entities;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.WebApi.Attributes.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeUsingRole : Attribute, IAuthorizationFilter
    {
        public string[] RoleNames { get; set; }

        public AuthorizeUsingRole(params string[] roleNames)
        {
            RoleNames = roleNames;
        }

        private bool IsInRoles(string current, params string[] roles)
        {
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i].Equals(current))
                    return true;
            }

            return false;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var http = context.HttpContext;

            if ((http.Items["User"] as User) is not null)
            {
                var role = http.Items["Role"]!.ToString();

                if (!IsInRoles(role, RoleNames))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized", status = StatusCodes.Status401Unauthorized });
                }
            }
            else
            { 
                context.Result = new JsonResult(new { message = "Unauthorized", status = StatusCodes.Status401Unauthorized });
            }

        }
    }
}
