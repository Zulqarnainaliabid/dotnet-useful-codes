using EfCoreDbIterceptors.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace EfCoreDbIterceptors.Infrastructure
{
    public class TenantManagerMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantManagerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantInfo = context.RequestServices.GetRequiredService<TenantDetail>();
            var tenantName = context.Request.Headers["Tenant"];

            if (string.IsNullOrEmpty(tenantName))
                tenantName = "tenant1";

            tenantInfo.Name = tenantName;

            await _next(context);
        }
    }
}
