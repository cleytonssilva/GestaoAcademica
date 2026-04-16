# Sprint3.Data

Projeto de **Camada de Acesso a Dados (DAL - Data Access Layer)** para o Sprint3.

## Objetivo

Centralizar toda a lógica de acesso a dados, permitindo que ela seja facilmente reutilizada por múltiplos projetos da solução, como APIs, serviços e outras aplicações.

## Estrutura

```
Sprint3.Data/
├── Contexts/
│   └── IDataContext.cs      # Interface para contexto de dados
├── Entities/
│   └── BaseEntity.cs        # Classe base para todas as entidades
├── Interfaces/
│   └── IRepository.cs       # Interface genérica para repositórios
└── Repositories/
    └── Repository.cs        # Classe base genérica para repositórios
```

## Componentes

### 1. **Entities** (Entidades)
Definem os modelos de dados do domínio. Herdam de `BaseEntity` que já inclui propriedades comuns como `Id`, `CreatedAt`, `UpdatedAt` e `IsActive`.

**Exemplo de uso:**
```csharp
public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

### 2. **Repositories** (Repositórios)
Implementam o padrão Repository, fornecendo métodos para acesso a dados. A classe base `Repository<T>` fornece operações CRUD comuns.

**Exemplo de uso:**
```csharp
public class ProductRepository : Repository<Product>
{
    // Implementar métodos específicos de Product
}
```

### 3. **Interfaces** (Interfaces)
Definem contratos para os repositórios, promovendo inversão de controle (IoC).

### 4. **Contexts** (Contextos)
Gerenciam a conexão e transações com o banco de dados.

## Como Usar

### 1. Criar uma nova Entidade

```csharp
// Em Entities/Product.cs
namespace Sprint3.Data.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
```

### 2. Criar um Repositório

```csharp
// Em Repositories/ProductRepository.cs
namespace Sprint3.Data.Repositories;

public class ProductRepository : Repository<Product>
{
    public async Task<Product?> GetByNameAsync(string name)
    {
        // Implementação específica
        return await Task.FromResult<Product?>(null);
    }
}
```

### 3. Usar no Projeto Principal

No projeto `Sprint3` (API), você pode injetar o repositório:

```csharp
// No Program.cs
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

// No Controller
public class ProductController : ControllerBase
{
    private readonly IRepository<Product> _repository;

    public ProductController(IRepository<Product> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }
}
```

## Referências

- Este projeto é referenciado por: `Sprint3`
- Use-o para criar outros projetos que precisem de acesso a dados

## Próximos Passos

1. ✅ Estrutura criada
2. ⏳ Conectar com banco de dados (Entity Framework Core, Dapper, etc.)
3. ⏳ Criar entidades específicas do domínio
4. ⏳ Implementar repositórios específicos
5. ⏳ Adicionar migrations e seeds
