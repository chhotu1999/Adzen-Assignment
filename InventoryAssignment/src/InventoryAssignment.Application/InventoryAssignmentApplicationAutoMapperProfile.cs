using AutoMapper;
using InventoryAssignment.Customer;
using InventoryAssignment.InvSales;
using InventoryAssignment.Product;
using InventoryAssignment.Sales;
using InventoryAssignment.StockSummary;

namespace InventoryAssignment;

public class InventoryAssignmentApplicationAutoMapperProfile : Profile
{
    public InventoryAssignmentApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<CustomerModel, CustomerDto>();
        CreateMap<CreateUpdateCustomerDto, CustomerModel>();
        CreateMap<ProductModel, ProductDto>();
        CreateMap<CreateUpdateProductDto, ProductModel>();
        CreateMap<StockSummaryModel, StockSummaryDto>();
        CreateMap<CreateStockSummaryDto, StockSummaryModel>();
        CreateMap<MvSales, SalesDto>();
        CreateMap<MvSalesDetail, MvSalesDetailsDto>();
        CreateMap<CreateSalesDetailDto, MvSalesDetail>();
    }
}
