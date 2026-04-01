# Guia de Configuração: Banco de Dados MySQL - SistemaBDGestaoAcademica

## 📋 Pré-requisitos

Antes de prosseguir, certifique-se de que você possui:

1. **MySQL Server instalado** (versão 5.7 ou superior)
2. **MySQL Workbench** ou outro cliente MySQL instalado
3. **Acesso ao usuário root do MySQL** (ou outro usuário com privilégios de criação de banco de dados)
4. **Visual Studio 2026** com o projeto `GestaoAcademica` aberto

## 🔧 Passo 1: Criar o Banco de Dados

### Opção A: Usando MySQL Workbench

1. Abra o **MySQL Workbench**
2. Conecte-se ao seu servidor MySQL
3. Abra uma nova query (Ctrl+T)
4. Abra o arquivo: `GestaoAcademica\Scripts\01-CreateDatabase.sql`
5. Execute o script completo (Ctrl+Shift+Enter)
6. Verifique se o banco `SistemaBDGestaoAcademica` foi criado

### Opção B: Usando linha de comando

```powershell
# Abra PowerShell e execute:
mysql -u root -p < "C:\Users\011.215094\source\repos\cleytonssilva\GestaoAcademica\GestaoAcademica\Scripts\01-CreateDatabase.sql"

# Digite a senha do MySQL quando solicitado (pressione Enter se não houver senha)
```

### Opção C: Usando Docker (se disponível)

```powershell
# Se usar MySQL em Docker:
docker exec -i <container-name> mysql -u root -p < script.sql
```

## 🎯 Passo 2: Criar Views e Stored Procedures

1. No **MySQL Workbench**, abra uma nova query
2. Abra o arquivo: `GestaoAcademica\Scripts\02-CreateViews.sql`
3. Execute o script (Ctrl+Shift+Enter)

**Validação:** Você deve ver as views criadas em `GestaoAcademica.Views` no Workbench

## 📊 Passo 3: Popular o Banco com Dados de Exemplo

1. No **MySQL Workbench**, abra uma nova query
2. Abra o arquivo: `GestaoAcademica\Scripts\03-InsertSampleData.sql`
3. Execute o script (Ctrl+Shift+Enter)

**Resultado esperado:**
- 3 professores inseridos
- 4 alunos inseridos
- 3 disciplinas criadas
- 3 turmas criadas
- 7 matrículas criadas
- 7 avaliações registradas

## ✅ Passo 4: Validar a Conexão do C#

### 4.1 Atualizar App.config (já feito)

O arquivo `App.config` foi atualizado com a connection string correta:
```xml
<add name="GestaoAcademica" 
     connectionString="Server=localhost;Database=SistemaBDGestaoAcademica;User=root;Password=;" 
     providerName="MySql.Data.MySqlClient" />
```

**Se sua senha do MySQL for diferente, atualize o campo `Password=`**

### 4.2 Instalar MySql.Data (se necessário)

Se o pacote MySQL.Data não estiver instalado:

```powershell
# Abra o Package Manager Console no Visual Studio
# Execute:
Install-Package MySql.Data -Version 8.0.33
```

### 4.3 Testar a Conexão

Crie um pequeno teste em `Program.cs`:

```csharp
using GestaoAcademica.Dados;
using System;

namespace GestaoAcademica
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var context = new GestaoAcademicaContext();
                string connString = context.GetConnectionString();
                
                Console.WriteLine("✓ Conexão configurada com sucesso!");
                Console.WriteLine($"Banco de Dados: SistemaBDGestaoAcademica");
                Console.WriteLine($"Connection String: {connString}");
                Console.WriteLine("\nPressione qualquer tecla para sair...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erro na conexão: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
```

## 🔍 Passo 5: Executar Consultas de Validação

No **MySQL Workbench**, execute as seguintes queries para validar:

### Contar registros
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

### Listar alunos com suas notas
```sql
SELECT * FROM v_media_alunos;
```

### Listar alunos aprovados por disciplina
```sql
CALL sp_alunos_aprovados_disciplina(1);  -- 1 = ID da disciplina (Física Geral)
```

## 🐛 Troubleshooting

### Problema: "Access Denied for user 'root'@'localhost'"
**Solução:** Verifique a senha no `App.config`. Se não houver senha, deixe em branco: `Password=;`

### Problema: "Database 'SistemaBDGestaoAcademica' doesn't exist"
**Solução:** Execute o script `01-CreateDatabase.sql` novamente

### Problema: "The table doesn't exist"
**Solução:** Certifique-se de que todos os três scripts foram executados na ordem correta

### Problema: Erro de violação de constraint
**Solução:** Execute `03-InsertSampleData.sql` apenas após criar as tabelas e views

## 📝 Estrutura do Banco de Dados

### Tabelas Principais
- **pessoas**: Tabela base com dados comuns de alunos e professores
- **alunos**: Dados específicos de alunos (matrícula, data)
- **professores**: Dados específicos de professores (disciplina, salário)
- **disciplinas**: Disciplinas oferecidas pela instituição
- **turmas**: Turmas específicas de cada disciplina
- **matriculas**: Relação aluno-turma com status
- **avaliacoes**: Notas/avaliações dos alunos

### Views Disponíveis
- `v_alunos`: Alunos com dados pessoais
- `v_professores`: Professores com dados pessoais
- `v_disciplinas_professor`: Disciplinas com professor responsável
- `v_media_alunos`: Média de alunos por disciplina com situação (APROVADO/RECUPERACAO/REPROVADO)

### Stored Procedures
- `sp_calcular_media_aluno(id_aluno, id_turma)`: Calcula a média do aluno
- `sp_alunos_aprovados_disciplina(id_disciplina)`: Lista alunos aprovados

## 🚀 Próximas Etapas

1. Integrar Entity Framework 6 para ORM (opcional)
2. Criar repositórios de dados para cada entidade
3. Implementar serviços de negócio
4. Criar interface de usuário

## 📞 Suporte

Se encontrar problemas:
1. Verifique os logs em `App.config`
2. Confirme que o MySQL está rodando
3. Valide as permissões do usuário MySQL
4. Verifique se as portas estão abertas (padrão: 3306)

---
**Última atualização:** 2025
**Versão do Banco:** 1.0
