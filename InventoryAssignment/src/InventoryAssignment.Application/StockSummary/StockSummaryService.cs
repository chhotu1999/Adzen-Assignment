
using InventoryAssignment.Product;
using InventoryAssignment.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InventoryAssignment.StockSummary
{
    public class StockSummaryService : ApplicationService, IStockSummaryService
    {
        private readonly IRepository<StockSummaryModel, int> _stockSummaryRepo;  // Use entity, not DTO
        private readonly IRepository<ProductModel, int> _productRepo;

        public StockSummaryService(IRepository<StockSummaryModel, int> stockSummaryRepo, IRepository<ProductModel, int> productRepo)
        {
            _stockSummaryRepo = stockSummaryRepo;
            _productRepo = productRepo;
        }

        public async Task<StockSummaryDto> AddStockSummaryAsync(CreateStockSummaryDto input)
        {
            try
            {
                var product = await _productRepo.GetAsync(input.ProductId);
                if (product == null)
                {
                    throw new UserFriendlyException("Product not found.");
                }

                var stockSummary = new StockSummaryModel  // Use entity, not DTO
                {
                    ProductId = input.ProductId,
                    StockMode = "Inward",
                    StockQuantity = input.StockQuantity,
                    BalanceQuantity = product.StockLevel + input.StockQuantity
                };

                // Update the Product stock level
                product.StockLevel = stockSummary.BalanceQuantity;
                await _productRepo.UpdateAsync(product);

                // Insert into stock summary table
                await _stockSummaryRepo.InsertAsync(stockSummary);

                // Map the entity to the DTO to return
                return ObjectMapper.Map<StockSummaryModel, StockSummaryDto>(stockSummary);

            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public async Task<ResponseResult<StockSummaryDto>> GetStockSummaryPagedAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var skip = (pageNumber - 1) * pageSize;

            var totalRecords = await _stockSummaryRepo.CountAsync();

            var stockSummaries = await _stockSummaryRepo
                .WithDetails(s => s.Product)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var items = stockSummaries.Select(s => new StockSummaryDto
            {
                ProductId = s.ProductId,
                ProductName = s.Product.Name,
                StockMode = s.StockMode,
                StockQuantity = s.StockQuantity,
                BalanceQuantity = s.BalanceQuantity,
                Unit = s.Product.Unit
            }).ToList();

            return new ResponseResult<StockSummaryDto>
            {
                Items = items,
                TotalCount = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}

