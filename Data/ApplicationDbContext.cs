using Microsoft.EntityFrameworkCore;
using AlmoxManagerApi.Models.Entities;
using AlmoxManagerApi.Models.Dtos;

namespace AlmoxManagerApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadSummaryDto> LoadSummaries { get; set; }
        public DbSet<LoadFilteredDto> LoadFilters { get; set; }
    }
}
