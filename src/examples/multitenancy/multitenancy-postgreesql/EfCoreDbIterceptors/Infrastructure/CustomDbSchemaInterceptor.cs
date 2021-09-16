using EfCoreDbIterceptors.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EfCoreDbIterceptors.Infrastructure
{
    public class CustomDbSchemaInterceptor : DbCommandInterceptor
    {
        private readonly TenantDetail tenant;
        public CustomDbSchemaInterceptor(TenantDetail tenant)
        {
            this.tenant = tenant;
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            
            command.CommandText= Regex.Replace(command.CommandText, @"FROM ", $"FROM {tenant.Name}.");

            return base.ReaderExecutingAsync(command, eventData, result);
        }
    }
}
