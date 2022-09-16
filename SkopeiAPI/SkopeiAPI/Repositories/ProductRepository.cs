using Microsoft.Extensions.Logging;
using SkopeiAPI.DbService;
using SkopeiAPI.Models;
using SkopeiAPI.Models.Dto;
using SkopeiAPI.Repositories.Generic;
using System;
using System.Threading.Tasks;

namespace SkopeiAPI.Repositories
{
    // This class implements Product specific methods and derives from the generic baseclass repository.
    public class ProductRepository : GenericRepository<Product>
    {
        // The constructer sends the arguments to the baseclass where they can be accessed accordingly
        public ProductRepository(SkopeiDbContext skopeiDbContext, ILogger logger) : base(skopeiDbContext, logger)
        {
        }

        public bool Update(Product product, UpdateProductDto updateProductDto)
            // Updates the found product propperties with new values.
            // This should optimaly be done with an automapper but due to the time box of this project not implemented.
        {
            try
            {
                product.Name = updateProductDto.Name;
                product.Quantity = updateProductDto.Quantity;
                product.Price = updateProductDto.Price;
                product.DateModified = DateTime.UtcNow;

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{ProductRepository} Error on 'Update' method", typeof(Product));
                return false;
            }
        }
        public override async Task<bool> Delete(int id)
            // This method finds the according Product and sets the IsDeleted property to true.
        {
            try
            {
                var product = await _dbSet.FindAsync(id);

                if (product == null)
                    return false;

                product.IsDeleted = true;

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{ProductRepository} Error on 'Delete' method", typeof(Product));
                return false;
            }
        }

    }
}
