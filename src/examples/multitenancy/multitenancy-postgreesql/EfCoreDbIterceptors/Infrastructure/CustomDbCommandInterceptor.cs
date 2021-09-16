using EfCoreDbIterceptors.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace EfCoreDbIterceptors.Infrastructure
{
    public class CustomDbCommandInterceptor: DbCommandInterceptor
    {
        private readonly TenantDetail tenant;
        public CustomDbCommandInterceptor(TenantDetail tenant)
        {
            this.tenant = tenant;
        }
        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            if (command.CommandText.Contains("WHERE"))
            {
                command.CommandText += $" AND \"Tenant\" = '{tenant.Name}'";
            }
            else
            {
                command.CommandText += $" WHERE \"Tenant\" = '{tenant.Name}'";
            }

            return base.ReaderExecutingAsync(command, eventData, result);
        }
    }
}
