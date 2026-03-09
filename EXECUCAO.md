# 🚀 GUIA DE EXECUÇÃO E CONTRIBUIÇÃO

**Versão:** 1.0  

---

## 1. GUIA DE EXECUÇÃO

### 1.1 Pré-requisitos

Verifique se possui instalado:

```bash
# Verificar Visual Studio
- Visual Studio 2019 Community+ OU Visual Studio Code

# Verificar .NET Framework
- .NET Framework 4.7.2+

# Verificar C#
- C# 7.3+
```

### 1.2 Instalação Passo a Passo

#### **Opção 1: Visual Studio (Recomendado)**

```bash
# 1. Clone o repositório
git clone https://github.com/cleytonssilva/GestaoAcademica.git

# 2. Abra a solução
cd GestaoAcademica
start GestaoAcademica.sln

# 3. Aguarde o VS carregar e restaurar packages (se houver)

# 4. Compile (Ctrl + Shift + B)
# Verifique se compilou sem erros

# 5. Execute (F5 para debug ou Ctrl + F5 para sem debug)
```

#### **Opção 2: Linha de Comando**

```bash
# 1. Clone e navegue
git clone https://github.com/cleytonssilva/GestaoAcademica.git
cd GestaoAcademica/GestaoAcademica

# 2. Compile
dotnet build

# 3. Execute
dotnet run
```

### 1.3 Verificação de Execução

Se tudo estiver correto, você verá:

```
╔════════════════════════════════════════╗
║  SISTEMA DE GESTÃO ACADÊMICA v1.0      ║
║  Apresentação Acadêmica                ║
╚════════════════════════════════════════╝

═══════ MENU PRINCIPAL ═══════
1. Cadastrar Aluno
2. Cadastrar Professor
3. Cadastrar Disciplina
4. Matricular Aluno
5. Atribuir Nota
6. Listar Alunos Aprovados
7. Relatórios
8. Sair
══════════════════════════════

Opção: 
```

---

## 2. FLUXO DE USO PRÁTICO

### 2.1 Exemplo Completo: Criar e Aprovar um Aluno

```bash
# === PASSO 1: Cadastrar um Professor ===
Opção: 2
Nome Completo: João Silva
Email: joao.silva@escola.com
Data de Nascimento (dd/MM/yyyy): 15/05/1980
CPF (XXX.XXX.XXX-XX): 123.456.789-00
Disciplina Principal: Matemática
Salário (R$): 3500.00

✓ Professor João Silva cadastrado com sucesso!

# === PASSO 2: Cadastrar uma Disciplina ===
Opção: 3
Nome da Disciplina: Matemática I
Código (ex: MAT001): MAT001

--- Professores Disponíveis ---
1. João Silva - Matemática

Selecione o número: 1

✓ Disciplina Matemática I cadastrada com sucesso! (Código: MAT001)

# === PASSO 3: Cadastrar um Aluno ===
Opção: 1
Nome Completo: Maria Santos
Email: maria@aluno.com
Data de Nascimento (dd/MM/yyyy): 10/03/2005
CPF (XXX.XXX.XXX-XX): 987.654.321-00
Matrícula (número único): 2025001

✓ Aluno Maria Santos cadastrado com sucesso! (Matrícula: 2025001)

# === PASSO 4: Matricular Aluno em Disciplina ===
Opção: 4
Matrícula do Aluno: 2025001

--- Disciplinas Disponíveis ---
1. Matemática I (Código: MAT001)

Selecione o número: 1

✓ Aluno Maria Santos matriculado em Matemática I!

# === PASSO 5: Atribuir Nota ===
Opção: 5
Matrícula do Aluno: 2025001

--- Disciplinas do Aluno ---
1. Matemática I

Selecione o número: 1
Nota em Matemática I (0 a 10): 8.5

✓ Nota 8.5 atribuída com sucesso!
Situação Atual: APROVADO

# === PASSO 6: Listar Alunos Aprovados ===
Opção: 6

═══════ ALUNOS APROVADOS ═══════

Total de Alunos Aprovados: 1

┌────────────────────────────────────────────────┐
│ Maria Santos                    Média: 8.50    │
└────────────────────────────────────────────────┘

# === PASSO 7: Gerar Relatório ===
Opção: 7
1 (Relatório de Aluno)
Matrícula do Aluno: 2025001

=== RELATÓRIO ACADÊMICO ===
Aluno: Maria Santos
Matrícula: 2025001
Email: maria@aluno.com
CPF: 987.654.321-00
Total de Notas: 1
Média Geral: 8.50
Situação: APROVADO

--- Notas por Disciplina ---
  Matemática I: 8.50 (APROVADO)
```

---

## 3. SOLUÇÃO DE PROBLEMAS

### 3.1 Erro: "Project file not found"

```bash
# Certifique-se de estar no diretório correto
cd GestaoAcademica

# Verifique se vê GestaoAcademica.csproj
dir *.csproj

# Se não estiver no diretório correto:
cd GestaoAcademica  # Pasta interna
```

### 3.2 Erro: "CS1061: Tipo não contém definição"

```bash
# Pode haver referencias faltando. Reconstrua:
dotnet clean
dotnet build
```

### 3.3 Erro: "objeto bloqueado por outro processo"

```bash
# Encerre a aplicação e limpe:
dotnet clean
dotnet build
dotnet run
```

### 3.4 Erro de Validação: "CPF inválido"

```
O sistema usa validação real de CPF. Exemplos válidos:
❌ 111.111.111-11 (inválido)
✅ 123.456.789-09 (válido)

Para testes rápidos, use CPFs que passem na validação
```

---

## 4. ESTRUTURA DE PASTAS

```
GestaoAcademica/
├── GestaoAcademica/              # Projeto principal
│   ├── Dominio/                  # Modelos de dados
│   │   ├── Pessoa.cs
│   │   ├── Aluno.cs
│   │   ├── Professor.cs
│   │   ├── Disciplina.cs
│   │   ├── Matricula.cs
│   │   └── Nota.cs
│   │
│   ├── Dados/                    # Camada de dados
│   │   └── Repositorio.cs
│   │
│   ├── Servicos/                 # Lógica de negócio
│   │   ├── ServicoCadastro.cs
│   │   ├── ServicoMatricula.cs
│   │   ├── ServicoAvaliacao.cs
│   │   ├── ServicoRelatorio.cs
│   │   └── ValidadorAcademico.cs
│   │
│   ├── UI/                       # Interface com usuário
│   │   └── TerminalUI.cs
│   │
│   ├── Program.cs                # Ponto de entrada
│   ├── GestaoAcademica.csproj   # Configuração do projeto
│   └── Properties/
│
├── README.md                     # Documentação principal
├── REQUISITOS.md                 # Especificação de requisitos
├── ARQUITETURA.md                # Documentação técnica
├── EXECUCAO.md                   # Este arquivo
└── .gitignore                    # Arquivos a ignorar no git
```

---

## 5. CONTRIBUINDO PARA O PROJETO

### 5.1 Passos para Contribuir

```bash
# 1. Faça um Fork do projeto
# Clique em "Fork" no GitHub

# 2. Clone seu fork
git clone https://github.com/SEU_USUARIO/GestaoAcademica.git
cd GestaoAcademica

# 3. Crie uma branch para sua feature
git checkout -b feature/nova-funcionalidade

# 4. Faça as mudanças
# ... edite os arquivos ...

# 5. Commit suas mudanças
git add .
git commit -m "Adiciona nova funcionalidade"

# Mensagens de commit devem ser descritivas:
# ✅ "Adiciona validação de email melhorada"
# ❌ "Fix bug"

# 6. Push para sua branch
git push origin feature/nova-funcionalidade

# 7. Abra um Pull Request
# No GitHub, clique em "Compare & pull request"

# 8. Descreva suas mudanças no PR
```

### 5.2 Padrões de Código

#### **Nomenclatura**

```csharp
// Classes: PascalCase
public class RepositorioAluno { }

// Métodos: PascalCase
public void CadastrarAluno() { }

// Propriedades: PascalCase
public int Matricula { get; set; }

// Variáveis locais: camelCase
int matricula = 2025001;

// Constantes: UPPER_CASE
const double NOTA_MINIMA = 6.0;

// Campos privados: _camelCase
private List<Aluno> _alunos;
```

#### **Comentários**

```csharp
/// <summary>
/// Cadastra um novo aluno no sistema
/// </summary>
/// <param name="nome">Nome completo do aluno</param>
/// <param name="email">Email válido</param>
/// <returns>Aluno cadastrado com sucesso</returns>
/// <exception cref="ArgumentException">Se dados inválidos</exception>
public Aluno CadastrarAluno(string nome, string email)
{
    // Validar entrada
    ValidadorAcademico.ValidarNome(nome);
    
    // Criar objeto
    var aluno = new Aluno(nome, email);
    
    // Persistir
    _repositorio.Adicionar(aluno);
    
    return aluno;
}
```

#### **Tratamento de Exceção**

```csharp
try
{
    // Operação
    ValidadorAcademico.ValidarCPF(cpf);
}
catch (ArgumentException ex)
{
    // Tratamento específico
    Console.WriteLine($"Erro: {ex.Message}");
}
catch (Exception ex)
{
    // Tratamento genérico
    Console.WriteLine($"Erro inesperado: {ex.Message}");
}
```

### 5.3 Checklist Antes de Fazer PR

- ✅ Código compila sem erros
- ✅ Segue padrão de nomenclatura
- ✅ Tem comentários em métodos públicos
- ✅ Trata exceções apropriadamente
- ✅ Mensagens de commit são descritivas
- ✅ Não há código comentado (remova ou explique)
- ✅ Máximo 30 linhas por método
- ✅ Nomes significativos para variáveis

### 5.4 Tipos de Contribuição Bem-vindos

```
✅ Correções de bugs
✅ Melhorias de performance
✅ Documentação melhorada
✅ Novos relatórios
✅ Testes unitários
✅ Traduções
✅ Exemplos de uso

❌ Mudanças arquiteturais sem discussão
❌ Dependências externas não aprovadas
❌ Código sem documentação
```

---

## 6. TESTES MANUAIS

### 6.1 Teste de Validação de CPF

```
Entradas para testar:

✅ Válidos:
- 123.456.789-09
- 987.654.321-00

❌ Inválidos:
- 111.111.111-11
- 123.456.789-00
- abc.def.ghi-jk
- Vazio
```

### 6.2 Teste de Validação de Idade

```
Entradas para testar:

✅ Válidos:
- 01/01/2005 (19 anos) - Aceita
- 15/12/1980 (44 anos) - Aceita

❌ Inválidos:
- 01/01/2010 (14 anos) - Rejeita
- 25/12/2024 (dias atrás) - Rejeita
```

### 6.3 Teste de Matrícula Duplicada

```
Passos:

1. Cadastrar Aluno 1: Maria, Matrícula 2025001
2. Tentar Cadastrar Aluno 2: João, Matrícula 2025001
3. Esperado: Erro "Matrícula já existe"
```

---

## 7. DÚVIDAS E SUPORTE

### 7.1 Onde Pedir Ajuda

1. **Issues no GitHub**: Abra uma issue descrevendo o problema
2. **Discussões**: Use a aba "Discussions" para dúvidas
3. **Email**: Contate através do email do repositório

### 7.2 Formato para Relatar Bug

```markdown
## Descrição do Bug
[Descrição clara do que aconteceu]

## Passos para Reproduzir
1. [Passo 1]
2. [Passo 2]
3. [Passo 3]

## Comportamento Esperado
[O que deveria ter acontecido]

## Comportamento Atual
[O que realmente aconteceu]

## Ambiente
- OS: Windows/Linux/macOS
- Visual Studio: versão
- .NET Framework: versão

## Logs
[Cole logs relevantes aqui]
```

---

## 8. ROADMAP FUTURO

```
v1.0 (Atual) ✅
└── Funcionalidades básicas

v1.1 (Planejado)
├── Persistência em banco de dados
├── Autenticação de usuários
└── Relatórios em PDF

v2.0 (Futuro)
├── API REST
├── Interface Web
└── App Mobile
```

---

## 9. REFERÊNCIAS ÚTEIS

### Documentação
- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET Framework Guide](https://docs.microsoft.com/en-us/dotnet/framework/)

### Ferramentas
- [Visual Studio Download](https://visualstudio.microsoft.com/)
- [Git Documentation](https://git-scm.com/doc)

### Padrões e Boas Práticas
- [Microsoft Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/)
- [Clean Code](https://www.oreilly.com/library/view/clean-code-a/9780136083238/)

---
  
**Mantido por:** [@cleytonssilva](https://github.com/cleytonssilva)
