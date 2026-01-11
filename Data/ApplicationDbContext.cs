using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CCIMS.Models;

namespace CCIMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Case> Cases { get; set; }
        public DbSet<InvestigationLog> InvestigationLogs { get; set; }
        
    }
}
