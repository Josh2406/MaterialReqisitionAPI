using MaterialRequisition.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaterialRequisition.Persistence.Context
{
    public class RequisitionContext: DbContext
    {
        public RequisitionContext(DbContextOptions<RequisitionContext> dbContextOptions): base(dbContextOptions) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ActivityTimeline> ActivityTimelines { get; set; }
        public virtual DbSet<BusinessUnit> BusinessUnits { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<ItemStock> ItemStocks { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Requisition> Requisitions { get; set; }
        public virtual DbSet<RequisitionApproval> RequisitionApprovals { get; set; }
        public virtual DbSet<RequisitionItem> RequisitionItems { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StockPosting> StockPostings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
           .SelectMany(t => t.GetProperties())
           .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            modelBuilder.Entity<Account>().HasIndex(x => x.StaffId).IsUnique();
            modelBuilder.Entity<Account>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Account>().HasIndex(x => x.Phone).IsUnique();

            modelBuilder.Entity<BusinessUnit>().HasIndex(x => x.UnitName).IsUnique();
            modelBuilder.Entity<BusinessUnit>().HasIndex(x => x.UnitCode).IsUnique();

            modelBuilder.Entity<InventoryCategory>().HasIndex(x => x.CategoryName).IsUnique();

            modelBuilder.Entity<InventoryItem>().HasIndex(x => x.ItemName).IsUnique();

            modelBuilder.Entity<Permission>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Role>().HasIndex(x => x.RoleName).IsUnique();

            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
        }
    }
}
