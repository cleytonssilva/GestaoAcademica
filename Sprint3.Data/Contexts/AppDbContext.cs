using Microsoft.EntityFrameworkCore;

namespace Sprint3.Data.Contexts; // Ajustado para a sua pasta atual

// A classe herda de DbContext (Obrigatório para o Entity Framework funcionar)
public class AppDbContext : DbContext
{
    // Construtor que recebe as opções de configuração (como a connection string)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Futuramente, você vai mapear suas entidades (tabelas) aqui.
    // Exemplo: public DbSet<Usuario> Usuarios { get; set; }
}
