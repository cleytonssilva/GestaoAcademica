# 📐 PADRÕES E CONVENÇÕES DE CÓDIGO

**Versão:** 1.0  
**Data:** Janeiro 2025  
**Status:** Obrigatório para todo desenvolvimento

---

## 1. NOMENCLATURA

### 1.1 Classes

```csharp
// ✅ CORRETO: PascalCase, nomes significativos
public class RepositorioAluno { }
public class ServicoCadastro { }
public class ValidadorAcademico { }

// ❌ INCORRETO
public class repo { }
public class Serv { }
public class Val { }
```

### 1.2 Métodos e Propriedades

```csharp
// ✅ CORRETO: PascalCase, verbo + substantivo
public void CadastrarAluno() { }
public Aluno ObterAluno(int matricula) { }
public bool VerificarMatriculaDuplicada(int matricula) { }
public string ObterInformacoes() { }

// ❌ INCORRETO
public void cadastrar() { }
public Aluno obter(int m) { }
public bool verificar(int m) { }
public string info() { }
```

### 1.3 Variáveis Locais e Parâmetros

```csharp
// ✅ CORRETO: camelCase, descritivos
public void CadastrarAluno(string nomeCompleto, string email)
{
    int matriculaAluno = 2025001;
    var dataNascimento = new DateTime(2005, 3, 10);
    bool foiCadastrado = false;
}

// ❌ INCORRETO
public void CadastrarAluno(string n, string e)
{
    int m = 2025001;
    var d = new DateTime(2005, 3, 10);
    bool f = false;
}
```

### 1.4 Constantes

```csharp
// ✅ CORRETO: UPPER_CASE
public const int IDADE_MINIMA_ALUNO = 18;
public const double NOTA_MINIMA_APROVACAO = 6.0;
public const decimal SALARIO_MINIMO = 1000M;

// ❌ INCORRETO
public const int IdadeMinima = 18;
public const double notaMinimaAprovacao = 6.0;
```

### 1.5 Campos Privados

```csharp
// ✅ CORRETO: _camelCase
private List<Aluno> _alunos;
private IRepositorioAluno _repositorioAluno;
private readonly object _lockObject = new object();

// ❌ INCORRETO
private List<Aluno> alunos;
private List<Aluno> m_alunos;
private List<Aluno> $alunos;
```

### 1.6 Interfaces

```csharp
// ✅ CORRETO: Prefixo 'I' + PascalCase
public interface IRepositorioAluno { }
public interface IServicoMatricula { }
public interface IPersistencia { }

// ❌ INCORRETO
public interface RepositorioAluno { }
public interface CadastroInterface { }
```

---

## 2. ESTRUTURA DE CLASSE

### 2.1 Ordem de Declaração

```csharp
public class ExemploCompleto
{
    // 1. Constantes
    private const int TAMANHO_MAXIMO = 100;
    public const double PI = 3.14159;

    // 2. Campos estáticos
    private static int _contagem = 0;

    // 3. Campos de instância (privados com _)
    private List<Aluno> _alunos;
    private IRepositorioAluno _repositorio;

    // 4. Propriedades (public)
    public Guid Id { get; private set; }
    public string Nome { get; set; }

    // 5. Construtores (do mais específico para o geral)
    public ExemploCompleto(string nome)
    {
        Nome = nome;
        Id = Guid.NewGuid();
    }

    // 6. Métodos públicos
    public void AdicionarAluno(Aluno aluno)
    {
        _alunos.Add(aluno);
    }

    // 7. Métodos privados
    private void ValidarAluno(Aluno aluno)
    {
        if (aluno == null)
            throw new ArgumentNullException(nameof(aluno));
    }

    // 8. Propriedade computada
    public int Total => _alunos.Count;
}
```

### 2.2 Espaçamento Dentro de Classe

```csharp
public class Disciplina
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }

    // Linha em branco SEPARANDO RESPONSABILIDADES
    public Disciplina(string nome, string codigo)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Codigo = codigo;
    }

    // Linha em branco antes de novo método
    public override string ToString()
    {
        return $"{Nome} ({Codigo})";
    }
}
```

---

## 3. COMENTÁRIOS E DOCUMENTAÇÃO

### 3.1 XML Documentation

```csharp
/// <summary>
/// Cadastra um novo aluno no sistema com validação automática.
/// </summary>
/// <param name="nome">Nome completo do aluno (3+ caracteres)</param>
/// <param name="email">Email válido no padrão usuario@dominio.com</param>
/// <param name="dataNascimento">Data de nascimento (deve ter 18+ anos)</param>
/// <param name="cpf">CPF válido no formato XXX.XXX.XXX-XX</param>
/// <param name="matricula">Número único de matrícula</param>
/// <returns>Objeto Aluno cadastrado com sucesso</returns>
/// <exception cref="ArgumentException">Lançado se dados são inválidos</exception>
/// <exception cref="InvalidOperationException">Lançado se matrícula já existe</exception>
/// <example>
/// <code>
/// var aluno = servicoCadastro.CadastrarAluno(
///     "João Silva",
///     "joao@email.com",
///     new DateTime(2005, 3, 10),
///     "123.456.789-00",
///     2025001);
/// </code>
/// </example>
public Aluno CadastrarAluno(string nome, string email, 
                           DateTime dataNascimento, 
                           string cpf, int matricula)
{
    // Implementação
}
```

### 3.2 Comentários Inline

```csharp
public void MatricularAluno(Aluno aluno, Disciplina disciplina)
{
    // Validar nulidade dos parâmetros
    if (aluno == null)
        throw new ArgumentNullException(nameof(aluno));

    if (disciplina == null)
        throw new ArgumentNullException(nameof(disciplina));

    // Verificar se aluno já está matriculado na disciplina
    if (_repositorioMatricula.VerificarMatriculaEmDisciplina(
        aluno.Matricula, disciplina.Codigo))
    {
        throw new InvalidOperationException(
            "Aluno já está matriculado nesta disciplina");
    }

    // Criar e persistir matrícula
    var matricula = new Matricula(aluno, disciplina);
    aluno.Matriculas.Add(matricula);
    _repositorioMatricula.Adicionar(matricula);
}
```

### 3.3 O que NOT fazer

```csharp
// ❌ NÃO: Óbvio demais
public void AdicionarAluno(Aluno aluno)
{
    // Adiciona um aluno
    _alunos.Add(aluno);
}

// ❌ NÃO: Comentário desatualizado
public void CadastrarAluno(string nome)
{
    // TODO: Implementar validação de CPF (feito!)
    // BUG: Não funciona (funciona)
}

// ❌ NÃO: Comentário explicando código ruim
public void ProcessarDados(List<Aluno> a)
{
    // Filtrar alunos com média > 6
    // E ordenar por nome
    // E fazer algo mais...
    var resultado = a.Where(x => x.CalcularMedia() > 6)
                     .OrderBy(x => x.Nome)
                     .ToList();
}
```

---

## 4. TAMANHO E COMPLEXIDADE

### 4.1 Máximo de Linhas por Método

```csharp
// ✅ CORRETO: Até 30 linhas por método
public Aluno CadastrarAluno(string nome, string email, 
                           DateTime dataNascimento, 
                           string cpf, int matricula)
{
    // 1-5: Validações
    ValidadorAcademico.ValidarNome(nome);
    ValidadorAcademico.ValidarEmail(email);
    ValidadorAcademico.ValidarDataNascimento(dataNascimento);
    ValidadorAcademico.ValidarCPF(cpf);
    ValidadorAcademico.ValidarMatricula(matricula);

    // 6-10: Verificar duplicidades
    if (_repositorioAluno.VerificarMatriculaDuplicada(matricula))
        throw new InvalidOperationException("Matrícula já existe");

    if (_repositorioAluno.VerificarCPFDuplicado(cpf))
        throw new InvalidOperationException("CPF já existe");

    // 11-15: Criar objeto
    var aluno = new Aluno(nome, email, dataNascimento, cpf, matricula);

    // 16-20: Persistir
    _repositorioAluno.Adicionar(aluno);

    // 21-25: Retornar
    return aluno;
}

// ❌ INCORRETO: Muito longo (extrair em métodos)
public Aluno CadastrarAluno(string nome, string email, ...)
{
    // 50+ linhas de código
    // Fazer validação
    // Fazer transformação
    // Fazer persistência
    // Fazer notificação
    // ... tudo em um método
}
```

### 4.2 Máximo de Parâmetros

```csharp
// ✅ CORRETO: Até 4 parâmetros
public Aluno CadastrarAluno(string nome, string email, 
                           DateTime dataNascimento, string cpf)

// ⚠️ CONSIDERAR: 5 parâmetros
public void CriarAluno(string nome, string email, 
                      DateTime dataNascimento, string cpf, int matricula)
// → Considere usar objeto (DTO)

// ❌ INCORRETO: Muitos parâmetros
public void ProcessarAluno(string n, string e, DateTime d, 
                          string c, int m, string a, int i, bool f)
// → Use classe para agrupar dados
public class CadastroAlunoDTO
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    // ...
}
```

---

## 5. TRATAMENTO DE EXCEÇÕES

### 5.1 Padrão de Tratamento

```csharp
// ✅ CORRETO: Específico
try
{
    ValidadorAcademico.ValidarCPF(cpf);
    var aluno = _servicoCadastro.CadastrarAluno(nome, email, data, cpf, matricula);
    ExibirSucesso($"Aluno {aluno.Nome} cadastrado!");
}
catch (ArgumentException ex)
{
    // Erro de validação
    ExibirErro($"Validação: {ex.Message}");
}
catch (InvalidOperationException ex)
{
    // Erro de negócio
    ExibirErro($"Operação: {ex.Message}");
}
catch (Exception ex)
{
    // Erro inesperado
    ExibirErro($"Erro inesperado: {ex.Message}");
    // Opcionalmente: log.Error(ex);
}

// ❌ INCORRETO: Genérico
try
{
    // operação
}
catch (Exception ex)
{
    Console.WriteLine("Erro!");
}
```

### 5.2 Lançar Exceções

```csharp
// ✅ CORRETO: Mensagem clara
if (aluno == null)
    throw new ArgumentNullException(nameof(aluno), 
        "Aluno não pode ser nulo");

if (!email.Contains("@"))
    throw new ArgumentException(
        "Email deve conter '@'", nameof(email));

if (_repositorio.Existe(cpf))
    throw new InvalidOperationException(
        "CPF já existe no sistema");

// ❌ INCORRETO
throw new Exception("Erro");
throw new ApplicationException("Algo deu errado");
if (cpf == null) throw new Exception();
```

---

## 6. PROPRIEDADES E ENCAPSULAMENTO

### 6.1 Propriedades Auto-Implementadas

```csharp
// ✅ CORRETO: Para dados simples
public class Aluno
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    // ✅ Com inicialização
    public List<Nota> Notas { get; set; } = new List<Nota>();

    // ✅ Apenas leitura
    public DateTime DataCadastro { get; private set; } = DateTime.UtcNow;
}
```

### 6.2 Propriedades Computadas

```csharp
// ✅ CORRETO: Propriedade que calcula valor
public class Aluno : Pessoa
{
    public List<Nota> Notas { get; set; }

    // Propriedade que calcula baseado em dados
    public double Media
    {
        get
        {
            if (Notas.Count == 0) return 0;
            return Notas.Average(n => n.Valor);
        }
    }

    // Versão mais concisa (C# 6+)
    public bool EstaAprovado => Media >= 6.0;
}
```

### 6.3 Validação em Setter

```csharp
// ✅ CORRETO: Validar no setter
public class Aluno
{
    private int _matricula;
    
    public int Matricula
    {
        get { return _matricula; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Matrícula deve ser positiva");
            _matricula = value;
        }
    }
}
```

---

## 7. COLECCÕES E LINQ

### 7.1 Inicializar Coleções

```csharp
// ✅ CORRETO: Inicialização com =
public class Aluno
{
    public List<Nota> Notas { get; set; } = new List<Nota>();
    public List<Matricula> Matriculas { get; set; } = new List<Matricula>();
}

// ⚠️ EVITAR: Set null como padrão
public List<Nota> Notas { get; set; } // Pode ser null!

// ✅ CORRETO: Usar LINQ apropriadamente
var alunosAprovados = alunos
    .Where(a => a.CalcularMedia() >= 6.0)
    .OrderByDescending(a => a.CalcularMedia())
    .ToList();
```

### 7.2 Retornar Coleções com Segurança

```csharp
// ✅ CORRETO: Retornar cópia
public List<Aluno> ListarTodos()
{
    return new List<Aluno>(_alunos); // Cópia
}

// ❌ INCORRETO: Retornar referência direta
public List<Aluno> ListarTodos()
{
    return _alunos; // Alguém pode modificar!
}
```

---

## 8. FERRAMENTAS E VERIFICAÇÃO

### 8.1 Análise de Código

```bash
# Verificar em Visual Studio
- Tools > Analyze Code > Run Code Analysis

# Verificar em VS Code
- Instalar: C# DevKit
- Usa roslyn analyzer automaticamente
```

### 8.2 Checklist Antes de Commit

- ✅ Código compila sem warnings
- ✅ Nomes seguem convenção (CamelCase, _camelCase)
- ✅ Máximo 30 linhas por método
- ✅ Máximo 4 parâmetros por método
- ✅ Comentários XML em públicos
- ✅ Exceções específicas
- ✅ Sem TODO/FIXME não documentados
- ✅ Testes unitários (se aplicável)

---

## 9. EXEMPLOS COMPLETOS

### 9.1 Classe Bem Estruturada

```csharp
/// <summary>
/// Valida todos os dados acadêmicos do sistema.
/// Centraliza regras de negócio para reutilização.
/// </summary>
public class ValidadorAcademico
{
    // ✅ Constantes bem nomeadas
    public const int IDADE_MINIMA_ALUNO = 18;
    public const double NOTA_MINIMA_APROVACAO = 6.0;
    public const decimal SALARIO_MINIMO = 1000M;

    /// <summary>
    /// Valida um CPF conforme regras brasileiras.
    /// </summary>
    /// <param name="cpf">CPF no formato XXX.XXX.XXX-XX</param>
    /// <exception cref="ArgumentException">Se CPF inválido</exception>
    public static void ValidarCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException(
                "CPF não pode ser vazio", nameof(cpf));

        string cpfLimpo = Regex.Replace(cpf, @"\D", "");

        if (cpfLimpo.Length != 11)
            throw new ArgumentException(
                "CPF deve conter 11 dígitos", nameof(cpf));

        // Validações adicionais...
    }

    /// <summary>
    /// Verifica se uma nota representa aprovação.
    /// </summary>
    public static bool EstaAprovado(double nota)
    {
        return nota >= NOTA_MINIMA_APROVACAO;
    }

    /// <summary>
    /// Calcula a situação do aluno.
    /// </summary>
    public static string CalcularSituacao(double media)
    {
        if (media >= NOTA_MINIMA_APROVACAO)
            return "APROVADO";
        else if (media >= 4.0)
            return "RECUPERAÇÃO";
        else
            return "REPROVADO";
    }
}
```

### 9.2 Método Bem Estruturado

```csharp
/// <summary>
/// Cadastra um novo aluno com validação automática.
/// </summary>
public Aluno CadastrarAluno(string nome, string email, 
                           DateTime dataNascimento, 
                           string cpf, int matricula)
{
    // 1. Validar dados pessoais
    ValidadorAcademico.ValidarNome(nome);
    ValidadorAcademico.ValidarEmail(email);
    ValidadorAcademico.ValidarDataNascimento(dataNascimento);
    ValidadorAcademico.ValidarCPF(cpf);
    ValidadorAcademico.ValidarMatricula(matricula);

    // 2. Verificar duplicidades
    if (_repositorioAluno.VerificarMatriculaDuplicada(matricula))
        throw new InvalidOperationException(
            $"Matrícula {matricula} já existe no sistema");

    if (_repositorioAluno.VerificarCPFDuplicado(cpf))
        throw new InvalidOperationException(
            $"CPF {cpf} já foi cadastrado");

    // 3. Criar aluno
    var aluno = new Aluno(nome, email, dataNascimento, cpf, matricula);

    // 4. Persistir
    _repositorioAluno.Adicionar(aluno);

    return aluno;
}
```

---

## 10. ANTI-PATTERNS A EVITAR

### 10.1 Magic Numbers

```csharp
// ❌ RUIM
if (idade > 18) { }
if (nota > 6) { }

// ✅ BOM
const int IDADE_MINIMA = 18;
const double NOTA_MINIMA_APROVACAO = 6.0;

if (idade >= IDADE_MINIMA) { }
if (nota >= NOTA_MINIMA_APROVACAO) { }
```

### 10.2 Null Checks Excessivos

```csharp
// ❌ RUIM
if (aluno != null && aluno.Notas != null && aluno.Notas.Count > 0)
{
    // Fazer algo
}

// ✅ BOM
if (aluno?.Notas?.Count > 0)
{
    // Fazer algo
}
```

### 10.3 Métodos Muito Longos

```csharp
// ❌ RUIM: Tudo em um método
public void ProcessarDados()
{
    // Ler dados (10 linhas)
    // Validar dados (15 linhas)
    // Transformar dados (20 linhas)
    // Persistir dados (10 linhas)
    // Notificar usuário (5 linhas)
    // ... 60+ linhas
}

// ✅ BOM: Dividir responsabilidades
public void ProcessarDados()
{
    var dados = LerDados();
    ValidarDados(dados);
    var transformados = TransformarDados(dados);
    PersistirDados(transformados);
    NotificarUsuario();
}
```

---

## CONCLUSÃO

Estes padrões e convenções garantem:

✅ Código legível e manutenível  
✅ Fácil colaboração entre desenvolvedores  
✅ Menos bugs e erros  
✅ Facilita aprendizado de OOP  

---

**Versão:** 1.0  
