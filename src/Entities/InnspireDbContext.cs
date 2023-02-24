using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnspireWebAPI.Entities
{
    public class InnspireDbContext : IdentityDbContext<InnspireUser, InnspireRole, string>
    {
        public InnspireDbContext(DbContextOptions<InnspireDbContext> options) : base(options)
        {

        }
    }
}
