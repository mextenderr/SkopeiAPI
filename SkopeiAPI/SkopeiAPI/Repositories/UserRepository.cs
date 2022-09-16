using Microsoft.Extensions.Logging;
using SkopeiAPI.DbService;
using SkopeiAPI.Models;
using SkopeiAPI.Models.Dto;
using SkopeiAPI.Repositories.Generic;
using System;
using System.Threading.Tasks;

namespace SkopeiAPI.Repositories
{
    // This class implements User specific methods and derives from the generic baseclass repository.
    public class UserRepository : GenericRepository<User>
    {
        // The constructer sends the arguments to the baseclass where they can be accessed accordingly
        public UserRepository(SkopeiDbContext skopeiDbContext, ILogger logger) : base(skopeiDbContext, logger)
        {
        }

        public bool Update(User user, UpdateUserDto updateUserDto)
        // Updates the found product propperties with new values.
        // This should optimaly be done with an automapper but due to the time box of this project not implemented.
        {
            try
            {
                user.Name = updateUserDto.Name;
                user.Email = updateUserDto.Email;
                user.DateModified = DateTime.UtcNow;

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{UserRepository} Error on 'Update' method", typeof(User));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        // This method finds the according User and sets the IsDeleted property to true.
        {
            try
            {
                var user = await _dbSet.FindAsync(id);

                if (user == null)
                    return false;

                user.IsDeleted = true;

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{UserRepository} Error on 'Delete' method", typeof(User));
                return false;
            }
        }

    }
}
