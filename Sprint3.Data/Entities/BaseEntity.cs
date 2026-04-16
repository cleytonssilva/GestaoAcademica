namespace Sprint3.Data.Entities;

/// <summary>
/// Classe base para todas as entidades do domínio
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identificador único da entidade
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Data de criação da entidade
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Data da última atualização da entidade
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indica se a entidade está ativa
    /// </summary>
    public bool IsActive { get; set; } = true;
}
