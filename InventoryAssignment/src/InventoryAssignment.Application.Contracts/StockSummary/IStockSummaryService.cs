using InventoryAssignment.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.StockSummary
{
    public interface IStockSummaryService
    {
        Task<StockSummaryDto> AddStockSummaryAsync(CreateStockSummaryDto css);
        Task<ResponseResult<StockSummaryDto>> GetStockSummaryPagedAsync(int pageNumber, int pageSize);

    }
}
