using Microsoft.Extensions.Logging;
using SkopeiAPI.DbService;
using SkopeiAPI.Repositories;
using System.Threading.Tasks;

namespace SkopeiAPI.UnitOfWorks
{
    // Using a UnitOfWork class to make sure all repositories make use of the same DbContext
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SkopeiDbContext dbContext;
        private UserRepository userRepository;
        private ProductRepository productRepository;
        private readonly ILogger logger;

        public UnitOfWork(SkopeiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.dbContext = dbContext;
            logger = loggerFactory.CreateLogger("Logs");
        }

        // Return the current User Repository if existing, else create a new one
        public UserRepository UserRepo
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(dbContext, logger);
                }
                return userRepository;
            }
        }

        public ProductRepository ProductRepo
        {
            get
            {

                if (productRepository == null)
                {
                    productRepository = new ProductRepository(dbContext, logger);
                }
                return productRepository;
            }
        }

        // Method is called to execute all changes to the tracked objects to the database
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}
