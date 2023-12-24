using MegaOne.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaOne.Contexts;

public class MegaOneDBContext :DbContext
{
    public MegaOneDBContext(DbContextOptions opt) : base(opt) { }
    public DbSet<Slider> Sliders { get; set; }
}
