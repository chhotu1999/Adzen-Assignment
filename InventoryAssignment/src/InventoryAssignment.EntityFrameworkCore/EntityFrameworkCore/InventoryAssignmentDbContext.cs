using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using InventoryAssignment.Customer;
using InventoryAssignment.Product;
using InventoryAssignment.Sales;
using InventoryAssignment.StockSummary;

namespace InventoryAssignment.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class InventoryAssignmentDbContext :
    AbpDbContext<InventoryAssignmentDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    public DbSet<StockSummaryModel> StockSummaries { get; set; }

    public DbSet<MvSales> Sales { get; set; }
    public DbSet<MvSalesDetail> SalesDetails { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext 
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext .
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public InventoryAssignmentDbContext(DbContextOptions<InventoryAssignmentDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(InventoryAssignmentConsts.DbTablePrefix + "YourEntities", InventoryAssignmentConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<CustomerModel>(b =>
        {
            b.ToTable("Customer"); // Specify table name if desired
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired().HasMaxLength(100);
            b.Property(c => c.Gender).HasConversion<string>().IsRequired().HasMaxLength(100);
            b.Property(c => c.Email).HasMaxLength(100);
            b.Property(c => c.Contact).HasMaxLength(20);
            b.Property(c => c.Address).HasMaxLength(200);
        });

        builder.Entity<ProductModel>(b =>
        {
            b.ToTable("Product"); // Specify table name if desired
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired().HasMaxLength(100);
            b.Property(c => c.Code).HasMaxLength(50);
            b.Property(c => c.UnitPrice).HasPrecision(18, 2);
            b.Property(c => c.Category).HasConversion<string>().IsRequired().HasMaxLength(200);
            b.Property(c => c.Unit).HasConversion<string>().IsRequired().HasMaxLength(200);
            b.Property(c => c.Description).HasMaxLength(200);
            b.Property(c => c.StockLevel).HasPrecision(18, 4);
        });

        builder.Entity<StockSummaryModel>(b =>
        {
            b.ToTable("StockSummary"); // Specify table name if desired
            b.HasKey(c => c.Id);
            b.Property(c => c.ProductId).IsRequired();
            b.Property(c => c.StockMode).HasConversion<string>().IsRequired().HasMaxLength(50);
            b.Property(c => c.StockQuantity).HasPrecision(18, 4);
            b.Property(c => c.BalanceQuantity).HasPrecision(18, 4);
        });

        builder.Entity<StockSummaryModel>(b =>
        {
            b.ToTable("StockSummary");
            b.HasKey(c => c.Id);
            b.Property(c => c.ProductId).IsRequired();
            b.Property(c => c.StockMode).HasConversion<string>().IsRequired().HasMaxLength(50);
            b.Property(c => c.StockQuantity).HasPrecision(18, 4);
            b.Property(c => c.BalanceQuantity).HasPrecision(18, 4);

            // Define the relationship with Product
            b.HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .IsRequired();
        });

        builder.Entity<MvSales>(b =>
        {
            b.ToTable("InvSales");
            b.HasKey(c => c.Id);
            b.Property(c => c.InvoiceDate);
            b.Property(c => c.CustomerId).IsRequired();
            b.Property(c => c.Remarks).HasMaxLength(100);
            b.Property(c => c.NetAmount).HasPrecision(18, 4);
            b.Property(c => c.Discount).HasPrecision(18, 4);
            b.Property(c => c.TotalAmount).HasPrecision(18, 4);

            // Define the relationship with Product
            b.HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .IsRequired();
        });

        builder.Entity<MvSalesDetail>(b =>
        {
            b.ToTable("InvSalesDetail");
            b.HasKey(c => c.Id);
            b.Property(c => c.SalesId).IsRequired();
            b.Property(c => c.ProductId).IsRequired();
            b.Property(c => c.Quantity).HasPrecision(18, 4);
            b.Property(c => c.UnitPrice).HasPrecision(18, 4);
            b.Property(c => c.TotalPrice).HasPrecision(18, 4);

            // Define the relationship with sales and Product
            b.HasOne(s => s.Sales)
                .WithMany()
                .HasForeignKey(s => s.SalesId)
                .IsRequired();

            b.HasOne(s => s.Product)
               .WithMany()
               .HasForeignKey(s => s.ProductId)
               .IsRequired();
        });
    }
}
