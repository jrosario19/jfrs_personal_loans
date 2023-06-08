using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data
{
    public class JFRSPersonalLoansDBContext: IdentityDbContext<ApplicationUser>
    {
        private string tenantId;
        public JFRSPersonalLoansDBContext(DbContextOptions<JFRSPersonalLoansDBContext> options, ITenantService tenantService)
            : base(options)
        {
            tenantId = tenantService.GetTenant();
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries().Where(e=>e.State==EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted && e.Entity is ITenantEntity))
            {
                if (string.IsNullOrEmpty(tenantId))
                {
                    throw new Exception("TenantId not found at the momment of creating the record");

                }
                var entity = item.Entity as ITenantEntity;
                entity.TenantId = tenantId;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var type = entity.ClrType;
                if (typeof(ITenantEntity).IsAssignableFrom(type))
                {
                    var method = typeof(JFRSPersonalLoansDBContext).GetMethod(nameof(SetTenantGlobalFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).MakeGenericMethod(type);
                    var filter = method.Invoke(null, new object[] { this });
                    entity.QueryFilter=(LambdaExpression)filter;
                    entity.AddIndex(entity.FindProperty(nameof(ITenantEntity.TenantId)));
                }
                else if (type.ShouldSkipTenantValidation())
                {
                    continue;
                }
                else
                {
                    throw new Exception($"The entity {entity} has not been marked as Tenant nor Commun");
                }
            }
        }
        
        private static LambdaExpression SetTenantGlobalFilter<TEntity>(JFRSPersonalLoansDBContext context) where TEntity : class, ITenantEntity
        {
            Expression<Func<TEntity, bool>> filter = x => x.TenantId == context.tenantId;
            return filter; 
        }

        public DbSet<CompanyConfiguration> CompanyConfigurations { get; set; }
        public DbSet<LoanConfiguration> LoanConfigurations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Installment> Installments { get; set; }
    }
}
