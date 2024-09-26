using Convite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Convite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurando a relação um-para-muitos entre ApplicationUser e Endereco
            modelBuilder.Entity<Endereco>()
                .HasOne(e => e.User) // Um endereço tem um usuário
                .WithMany(u => u.Enderecos) // Um usuário tem muitos endereços
                .HasForeignKey(e => e.UserId)
                .IsRequired(); // Define que o UserId é obrigatório
        }

    }
}
