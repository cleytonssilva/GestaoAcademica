namespace Sprint3.Data.Contexts;

/// <summary>
/// Interface para o contexto de dados
/// Define o contrato para acesso ao banco de dados
/// </summary>
public interface IDataContext : IDisposable
{
    /// <summary>
    /// Salva todas as mudanças no banco de dados
    /// </summary>
    /// <returns>Número de entidades afetadas</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Inicia uma nova transação
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Confirma a transação atual
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Desfaz a transação atual
    /// </summary>
    Task RollbackTransactionAsync();
}
