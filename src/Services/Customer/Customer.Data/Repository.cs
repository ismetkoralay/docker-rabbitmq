namespace Customer.Data
{
    public class Repository<T> : RepositoryBase<T> where T : class
    {
        public Repository(CustomerDbContext dbContext) : base(dbContext)
        {
        }
    }
}