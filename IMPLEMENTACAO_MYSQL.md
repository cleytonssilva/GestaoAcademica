# 🗄️ Implementação de Persistência em MySQL

## Resumo da Implementação

O sistema Gestão Acadêmica foi integrado com persistência completa em MySQL usando Entity Framework 6 (.NET Framework). Isso permite que todos os dados sejam armazenados permanentemente em banco de dados, em vez de apenas na memória.

## Arquitetura de Banco de Dados

### Schema do Banco de Dados

O banco de dados `gestao_academica` possui 7 tabelas principais:

```
┌─────────────────────────────────────────────────────────────┐
│                    SCHEMA DO BANCO                          │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  pessoas (base para alunos e professores)                   │
│  ├── alunos (específico de alunos)                         │
│  ├── professores (específico de professores)               │
│  ├── disciplinas (disciplinas/cursos)                      │
│  ├── turmas (seções de disciplinas)                        │
│  ├── matriculas (inscrições de alunos em turmas)          │
│  └── avaliacoes (notas/avaliações)                         │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

### Tabelas Principais

#### `pessoas`
- **Id** (GUID): Identificador único
- **tipo** (ENUM): 'aluno' ou 'professor'
- **nome**: Nome completo
- **email**: Email (único)
- **cpf**: CPF (único)
- **data_nascimento**: Data de nascimento
- **data_cadastro**: Data/hora de criação

#### `alunos`
- **id** (FK): Referência a pessoas
- **matricula** (INT): Número de matrícula (único)
- **data_matricula**: Data da matrícula
- **ativo**: Indicador de status

#### `professores`
- **id** (FK): Referência a pessoas
- **disciplina_principal**: Disciplina principal
- **salario**: Salário (DECIMAL 10,2)
- **data_admissao**: Data de admissão
- **ativo**: Indicador de status

#### `disciplinas`
- **id** (GUID): Identificador único
- **nome**: Nome da disciplina
- **codigo**: Código (único, ex: MAT001)
- **professor_responsavel_id** (FK): Professor responsável
- **descricao**: Descrição
- **ativa**: Status

#### `turmas`
- **id** (GUID): Identificador único
- **disciplina_id** (FK): Disciplina
- **numero_turma**: Número/identificação da turma
- **semestre**: Semestre
- **ano**: Ano
- **capacidade_maxima**: Limite de alunos

#### `matriculas`
- **id** (GUID): Identificador único
- **aluno_id** (FK): Aluno
- **turma_id** (FK): Turma
- **data_matricula**: Data da inscrição
- **data_conclusao**: Data de conclusão (se aplicável)
- **situacao** (ENUM): ativa / concluida / cancelada

#### `avaliacoes`
- **id** (GUID): Identificador único
- **matricula_id** (FK): Matrícula
- **professor_id** (FK): Professor que atribuiu
- **turma_id** (FK): Turma
- **valor** (DECIMAL 4,2): Nota (0.0 a 10.0)
- **tipo_avaliacao**: Tipo (prova, trabalho, etc.)
- **peso**: Peso da avaliação
- **data_avaliacao**: Data da avaliação

## Instalação e Configuração

### 1. Pré-requisitos

- MySQL Server 5.7 ou superior
- Visual Studio 2019+ ou VS Code com .NET Framework 4.7.2

### 2. Criar o Banco de Dados

Execute o script SQL em seu MySQL:

```bash
# Abra o MySQL
mysql -u root -p

# Execute o script
source BD_Scripts/01_create_database.sql
```

Ou copie e cole o conteúdo do arquivo `BD_Scripts/01_create_database.sql` no MySQL Workbench.

### 3. Configurar a String de Conexão

Edite o arquivo `App.config` e atualize a connection string se necessário:

```xml
<connectionStrings>
  <add name="GestaoAcademica" 
       connectionString="Server=localhost;Database=gestao_academica;User=root;Password=;" 
       providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

**Campos:**
- `Server`: Endereço do MySQL (padrão: localhost)
- `Database`: Nome do banco (padrão: gestao_academica)
- `User`: Usuário MySQL (padrão: root)
- `Password`: Senha do usuário (padrão: vazio)

### 4. Restaurar Pacotes NuGet

```bash
# No Visual Studio: Tools → NuGet Package Manager → Manage NuGet Packages for Solution
# Ou via PowerShell:
cd GestaoAcademica
nuget restore
```

### 5. Compilar e Executar

```bash
# Compilar
dotnet build

# Executar
dotnet run
```

## Camadas de Persistência

### Repository Pattern

O sistema usa o padrão Repository para abstrair a persistência:

```csharp
// Interface (agnostic de tecnologia)
public interface IRepositorioAluno
{
    void Adicionar(Aluno aluno);
    Aluno ObterPorMatricula(int matricula);
    bool VerificarMatriculaDuplicada(int matricula);
    List<Aluno> ListarTodos();
}

// Implementação In-Memory (para testes)
public class RepositorioAlunoEmMemoria : IRepositorioAluno { ... }

// Implementação MySQL (produção)
public class RepositorioAlunoMySQL : IRepositorioAluno { ... }
```

### DbContext (Entity Framework 6)

```csharp
public class GestaoAcademicaContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    
    // Configuração de mapeamento
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Define como cada entidade mapeia para as tabelas
    }
}
```

### Inicialização em Program.cs

```csharp
public static void Main(string[] args)
{
    try
    {
        // Criar DbContext
        var context = new GestaoAcademicaContext();
        
        // Criar repositórios MySQL
        IRepositorioAluno repositorio = new RepositorioAlunoMySQL(context);
        
        // Usar repositório nos serviços
        ServicoCadastro servico = new ServicoCadastro(repositorio, ...);
        
        // Executar aplicação
        terminalUI.Executar();
    }
    catch (Exception ex)
    {
        // Fallback para in-memory se houver erro de conexão
        IRepositorioAluno repositorio = new RepositorioAlunoEmMemoria();
    }
}
```

## Fluxo de Dados

```
┌─────────────────────────────────────────────┐
│      Usuário (TerminalUI)                   │
└────────────────┬────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────┐
│      Serviços de Negócio                    │
│  (ServicoCadastro, ServicoMatricula, etc)   │
└────────────────┬────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────┐
│      Repositórios (Interfaces)              │
│  (IRepositorioAluno, IRepositorioProfessor) │
└────────────────┬────────────────────────────┘
                 │
      ┌──────────┴──────────┐
      │                     │
      ▼                     ▼
┌──────────────┐    ┌──────────────────┐
│  In-Memory   │    │   Entity Framework  │
│  (Testes)    │    │   MySQL (Produção)  │
└──────────────┘    └──────────────────┘
                         │
                         ▼
                    ┌──────────────┐
                    │   MySQL DB   │
                    │  gestao_      │
                    │  academica   │
                    └──────────────┘
```

## Transações e Consistência

O Entity Framework 6 gerencia automaticamente transações:

```csharp
public void Adicionar(Aluno aluno)
{
    try
    {
        _context.Alunos.Add(aluno);
        _context.SaveChanges();  // Commit da transação
    }
    catch (DbUpdateException ex)
    {
        // Rollback automático
        throw new InvalidOperationException("Erro ao salvar", ex);
    }
}
```

## Validações de Banco de Dados

### Constraints

- **UNIQUE**: Email, CPF, Matrícula, Código de Disciplina
- **NOT NULL**: Campos obrigatórios
- **FOREIGN KEY**: Integridade referencial com cascata
- **DEFAULT**: Valores padrão (timestamps, booleans)

### Índices para Performance

```sql
INDEX idx_email (email)
INDEX idx_cpf (cpf)
INDEX idx_matricula (matricula)
INDEX idx_codigo (codigo)
INDEX idx_professor (professor_responsavel_id)
INDEX idx_semestre (semestre, ano)
INDEX idx_data_matricula (data_matricula)
UNIQUE unique_turma (disciplina_id, numero_turma, semestre)
UNIQUE unique_matricula (aluno_id, turma_id)
```

## Troubleshooting

### Erro: "Unable to connect to MySQL"

**Solução:**
1. Verifique se MySQL está rodando: `mysql -u root -p`
2. Verifique a string de conexão em `App.config`
3. Verifique se o banco `gestao_academica` existe
4. Verifique credenciais (usuário e senha)

### Erro: "A foreign key constraint fails"

**Causa:** Tentativa de deletar registros com dependências

**Solução:** Use `CASCADE DELETE` ou delete registros dependentes primeiro

### Erro: "Duplicate entry for unique key"

**Causa:** Tentativa de inserir valor duplicado em campo UNIQUE

**Solução:** A validação em `ValidadorAcademico` deve prevenir isso. Se ocorrer, há um bug na lógica.

## Views Disponíveis

O banco fornece 4 views para consultas comuns:

```sql
-- Alunos com dados consolidados
SELECT * FROM v_alunos;

-- Professores com dados consolidados
SELECT * FROM v_professores;

-- Disciplinas com professor responsável
SELECT * FROM v_disciplinas_professor;

-- Média de alunos por disciplina
SELECT * FROM v_media_alunos;
```

## Stored Procedures

```sql
-- Calcular média ponderada de um aluno
CALL sp_calcular_media_aluno(aluno_id, turma_id, @media, @situacao);

-- Listar alunos aprovados em uma disciplina
CALL sp_alunos_aprovados_disciplina(disciplina_id);
```

## Backup e Restauração

### Backup do Banco

```bash
# Exportar estrutura e dados
mysqldump -u root -p gestao_academica > backup_academica.sql

# Exportar apenas estrutura
mysqldump -u root -p --no-data gestao_academica > schema_academica.sql
```

### Restaurar Banco

```bash
mysql -u root -p < backup_academica.sql
```

## Melhorias Futuras

- [ ] Migrations automáticas com Entity Framework
- [ ] Connection pooling otimizado
- [ ] Índices adicionais para queries complexas
- [ ] Replicação e backup automático
- [ ] Auditoria com audit log
- [ ] Versionamento de histórico
- [ ] Relatórios com views materializadas
- [ ] Performance tuning com análise de queries

## Referências

- [MySQL Documentation](https://dev.mysql.com/doc/)
- [Entity Framework 6 Code First](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/dbcontext-api)
- [MySQL Data Provider for .NET](https://dev.mysql.com/doc/connector-net/en/)
- [Database Design Best Practices](https://en.wikipedia.org/wiki/Database_design)

---

**Data**: Março 2026  
**Versão**: 1.0.0  
**Status**: Implementação Completa
