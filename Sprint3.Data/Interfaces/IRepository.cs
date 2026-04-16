namespace Sprint3.Data.Interfaces;

/// <summary>
/// Interface base para repositórios genéricos
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Obtém todas as entidades
    /// </summary>
    /// <returns>Coleção de entidades</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Obtém uma entidade por ID
    /// </summary>
    /// <param name="id">ID da entidade</param>
    /// <returns>Entidade encontrada ou null</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Adiciona uma nova entidade
    /// </summary>
    /// <param name="entity">Entidade a ser adicionada</param>
    /// <returns>Entidade adicionada</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Atualiza uma entidade
    /// </summary>
    /// <param name="entity">Entidade a ser atualizada</param>
    /// <returns>Entidade atualizada</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Remove uma entidade
    /// </summary>
    /// <param name="id">ID da entidade a ser removida</param>
    /// <returns>Indica se a exclusão foi bem-sucedida</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Verifica se existe uma entidade com o ID fornecido
    /// </summary>
    /// <param name="id">ID da entidade</param>
    /// <returns>True se existe, False caso contrário</returns>
    Task<bool> ExistsAsync(int id);
}
