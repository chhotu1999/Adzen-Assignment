using InventoryAssignment.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.InvSales
{
    public interface ISalesDetailService
    {
        Task<MvSalesDetail> CreateSaleDetailAsync(CreateSalesDetailDto input);
    }
}
