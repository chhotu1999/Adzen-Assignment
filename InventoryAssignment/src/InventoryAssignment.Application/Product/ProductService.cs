using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InventoryAssignment.Product
{
    public class ProductService : CrudAppService<
        ProductModel,           // The entity
        ProductDto,        // The DTO used to display
        int,               // Primary key type
        PagedAndSortedResultRequestDto, // Used for paging/sorting
        CreateUpdateProductDto>, // Used for update if different than create
        IProductService
    {
        public ProductService(IRepository<ProductModel, int> repository)
            : base(repository)
        {

        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            // Retrieve all products from the repository without pagination
            var products = await Repository.GetListAsync();

            var productDtos = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Unit = product.Unit,
                Price = product.UnitPrice,
                StockLevel = product.StockLevel,

            }).ToList();

            return productDtos;
        }
    }
}
