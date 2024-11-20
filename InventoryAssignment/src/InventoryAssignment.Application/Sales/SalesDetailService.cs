using AutoMapper;
using AutoMapper.Internal.Mappers;
using InventoryAssignment.InvSales;
using InventoryAssignment.StockSummary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace InventoryAssignment.Sales
{
    public class SalesDetailService : ISalesDetailService
    {
        private readonly IRepository<MvSalesDetail, int> _salesDetailRepository;
        private readonly IMapper _mapper;
        public SalesDetailService(IRepository<MvSalesDetail, int> salesDetailRepository, IMapper mapper)
        {
            _salesDetailRepository = salesDetailRepository;
            _mapper = mapper;
        }

        public async Task<MvSalesDetail> CreateSaleDetailAsync(CreateSalesDetailDto input)
        {
            try
            {

                var salesdetails = new MvSalesDetail  // Use entity, not DTO
                {
                    SalesId = input.SalesId,
                    ProductId = input.ProductId,
                    Quantity = input.Quantity,
                    UnitPrice = input.UnitPrice,
                    TotalPrice = input.TotalPrice
                };

                // Insert into stock summary table
                await _salesDetailRepository.InsertAsync(salesdetails);
                return salesdetails;


            }
            catch (Exception ex)
            {
                return null;
            }

            
        }
    }
}
