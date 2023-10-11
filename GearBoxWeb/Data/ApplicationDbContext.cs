using Microsoft.EntityFrameworkCore;

namespace GearBoxWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

            
        }
    }
}
