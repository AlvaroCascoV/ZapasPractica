using Microsoft.EntityFrameworkCore;
using ZapasPractica.Models;

namespace ZapasPractica.Data
{
    public class ZapasContext: DbContext
    {
        public ZapasContext(DbContextOptions<ZapasContext> options) : base(options) { }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenZapatilla> ImagenesZapatillas { get; set; }
    }
}