using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using DAL.Data;

namespace DAL
{
    public class DashboardDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardDbContext(DbContextOptions<DashboardDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Tenant> Tenants { get; set; }
        // Add your other DbSets here, e.g.:
        public DbSet<License> Licenses { get; set; }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tenantId = _httpContextAccessor.HttpContext?.Items["TenantId"] as string;
            modelBuilder.Entity<Document>().HasQueryFilter(d => d.TenantId == tenantId);
            modelBuilder.Entity<License>().HasQueryFilter(l => l.TenantId == tenantId);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetTenantId();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTenantId();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetTenantId()
        {
            var tenantId = _httpContextAccessor.HttpContext?.Items["TenantId"] as string;
            foreach (var entry in ChangeTracker.Entries<Document>())
            {
                if (entry.State == EntityState.Added && string.IsNullOrEmpty(entry.Entity.TenantId))
                {
                    entry.Entity.TenantId = tenantId;
                }
            }
        }
    }
}
