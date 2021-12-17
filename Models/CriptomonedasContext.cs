using Microsoft.EntityFrameworkCore;

namespace ApiMonedas.Models
{
    public class CriptomonedasContext : DbContext
    {
        public CriptomonedasContext(DbContextOptions<CriptomonedasContext> options)
            : base(options)
        {
        }

        public DbSet<Criptomonedas> Criptomonedas { get; set; }
    }
}