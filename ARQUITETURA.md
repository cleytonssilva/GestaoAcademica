# 🏗️ DOCUMENTAÇÃO TÉCNICA - ARQUITETURA E DESIGN

**Documento:** Arquitetura e Design do Sistema  
**Projeto:** Sistema de Gestão Acadêmica  
**Versão:** 1.0  

---

## 1. VISÃO ARQUITETURAL

### 1.1 Padrão de Arquitetura: Layered Architecture

O sistema segue o padrão de **Arquitetura em Camadas (Layered Architecture)**, dividindo as responsabilidades em 4 camadas principais:

```
┌─────────────────────────────────────────────────┐
│  PRESENTATION LAYER (UI)                         │
│  - TerminalUI                                    │
│  - Responsabilidade: Interação com usuário      │
└─────────────────────────────────────────────────┘
                        ↓ (depende)
┌─────────────────────────────────────────────────┐
│  SERVICE LAYER (Lógica de Negócio)              │
│  - ServicoCadastro                              │
│  - ServicoMatricula                             │
│  - ServicoAvaliacao                             │
│  - ServicoRelatorio                             │
│  - ValidadorAcademico                           │
│  Responsabilidade: Regras de negócio            │
└─────────────────────────────────────────────────┘
                        ↓ (depende)
┌─────────────────────────────────────────────────┐
│  DATA LAYER (Repositórios)                      │
│  - IRepositorioAluno                            │
│  - IRepositorioProfessor                        │
│  - IRepositorioDisciplina                       │
│  - IRepositorioMatricula                        │
│  Responsabilidade: Persistência de dados        │
└─────────────────────────────────────────────────┘
                        ↓ (depende)
┌─────────────────────────────────────────────────┐
│  DOMAIN LAYER (Modelos)                         │
│  - Pessoa (classe abstrata)                     │
│  - Aluno, Professor                             │
│  - Disciplina, Matricula, Nota                  │
│  Responsabilidade: Representação de dados       │
└─────────────────────────────────────────────────┘
```

### 1.2 Benefícios da Arquitetura

| Benefício | Descrição |
|-----------|-----------|
| **Separação de Responsabilidades** | Cada camada tem responsabilidade clara |
| **Testabilidade** | Fácil testar cada camada independentemente |
| **Manutenibilidade** | Mudanças em uma camada não afetam outras |
| **Reusabilidade** | Código reutilizável em diferentes contextos |
| **Escalabilidade** | Fácil adicionar novas funcionalidades |

---

## 2. PADRÕES DE DESIGN IMPLEMENTADOS

### 2.1 Repository Pattern

**Objetivo:** Abstrair a camada de dados

**Implementação:**

```csharp
// Interface define contrato
public interface IRepositorioAluno
{
    void Adicionar(Aluno aluno);
    Aluno ObterPorMatricula(int matricula);
    Aluno ObterPorCPF(string cpf);
    bool VerificarMatriculaDuplicada(int matricula);
    bool VerificarCPFDuplicado(string cpf);
    List<Aluno> ListarTodos();
}

// Implementação em memória
public class RepositorioAlunoEmMemoria : IRepositorioAluno
{
    private List<Aluno> _alunos = new List<Aluno>();

    public void Adicionar(Aluno aluno)
    {
        if (aluno == null)
            throw new ArgumentNullException(nameof(aluno));
        _alunos.Add(aluno);
    }

    public Aluno ObterPorMatricula(int matricula)
    {
        return _alunos.FirstOrDefault(a => a.Matricula == matricula);
    }

    // ... outros métodos
}
```

**Vantagens:**
- ✅ Fácil trocar entre persistência em memória, BD, arquivo
- ✅ Testes unitários podem usar mock
- ✅ Código fica desacoplado

---

### 2.2 Dependency Injection

**Objetivo:** Injetar dependências via construtor

**Implementação:**

```csharp
// Serviço recebe dependências via construtor
public class ServicoCadastro
{
    private readonly IRepositorioAluno _repositorioAluno;
    private readonly IRepositorioProfessor _repositorioProfessor;
    private readonly IRepositorioDisciplina _repositorioDisciplina;

    public ServicoCadastro(
        IRepositorioAluno repositorioAluno,
        IRepositorioProfessor repositorioProfessor,
        IRepositorioDisciplina repositorioDisciplina)
    {
        _repositorioAluno = repositorioAluno;
        _repositorioProfessor = repositorioProfessor;
        _repositorioDisciplina = repositorioDisciplina;
    }

    public Aluno CadastrarAluno(string nome, string email, ...)
    {
        // Usar repositórios injetados
        if (_repositorioAluno.VerificarCPFDuplicado(cpf))
            throw new InvalidOperationException("CPF já existe");

        var aluno = new Aluno(nome, email, ...);
        _repositorioAluno.Adicionar(aluno);
        return aluno;
    }
}

// No Program.cs - composição de dependências
IRepositorioAluno repositorioAluno = new RepositorioAlunoEmMemoria();
IRepositorioProfessor repositorioProfessor = new RepositorioProfessorEmMemoria();
IRepositorioDisciplina repositorioDisciplina = new RepositorioDisciplinaEmMemoria();

ServicoCadastro servicoCadastro = new ServicoCadastro(
    repositorioAluno,
    repositorioProfessor,
    repositorioDisciplina);
```

**Vantagens:**
- ✅ Baixo acoplamento
- ✅ Fácil testar (injetar mocks)
- ✅ Configuração centralizada

---

### 2.3 Service Layer Pattern

**Objetivo:** Centralizar lógica de negócio em serviços

**Implementação:**

```csharp
// ServicoCadastro contém lógica de cadastro
public class ServicoCadastro
{
    public Aluno CadastrarAluno(string nome, string email, 
                                DateTime dataNascimento, 
                                string cpf, int matricula)
    {
        // 1. Validar dados
        ValidadorAcademico.ValidarNome(nome);
        ValidadorAcademico.ValidarEmail(email);
        ValidadorAcademico.ValidarDataNascimento(dataNascimento);
        ValidadorAcademico.ValidarCPF(cpf);
        ValidadorAcademico.ValidarMatricula(matricula);

        // 2. Verificar duplicidades
        if (_repositorioAluno.VerificarMatriculaDuplicada(matricula))
            throw new InvalidOperationException("Matrícula já existe");

        if (_repositorioAluno.VerificarCPFDuplicado(cpf))
            throw new InvalidOperationException("CPF já existe");

        // 3. Persistir
        var aluno = new Aluno(nome, email, dataNascimento, cpf, matricula);
        _repositorioAluno.Adicionar(aluno);
        return aluno;
    }
}

// ServicoMatricula contém lógica de matrícula
public class ServicoMatricula
{
    public Matricula MatricularAluno(Aluno aluno, Disciplina disciplina)
    {
        if (aluno == null)
            throw new ArgumentNullException(nameof(aluno));

        if (disciplina == null)
            throw new ArgumentNullException(nameof(disciplina));

        // Validar duplicidade
        if (_repositorioMatricula.VerificarMatriculaEmDisciplina(
            aluno.Matricula, disciplina.Codigo))
            throw new InvalidOperationException(
                "Aluno já está matriculado nesta disciplina");

        // Criar matrícula
        var matricula = new Matricula(aluno, disciplina);
        aluno.Matriculas.Add(matricula);
        _repositorioMatricula.Adicionar(matricula);
        return matricula;
    }
}
```

**Vantagens:**
- ✅ Lógica centralizada e reutilizável
- ✅ UI fica simples
- ✅ Fácil testar regras de negócio

---

### 2.4 Validator Pattern

**Objetivo:** Centralizar validações de negócio

**Implementação:**

```csharp
public class ValidadorAcademico
{
    // Constantes de regras
    public const int IDADE_MINIMA_ALUNO = 18;
    public const double NOTA_MINIMA_APROVACAO = 6.0;
    public const decimal SALARIO_MINIMO = 1000M;

    // Validações
    public static void ValidarCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("CPF não pode ser vazio");

        string cpfLimpo = Regex.Replace(cpf, @"\D", "");

        if (cpfLimpo.Length != 11)
            throw new ArgumentException("CPF deve conter 11 dígitos");

        // Validar dígitos verificadores (omitido por brevidade)
        // ...
    }

    public static void ValidarNota(double nota)
    {
        if (nota < 0 || nota > 10)
            throw new ArgumentException("Nota deve estar entre 0 e 10");
    }

    public static bool EstaAprovado(double nota)
    {
        return nota >= NOTA_MINIMA_APROVACAO;
    }

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

**Uso:**

```csharp
try
{
    ValidadorAcademico.ValidarCPF(cpf);
    ValidadorAcademico.ValidarEmail(email);
    ValidadorAcademico.ValidarNota(nota);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro de validação: {ex.Message}");
}
```

**Vantagens:**
- ✅ Validações reutilizáveis
- ✅ Regras centralizadas
- ✅ Fácil manter constantes

---

## 3. DIAGRAMA DE CLASSES (Detalhado)

### 3.1 Hierarquia de Herança

```
                    Pessoa (Abstrata)
                      ↑        ↑
                      │        │
                  Aluno    Professor
                   │
         ┌─────────┼──────────┐
         │         │          │
      Notas   Matriculas   Propriedades
```

**Pessoa.cs** - Classe Base Abstrata
```csharp
public abstract class Pessoa
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    protected Pessoa(string nome, string email, 
                    DateTime dataNascimento, string cpf)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        DataNascimento = dataNascimento;
        CPF = cpf;
    }

    public abstract string ObterInformacoes();
}
```

**Aluno.cs** - Herda de Pessoa
```csharp
public class Aluno : Pessoa
{
    public int Matricula { get; set; }
    public List<Nota> Notas { get; set; } = new List<Nota>();
    public List<Matricula> Matriculas { get; set; } = new List<Matricula>();

    public Aluno(string nome, string email, DateTime dataNascimento,
                 string cpf, int matricula) 
        : base(nome, email, dataNascimento, cpf)
    {
        Matricula = matricula;
    }

    public double CalcularMedia()
    {
        if (Notas.Count == 0) return 0;
        return Notas.Average(n => n.Valor);
    }

    public override string ObterInformacoes()
    {
        return $"Aluno: {Nome}, Matrícula: {Matricula}, Média: {CalcularMedia():F2}";
    }
}
```

**Professor.cs** - Herda de Pessoa
```csharp
public class Professor : Pessoa
{
    public string Disciplina { get; set; }
    public decimal Salario { get; set; }

    public Professor(string nome, string email, 
                    DateTime dataNascimento, string cpf,
                    string disciplina, decimal salario)
        : base(nome, email, dataNascimento, cpf)
    {
        Disciplina = disciplina;
        Salario = salario;
    }

    public override string ObterInformacoes()
    {
        return $"Professor: {Nome}, Disciplina: {Disciplina}, Salário: {Salario:C}";
    }
}
```

### 3.2 Modelos de Dados

**Disciplina.cs**
```csharp
public class Disciplina
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public Professor ProfessorResponsavel { get; set; }

    public Disciplina(string nome, string codigo, 
                     Professor professorResponsavel)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Codigo = codigo;
        ProfessorResponsavel = professorResponsavel;
    }
}
```

**Matricula.cs**
```csharp
public class Matricula
{
    public Guid Id { get; set; }
    public Aluno Aluno { get; set; }
    public Disciplina Disciplina { get; set; }
    public DateTime DataMatricula { get; set; }

    public Matricula(Aluno aluno, Disciplina disciplina)
    {
        Id = Guid.NewGuid();
        Aluno = aluno;
        Disciplina = disciplina;
        DataMatricula = DateTime.UtcNow;
    }
}
```

**Nota.cs**
```csharp
public class Nota
{
    public Guid Id { get; set; }
    public Disciplina Disciplina { get; set; }
    public double Valor { get; set; }
    public DateTime DataAtribuicao { get; set; }

    public Nota(Disciplina disciplina, double valor)
    {
        Id = Guid.NewGuid();
        Disciplina = disciplina;
        Valor = valor;
        DataAtribuicao = DateTime.UtcNow;
    }
}
```

---

## 4. FLUXO DE DADOS

### 4.1 Cadastro de Aluno

```
TerminalUI
    │
    └──> CadastrarAluno()
         │
         ├──> Solicita dados do usuário
         │
         └──> ServicoCadastro.CadastrarAluno()
              │
              ├──> ValidadorAcademico.Validar*()
              │    ├──> ValidarNome()
              │    ├──> ValidarEmail()
              │    ├──> ValidarDataNascimento()
              │    ├──> ValidarCPF()
              │    └──> ValidarMatricula()
              │
              ├──> IRepositorioAluno.VerificarMatriculaDuplicada()
              ├──> IRepositorioAluno.VerificarCPFDuplicado()
              │
              ├──> Aluno aluno = new Aluno(...)
              │
              └──> IRepositorioAluno.Adicionar(aluno)
                   │
                   └──> RepositorioAlunoEmMemoria
                        └──> _alunos.Add(aluno)
```

### 4.2 Matrícula de Aluno

```
TerminalUI
    │
    └──> MatricularAluno()
         │
         ├──> Solicita matrícula do aluno
         ├──> ServicoCadastro.ObterAluno()
         │
         ├──> Exibe lista de disciplinas
         │
         └──> ServicoMatricula.MatricularAluno(aluno, disciplina)
              │
              ├──> Validar aluno e disciplina (null check)
              │
              ├──> IRepositorioMatricula.VerificarMatriculaEmDisciplina()
              │    └──> Impedir duplicidade
              │
              ├──> Matricula matricula = new Matricula(...)
              │
              ├──> aluno.Matriculas.Add(matricula)
              │
              └──> IRepositorioMatricula.Adicionar(matricula)
```

### 4.3 Atribuição de Nota

```
TerminalUI
    │
    └──> AtribuirNota()
         │
         ├──> Solicita matrícula do aluno
         ├──> ServicoCadastro.ObterAluno()
         │
         ├──> Exibe disciplinas do aluno
         │
         ├──> Solicita nota
         │
         └──> ServicoAvaliacao.AtribuirNota(aluno, disciplina, nota)
              │
              ├──> Validar aluno e disciplina (null check)
              │
              ├──> ValidadorAcademico.ValidarNota(nota)
              │    └──> nota deve estar entre 0 e 10
              │
              ├──> Verificar se já existe nota
              │    └──> Se existir, oferecer atualizar
              │
              ├──> Nota nota = new Nota(...)
              │
              └──> aluno.Notas.Add(nota)
                   │
                   └──> Média atualizada automaticamente
```

---

## 5. TRATAMENTO DE EXCEÇÕES

### 5.1 Estratégia de Exceções

```csharp
try
{
    // Operação arriscada
    ValidadorAcademico.ValidarCPF(cpf);           // ArgumentException
    if (repositorio.VerificarCPFDuplicado(cpf))
        throw new InvalidOperationException(...); // Regra de negócio
    var aluno = new Aluno(...);
    repositorio.Adicionar(aluno);
}
catch (ArgumentException ex)
{
    // Erro de validação (dados inválidos)
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"❌ Validação: {ex.Message}");
    Console.ResetColor();
}
catch (InvalidOperationException ex)
{
    // Erro de negócio (regra violada)
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"❌ Operação: {ex.Message}");
    Console.ResetColor();
}
catch (Exception ex)
{
    // Erro inesperado
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"❌ Erro inesperado: {ex.Message}");
    Console.ResetColor();
}
```

### 5.2 Tipos de Exceção

| Tipo | Caso de Uso | Exemplo |
|------|-----------|---------|
| `ArgumentException` | Argumento inválido | CPF com formato errado |
| `ArgumentNullException` | Argumento null | Aluno = null em MatricularAluno |
| `InvalidOperationException` | Operação viola negócio | Matrícula duplicada |
| `Exception` | Erro genérico | Problema inesperado |

---

## 6. BOAS PRÁTICAS IMPLEMENTADAS

### 6.1 SOLID Principles

| Princípio | Implementação |
|-----------|--------------|
| **S**ingle Responsibility | Cada classe tem 1 responsabilidade |
| **O**pen/Closed | Aberto para extensão, fechado para modificação |
| **L**iskov Substitution | Subclasses substituem classes base |
| **I**nterface Segregation | Interfaces específicas, não genéricas |
| **D**ependency Inversion | Depender de abstrações, não implementações |

### 6.2 Clean Code

```csharp
// ❌ Ruim
public void ValidaCPF(string c)
{
    string s = Regex.Replace(c, @"\D", "");
    if (s.Length != 11) throw new Exception("Invalid");
}

// ✅ Bom
public static void ValidarCPF(string cpf)
{
    if (string.IsNullOrWhiteSpace(cpf))
        throw new ArgumentException("CPF não pode ser vazio");

    string cpfLimpo = Regex.Replace(cpf, @"\D", "");

    if (cpfLimpo.Length != 11)
        throw new ArgumentException("CPF deve conter 11 dígitos");
}
```

### 6.3 Nomenclatura

```csharp
// ✅ Bom
- public bool VerificarMatriculaDuplicada(int matricula)
- private List<Aluno> _alunos;
- const double NOTA_MINIMA_APROVACAO = 6.0;
- public void ExibirMensagemBoasVindas()

// ❌ Ruim
- public bool vm(int m)
- private List<Aluno> a;
- const double min = 6.0;
- public void Exibir()
```

---

## 7. CONSIDERAÇÕES DE PERFORMANCE

### 7.1 Otimizações

```csharp
// Usar FirstOrDefault ao invés de Where().FirstOrDefault()
var aluno = _alunos.FirstOrDefault(a => a.Matricula == matricula);

// Evitar LINQ em métodos hot-path
public bool VerificarMatriculaDuplicada(int matricula)
{
    // O(n) no pior caso, mas simples em memória
    return _alunos.Any(a => a.Matricula == matricula);
}

// Cache de resultados se necessário
private Dictionary<int, Aluno> _cache = new Dictionary<int, Aluno>();

public Aluno ObterPorMatricula(int matricula)
{
    if (_cache.ContainsKey(matricula))
        return _cache[matricula];

    var aluno = _alunos.FirstOrDefault(a => a.Matricula == matricula);
    if (aluno != null)
        _cache[matricula] = aluno;

    return aluno;
}
```

---

## 8. CONCLUSÃO

A arquitetura implementada segue os princípios de **Layered Architecture** com aplicação de **padrões de design** reconhecidos, garantindo:

✅ Código limpo e manutenível  
✅ Fácil de testar  
✅ Escalável para futuras mudanças  
✅ Educacional para aprendizado de OOP  

---

**Versão:** 1.0  
