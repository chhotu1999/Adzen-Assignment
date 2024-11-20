using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using InventoryAssignment.Customer;
using InventoryAssignment.InvSales;
using InventoryAssignment.Product;
using InventoryAssignment.Sales;
using InventoryAssignment.StockSummary;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryAssignment;

[DependsOn(
    typeof(InventoryAssignmentDomainModule),
    typeof(InventoryAssignmentApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class InventoryAssignmentApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<InventoryAssignmentApplicationModule>();
        });
        context.Services.AddTransient<ICustomerService, CustomerService>();
        context.Services.AddTransient<IProductService, ProductService>();
        context.Services.AddTransient<IStockSummaryService, StockSummaryService>();
        context.Services.AddTransient<ISalesService, SalesService>();
    }
}
