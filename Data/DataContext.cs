using Microsoft.EntityFrameworkCore;

namespace Cinema.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
