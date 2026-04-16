using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sprint3.Data.Contexts;

// Esta classe é lida APENAS pelo terminal/Entity Framework na hora de gerar o banco
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        NewMethod(optionsBuilder);

        return new AppDbContext(optionsBuilder.Options);

        static void NewMethod(DbContextOptionsBuilder<AppDbContext> optionsBuilder)
        {

            // Aqui configuramos o banco temporário para o Migration funcionar.
            // Estou usando SQLite como exemplo prático, mas você pode usar UseSqlServer() se preferir.
            optionsBuilder.UseSqlite("Data Source=banco.db");
        }
    }
}