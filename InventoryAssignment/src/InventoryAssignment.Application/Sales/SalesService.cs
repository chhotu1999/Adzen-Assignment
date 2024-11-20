
using AutoMapper;
using InventoryAssignment.InvSales;
using InventoryAssignment.Product;
using InventoryAssignment.SharedModel;
using InventoryAssignment.StockSummary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace InventoryAssignment.Sales
{
    public class SalesService : ApplicationService, ISalesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ProductModel, int> _productRepository;
        private readonly IRepository<MvSales, int> _salesRepository;
        private readonly IRepository<StockSummaryModel, int> _stockTransSummaryRepository;
        private readonly IRepository<MvSalesDetail, int> _salesDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager; // This is automatically injected


        public SalesService(IMapper mapper,
                            IRepository<MvSalesDetail, int> salesDetailRepository,
                            IRepository<ProductModel, int> productRepository,
                            IRepository<MvSales, int> salesRepository,
                            IRepository<StockSummaryModel, int> stockTransSummaryRepository,
                            IUnitOfWorkManager unitOfWorkManager)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _salesRepository = salesRepository;
            _salesDetailRepository = salesDetailRepository;
            _stockTransSummaryRepository = stockTransSummaryRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<SalesDto> CreateSaleAsync(CreateSaleDto input)
        {
            try
            {
                await StockValidationAsync(input.SalesDetails);

                decimal totalAmount = input.SalesDetails.Sum(d => d.UnitPrice * d.Quantity);
                decimal netAmount = totalAmount - input.Discount;

                // Create the Sales entity with details
                var sales = new MvSales
                {
                    CustomerId = input.CustomerId,
                    InvoiceDate = DateTime.Now,
                    Remarks = input.Remarks,
                    NetAmount = netAmount,
                    Discount = input.Discount,
                    TotalAmount = totalAmount
                };

                // Start a unit of work transaction
                using (var unitOfWork = _unitOfWorkManager.Begin())
                {
                    // Insert sales with details
                    await _salesRepository.InsertAsync(sales);
                    await unitOfWork.SaveChangesAsync(); // Commit the sale

                    foreach (var item in input.SalesDetails)
                    {
                        var salesdetail = new MvSalesDetail
                        {
                            SalesId = sales.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            TotalPrice = item.TotalPrice
                        };

                        await _salesDetailRepository.InsertAsync(salesdetail);

                        var product = await _productRepository.GetAsync(item.ProductId);
                        product.StockLevel -= item.Quantity;
                        await _productRepository.UpdateAsync(product);

                        var stockTrans = new StockSummaryModel
                        {
                            ProductId = item.ProductId,
                            StockMode = "Outward",
                            StockQuantity = item.Quantity,
                            BalanceQuantity = product.StockLevel
                        };
                        await _stockTransSummaryRepository.InsertAsync(stockTrans);
                    }

                    // Commit all changes in one transaction
                    await unitOfWork.SaveChangesAsync();

                    var salesDto = _mapper.Map<SalesDto>(sales);
                    return salesDto;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return null;
            }
        }

        private async Task StockValidationAsync(List<SalesDetailDto> salesDetails)
        {
            foreach (var detail in salesDetails)
            {
                var product = await _productRepository.GetAsync(detail.ProductId);
                if (product.StockLevel < detail.Quantity)
                {
                    throw new Exception($"Insufficient stock for product {product.Name}.");
                }
            }
        }



        public async Task<ResponseResult<SalesDto>> GetSalesSummaryPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Validate pagination parameters
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                // Calculate the skip value (where to start fetching records)
                var skip = (pageNumber - 1) * pageSize;

                // Get the total number of records in the sales repository
                var totalRecords = await _salesRepository.CountAsync();

                // Retrieve paginated sales data with related details
                var salesList = await _salesRepository
                    .WithDetails(s => s.Customer, s => s.SalesDetails)
                    .OrderBy(s => s.Id)  // Ensure ordering to prevent the exception
                    .Skip(skip)  // Skip the number of records based on the current page
                    .Take(pageSize)  // Take only the records for the current page
                    .ToListAsync();

                // Map the sales list to SalesDto
                var items = salesList.Select(s => new SalesDto
                {
                    SalesId = s.Id,
                    CustomerId = s.CustomerId,
                    InvoiceDate = s.InvoiceDate,
                    CustomerName = s.Customer.Name,
                    NetAmount = s.NetAmount,
                    Discount = s.Discount,
                    TotalAmount = s.TotalAmount,
                    Details = s.SalesDetails.Select(d => new SalesDetailDto
                    {
                        ProductName = d.Product.Name,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        TotalPrice = d.TotalPrice
                    }).ToList()
                }).ToList();

                // Return paginated result wrapped in ResponseResult
                return new ResponseResult<SalesDto>
                {
                    Items = items,
                    TotalCount = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return null;
            }
        }


    }
}
