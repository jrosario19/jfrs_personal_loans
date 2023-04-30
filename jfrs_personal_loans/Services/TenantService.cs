using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services
{
    public class TenantService:ITenantService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetTenant()
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext is null)
            {
                return string.Empty;
            }

            var authTicket = DecryptAuthCookie(httpContext);

            if (authTicket is null)
            {
                return string.Empty;
            }

            var claimTenant = authTicket.Principal.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTenantId);

            if (claimTenant is null)
            {
                return string.Empty;
            }

            return claimTenant.Value;
            
        }

        private static AuthenticationTicket DecryptAuthCookie(HttpContext httpContext)
        {
            var opt = httpContext.RequestServices
                .GetRequiredService<Microsoft.Extensions.Options.IOptionsMonitor<CookieAuthenticationOptions>>().Get("Identity.Aplication");

            var cookie = opt.CookieManager.GetRequestCookie(httpContext, opt.Cookie.Name);

            return opt.TicketDataFormat.Unprotect(cookie);
        }
    }
}
