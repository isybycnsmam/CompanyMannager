using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyMannager.Filters
{
    /// <summary>
    /// Filter for rejecting not authorized requests
    /// </summary>
    public class BasicAuthFilter : ActionFilterAttribute
    {
        private readonly string Username = "admin";
        private readonly string Password = "admin";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader) == true)
                {
                    var propperToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}.{Password}"));
                    if (authorizationHeader == $"Basic {propperToken}")
                    {
                        return;
                    }
                }
            }
            catch { }

            context.Result = new UnauthorizedResult();
        }
    }
}