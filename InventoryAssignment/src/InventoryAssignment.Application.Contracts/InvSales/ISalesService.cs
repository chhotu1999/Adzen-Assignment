using InventoryAssignment.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.InvSales
{
    public interface ISalesService
    {
        Task<ResponseResult<SalesDto>> GetSalesSummaryPagedAsync(int pageNumber, int pageSize);

        Task<SalesDto> CreateSaleAsync(CreateSaleDto input);
    }
}
