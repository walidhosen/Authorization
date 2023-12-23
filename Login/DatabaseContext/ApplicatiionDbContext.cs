using Login.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Login.DatabaseContext
{
    public class ApplicatiionDbContext(DbContextOptions<ApplicatiionDbContext> options) : IdentityDbContext<AppUser>(options)
    {


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicatiionDbContext).Assembly);
        }

    }
}
