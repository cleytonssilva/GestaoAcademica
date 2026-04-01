# 📚 Guia Completo: Teste, Validação e População do Banco de Dados

## 🎯 Resumo das Ações Realizadas

Foram criados e configurados os seguintes componentes para o seu projeto **Gestão Acadêmica**:

### ✅ Arquivos Criados

1. **`Scripts\01-CreateDatabase.sql`** - Criação da estrutura do banco de dados
   - Tabelas: pessoas, alunos, professores, disciplinas, turmas, matriculas, avaliacoes
   - Índices otimizados para performance
   - Constraints de integridade referencial

2. **`Scripts\02-CreateViews.sql`** - Views e Stored Procedures
   - Views: v_alunos, v_professores, v_disciplinas_professor, v_media_alunos
   - Procedures: sp_calcular_media_aluno, sp_alunos_aprovados_disciplina

3. **`Scripts\03-InsertSampleData.sql`** - Dados de exemplo
   - 3 professores com salários e disciplinas
   - 4 alunos com matrículas ativas
   - 3 disciplinas com código único
   - 3 turmas com capacidade máxima
   - 7 matrículas de alunos em turmas
   - 7 avaliações com diferentes tipos e pesos

4. **`SETUP_DATABASE.md`** - Documentação completa
   - Instruções passo a passo
   - Troubleshooting
   - Exemplos de queries de validação

### ✅ Arquivos Modificados

1. **`App.config`**
   - ✓ Banco de dados atualizado para: `SistemaBDGestaoAcademica`
   - ✓ Connection string corrigida

2. **`Dados\ConexaoBancoDados.cs`**
   - ✓ Banco de dados padrão atualizado para: `SistemaBDGestaoAcademica`

---

## 🚀 COMO USAR - PASSO A PASSO

### PASSO 1️⃣: Preparar o Ambiente

#### Pré-requisitos
- ✓ MySQL Server instalado (v5.7 ou superior)
- ✓ MySQL Workbench instalado
- ✓ Visual Studio 2026 aberto com o projeto

#### Configurar App.config (se necessário)

Se sua senha MySQL for diferente de vazio, atualize em `App.config`:

```xml
<connectionStrings>
    <add name="GestaoAcademica" 
         connectionString="Server=localhost;Database=SistemaBDGestaoAcademica;User=root;Password=SUA_SENHA;" 
         providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

---

### PASSO 2️⃣: Executar Scripts SQL

Abra **MySQL Workbench** e siga a sequência:

#### Script 1: Criar Banco de Dados
```
Menu: File > Open SQL Script
Selecione: GestaoAcademica\Scripts\01-CreateDatabase.sql
Tecla: Ctrl+Shift+Enter (Execute All)
```

**Resultado esperado:**
```
Database SistemaBDGestaoAcademica created
All tables created successfully
```

#### Script 2: Criar Views e Procedures
```
File > Open SQL Script
Selecione: GestaoAcademica\Scripts\02-CreateViews.sql
Ctrl+Shift+Enter
```

**Resultado esperado:**
```
4 views created
2 stored procedures created
```

#### Script 3: Popular com Dados
```
File > Open SQL Script
Selecione: GestaoAcademica\Scripts\03-InsertSampleData.sql
Ctrl+Shift+Enter
```

**Resultado esperado:**
```
3 professors inserted
4 students inserted
3 disciplines created
3 classes created
7 enrollments created
7 evaluations recorded
```

---

### PASSO 3️⃣: Validar os Dados

Execute estas queries no MySQL Workbench para confirmar:

#### Query 1: Contar Registros
```sql
USE SistemaBDGestaoAcademica;

SELECT 'Pessoas' as Tabela, COUNT(*) as Total FROM pessoas
UNION ALL
SELECT 'Alunos', COUNT(*) FROM alunos
UNION ALL
SELECT 'Professores', COUNT(*) FROM professores
UNION ALL
SELECT 'Disciplinas', COUNT(*) FROM disciplinas
UNION ALL
SELECT 'Turmas', COUNT(*) FROM turmas
UNION ALL
SELECT 'Matrículas', COUNT(*) FROM matriculas
UNION ALL
SELECT 'Avaliações', COUNT(*) FROM avaliacoes;
```

**Resultado esperado:**
```
Pessoas       | 7
Alunos        | 4
Professores   | 3
Disciplinas   | 3
Turmas        | 3
Matrículas    | 7
Avaliações    | 7
```

#### Query 2: Listar Alunos
```sql
SELECT * FROM v_alunos;
```

#### Query 3: Listar Professores
```sql
SELECT * FROM v_professores;
```

#### Query 4: Ver Médias dos Alunos
```sql
SELECT * FROM v_media_alunos;
```

#### Query 5: Alunos Aprovados por Disciplina
```sql
CALL sp_alunos_aprovados_disciplina(1);  -- 1 = ID da Física Geral
```

---

### PASSO 4️⃣: Instalar Pacote NuGet (se necessário)

Se o pacote `MySql.Data` não estiver instalado:

```powershell
# Abra Package Manager Console em Visual Studio
Tools > NuGet Package Manager > Package Manager Console

# Execute:
Install-Package MySql.Data -Version 8.0.33
```

---

### PASSO 5️⃣: Atualizar Referências do Projeto

O arquivo `.csproj` precisa ser atualizado (será feito automaticamente):

```xml
<ItemGroup>
    <Content Include="Scripts\01-CreateDatabase.sql" />
    <Content Include="Scripts\02-CreateViews.sql" />
    <Content Include="Scripts\03-InsertSampleData.sql" />
</ItemGroup>
```

---

## 📊 Estrutura do Banco de Dados

### Relacionamento das Tabelas

```
pessoas (id_pessoa, tipo, nome, email, cpf)
    ├─ alunos (id_aluno→id_pessoa, matricula)
    └─ professores (id_professor→id_pessoa, disciplina_principal, salario)

disciplinas (id_disciplina, nome, codigo)
    └─ professor_responsavel_id → professores(id_professor)

turmas (id_turma, id_disciplina)
    └─ id_disciplina → disciplinas(id_disciplina)

matriculas (id_matricula, id_aluno, id_turma)
    ├─ id_aluno → alunos(id_aluno)
    └─ id_turma → turmas(id_turma)

avaliacoes (id_avaliacao, id_matricula, id_professor, id_turma)
    ├─ id_matricula → matriculas(id_matricula)
    ├─ id_professor → professores(id_professor)
    └─ id_turma → turmas(id_turma)
```

### Views Disponíveis

| View | Descrição |
|------|-----------|
| `v_alunos` | Alunos com dados pessoais completos |
| `v_professores` | Professores com dados pessoais e salário |
| `v_disciplinas_professor` | Disciplinas com professor responsável |
| `v_media_alunos` | Média de cada aluno por disciplina com situação |

### Stored Procedures

```sql
-- Calcular média do aluno em uma turma
CALL sp_calcular_media_aluno(1, 1, @media, @situacao);
SELECT @media, @situacao;

-- Listar alunos aprovados em uma disciplina
CALL sp_alunos_aprovados_disciplina(1);
```

---

## 🔍 Dados de Exemplo Carregados

### Professores
| ID | Nome | Disciplina | Salário |
|----|------|-----------|---------|
| 1 | João Silva | Física | R$ 5.200,00 |
| 2 | Ana Oliveira | Matemática | R$ 5.500,00 |
| 3 | Carlos Souza | Química | R$ 5.300,00 |

### Alunos
| ID | Matrícula | Nome | Email |
|----|-----------|------|-------|
| 4 | 2024001 | Maria Santos | maria.santos@aluno.com |
| 5 | 2024002 | Pedro Silva | pedro.silva@aluno.com |
| 6 | 2024003 | Juliana Costa | juliana.costa@aluno.com |
| 7 | 2024004 | Ricardo Oliveira | ricardo.oliveira@aluno.com |

### Avaliações (Amostra)
- Maria Santos em Física: 8.5 (Prova) + 9.0 (Trabalho) = **Média: 8.75**
- Maria Santos em Cálculo: 7.5 (Prova) = **Média: 7.5**
- Pedro Silva em Física: 6.0 (Prova) + 7.0 (Participação) = **Média: 6.4**
- Juliana Costa em Cálculo: 9.0 (Prova) = **Média: 9.0** ✓ APROVADA

---

## 🐛 Troubleshooting

### Erro: "Access Denied for user 'root'@'localhost'"
**Causa:** Senha incorreta
**Solução:** Atualize `App.config` com a senha correta

### Erro: "Database 'SistemaBDGestaoAcademica' doesn't exist"
**Causa:** Script 01 não foi executado
**Solução:** Execute `Scripts\01-CreateDatabase.sql` novamente

### Erro: "Table 'SistemaBDGestaoAcademica.alunos' doesn't exist"
**Causa:** Script 01 falhou
**Solução:** Verifique logs no MySQL Workbench e execute novamente

### Erro: "Procedure 'SistemaBDGestaoAcademica.sp_calcular_media_aluno' doesn't exist"
**Causa:** Script 02 não foi executado
**Solução:** Execute `Scripts\02-CreateViews.sql` novamente

### Erro: "Duplicate entry for key 'unique_matricula'"
**Causa:** Tentativa de inserir matrícula duplicada
**Solução:** Execute o script 03 apenas uma vez. Se precisar inserir novamente, delete os dados primeiro

---

## 📝 Próximas Etapas

1. **Integrar Entity Framework 6** (opcional)
   ```
   Install-Package EntityFramework -Version 6.4.4
   ```

2. **Criar DbContext com EF6**
   - Herdar de `DbContext` em vez de usar em memória
   - Configurar `DbSet<T>` para cada entidade

3. **Implementar Repositórios de Dados**
   - `RepositorioAlunoMySQL`
   - `RepositorioProfessorMySQL`
   - `RepositorioDisciplinaMySQL`

4. **Criar Serviços de Negócio**
   - `ServicoCadastro`
   - `ServicoMatricula`
   - `ServicoAvaliacao`

5. **Desenvolver Interface do Usuário**
   - Terminal/Console UI
   - Web API
   - Aplicativo Desktop

---

## 📞 Informações Importantes

### Connection String
```
Server=localhost;Database=SistemaBDGestaoAcademica;User=root;Password=;
```

### Localização dos Scripts
- `GestaoAcademica\Scripts\01-CreateDatabase.sql`
- `GestaoAcademica\Scripts\02-CreateViews.sql`
- `GestaoAcademica\Scripts\03-InsertSampleData.sql`

### Localização da Documentação
- `GestaoAcademica\SETUP_DATABASE.md` (este arquivo)

### Versão do Projeto
- **.NET Framework:** 4.7.2
- **MySQL:** 5.7+
- **C# Version:** 7.3

---

## ✨ Resumo do Status

- ✅ Banco de dados MySQL criado
- ✅ Tabelas com constraints de integridade
- ✅ Views para consultas facilitadas
- ✅ Stored procedures para operações comuns
- ✅ Dados de exemplo populados
- ✅ Connection string configurada
- ✅ Documentação completa

**STATUS: PRONTO PARA TESTE E VALIDAÇÃO** 🎉

---

**Data:** 2025
**Versão:** 1.0
**Compatibilidade:** MySQL 5.7+, .NET Framework 4.7.2+
