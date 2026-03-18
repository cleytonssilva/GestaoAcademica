# 🚀 Quick Start - MySQL Persistence

## ✅ Status Atual
- **Compilação**: ✅ Sucesso
- **Código**: ✅ Pronto
- **MySQL**: ⏳ Opcional (sistema funciona com in-memory)

## 📋 Checklist de Instalação (5-10 minutos)

### Passo 1: Instalar MySQL Server (2 min)
```bash
# Windows: Download em https://dev.mysql.com/downloads/mysql/
# macOS: brew install mysql
# Linux: sudo apt-get install mysql-server
```

### Passo 2: Criar Banco de Dados (1 min)
```bash
mysql -u root -p < "GestaoAcademica/BD_Scripts/01_create_database.sql"
```

### Passo 3: Instalar Pacotes NuGet (2 min)
**Option A - Visual Studio Package Manager:**
```
Tools → NuGet Package Manager → Package Manager Console

Install-Package MySql.Data -Version 8.0.31
Install-Package EntityFramework -Version 6.4.4
Install-Package MySql.Data.EntityFramework -Version 8.0.31
```

**Option B - .NET CLI:**
```bash
cd GestaoAcademica
dotnet add package MySql.Data
dotnet add package EntityFramework
dotnet add package MySql.Data.EntityFramework
```

### Passo 4: Compilar e Executar (1 min)
```bash
dotnet build
dotnet run
```

## 📝 Arquivos Modificados

```
GestaoAcademica/
├── Dominio/
│   ├── Aluno.cs (✏️ Atualizado)
│   ├── Avaliacao.cs (🆕 Novo)
│   ├── Disciplina.cs (✏️ Atualizado)
│   ├── Matricula.cs (✏️ Atualizado)
│   ├── Pessoa.cs (✏️ Atualizado)
│   ├── Professor.cs (✏️ Atualizado)
│   ├── Turma.cs (🆕 Novo)
│   └── Nota.cs (✏️ Mantém compatibilidade)
├── Dados/
│   ├── ConexaoBancoDados.cs (🆕 Novo)
│   ├── GestaoAcademicaContext.cs (🆕 Novo)
│   ├── Repositorio.cs (ℹ️ Sem mudanças)
│   ├── RepositorioAlunoMySQL.cs (🆕 Novo)
│   ├── RepositorioProfessorMySQL.cs (🆕 Novo)
│   ├── RepositorioDisciplinaMySQL.cs (🆕 Novo)
│   └── RepositorioMatriculaMySQL.cs (🆕 Novo)
├── Servicos/
│   ├── ServicoCadastro.cs (ℹ️ Sem mudanças)
│   ├── ServicoMatricula.cs (ℹ️ Sem mudanças)
│   └── ... (ℹ️ Compatibilidade mantida)
├── App.config (✏️ Atualizado)
├── packages.config (🆕 Novo)
└── Program.cs (✏️ Atualizado)

BD_Scripts/
└── 01_create_database.sql (🆕 Novo)

Documentação/
├── IMPLEMENTACAO_MYSQL.md (🆕 Novo)
└── PERSISTENCIA_MYSQL_COMPLETA.md (🆕 Novo)
```

## 🔄 Funcionamento Automático

```csharp
// Program.cs - Lógica automática
try
{
    var context = new GestaoAcademicaContext();
    var repositorio = new RepositorioAlunoMySQL(context);
    // ✅ Usando MySQL
}
catch (Exception ex)
{
    var repositorio = new RepositorioAlunoEmMemoria();
    // ✓ Fallback para in-memory (dados não persistem)
}
```

## 📊 Banco de Dados

### Tabelas Criadas
```
pessoas
├── alunos
├── professores
├── disciplinas
├── turmas
├── matriculas
└── avaliacoes
```

### Connection String
```xml
<!-- App.config -->
<connectionStrings>
  <add name="GestaoAcademica" 
       connectionString="Server=localhost;Database=gestao_academica;User=root;Password=;" />
</connectionStrings>
```

**Altere conforme necessário:**
- `Server`: localhost (ou IP do servidor MySQL)
- `Database`: gestao_academica (nome do banco)
- `User`: root (seu usuário MySQL)
- `Password`: (sua senha)

## 🧪 Teste Sem MySQL

O sistema funciona completamente **sem MySQL instalado**:

```bash
dotnet run

# Funcionará com dados em memória
# Nenhum erro - apenas modo offline
```

## 💡 Próximas Etapas

### Se Quiser Ativar MySQL:
1. Instale MySQL Server
2. Execute `01_create_database.sql`
3. Instale pacotes NuGet
4. Atualize connection string em `App.config`
5. Recompile e execute

### Se Preferir In-Memory Agora:
- Deixe como está
- Sistema funcionará normalmente
- Dados se perdem ao fechar app
- Ideal para testes

## 🐛 Troubleshooting

### Erro: "MySql.Data não encontrado"
```
✓ Normal se pacote não está instalado
✓ Sistema usa fallback in-memory
✓ Instale pacotes quando precisar
```

### Erro: "Conexão recusada"
```
✓ MySQL não está rodando
✓ Verifique string de conexão em App.config
✓ Verifique se banco foi criado
```

### Erro: "Banco não existe"
```
✓ Execute 01_create_database.sql:
  mysql -u root -p < 01_create_database.sql
✓ Verifique nome do banco em App.config
```

## 📚 Documentação

| Arquivo | Conteúdo |
|---------|----------|
| **PERSISTENCIA_MYSQL_COMPLETA.md** | Implementação detalhada |
| **IMPLEMENTACAO_MYSQL.md** | Guia de setup |
| **README.md** | Visão geral do projeto |
| **ARQUITETURA.md** | Padrões de design |

## ⚡ Comandos Úteis

```bash
# Compilar
dotnet build

# Executar
dotnet run

# Limpar build
dotnet clean

# Restaurar pacotes
dotnet restore
```

## 📞 Suporte

**Documentação Local:**
```
/GestaoAcademica/
├── PERSISTENCIA_MYSQL_COMPLETA.md ← Completo
├── IMPLEMENTACAO_MYSQL.md ← Setup
├── ARQUITETURA.md ← Design
└── README.md ← Overview
```

---

**Versão:** 1.0.0  
**Status:** ✅ Pronto  
**Data:** Março 2026

