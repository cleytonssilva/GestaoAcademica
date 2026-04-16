using Sprint3.Data.Interfaces;

namespace Sprint3.Data.Repositories;

/// <summary>
/// Classe base genérica para repositórios
/// Implementa operações CRUD comuns
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade</typeparam>
public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected List<TEntity> _data = new();

    /// <summary>
    /// Obtém todas as entidades
    /// </summary>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(_data.AsEnumerable());
    }

    /// <summary>
    /// Obtém uma entidade por ID
    /// </summary>
    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        // Esta é uma implementação básica
        // Será sobrescrita pela implementação específica com banco de dados
        return await Task.FromResult<TEntity?>(null);
    }

    /// <summary>
    /// Adiciona uma nova entidade
    /// </summary>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        _data.Add(entity);
        return await Task.FromResult(entity);
    }

    /// <summary>
    /// Atualiza uma entidade
    /// </summary>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        // Esta é uma implementação básica
        // Será sobrescrita pela implementação específica com banco de dados
        return await Task.FromResult(entity);
    }

    /// <summary>
    /// Remove uma entidade
    /// </summary>
    public virtual async Task<bool> DeleteAsync(int id)
    {
        // Esta é uma implementação básica
        // Será sobrescrita pela implementação específica com banco de dados
        return await Task.FromResult(false);
    }

    /// <summary>
    /// Verifica se existe uma entidade
    /// </summary>
    public virtual async Task<bool> ExistsAsync(int id)
    {
        // Esta é uma implementação básica
        // Será sobrescrita pela implementação específica com banco de dados
        return await Task.FromResult(false);
    }
}
