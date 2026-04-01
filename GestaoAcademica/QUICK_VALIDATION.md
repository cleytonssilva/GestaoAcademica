# 🚀 GUIA RÁPIDO - Execute em 5 Minutos

## ⏱️ 5 Passos Rápidos

### 1. Abra MySQL Workbench
```
Aplicativo: MySQL Workbench
Conecte-se ao servidor localhost
```

### 2. Execute Script 1 (Criar Banco)
```
File > Open SQL Script
→ GestaoAcademica\Scripts\01-CreateDatabase.sql
Ctrl+Shift+Enter
```

### 3. Execute Script 2 (Views)
```
File > Open SQL Script
→ GestaoAcademica\Scripts\02-CreateViews.sql
Ctrl+Shift+Enter
```

### 4. Execute Script 3 (Dados)
```
File > Open SQL Script
→ GestaoAcademica\Scripts\03-InsertSampleData.sql
Ctrl+Shift+Enter
```

### 5. Validar (Execute esta query)
```sql
SELECT COUNT(*) as 'Total Registros' FROM pessoas;
-- Resultado esperado: 7
```

---

## ✅ Checklist de Validação

- [ ] Banco `SistemaBDGestaoAcademica` foi criado
- [ ] 7 tabelas foram criadas (pessoas, alunos, professores, disciplinas, turmas, matriculas, avaliacoes)
- [ ] 4 views foram criadas
- [ ] 7 pessoas foram inseridas
- [ ] 4 alunos registrados
- [ ] 3 professores registrados
- [ ] 3 disciplinas criadas
- [ ] 3 turmas criadas
- [ ] 7 matrículas ativas
- [ ] 7 avaliações registradas

---

## 🔗 Arquivos de Referência

| Arquivo | Descrição |
|---------|-----------|
| `Scripts\01-CreateDatabase.sql` | Criar banco e tabelas |
| `Scripts\02-CreateViews.sql` | Criar views e procedures |
| `Scripts\03-InsertSampleData.sql` | Inserir dados de exemplo |
| `App.config` | Connection string configurada ✓ |
| `SETUP_DATABASE.md` | Documentação detalhada |
| `TESTE_VALIDACAO_BANCO.md` | Este guia completo |

---

## 🎯 Resultado Final Esperado

```
SistemaBDGestaoAcademica
├── Tabelas (7)
│   ├── pessoas: 7 registros
│   ├── alunos: 4 registros
│   ├── professores: 3 registros
│   ├── disciplinas: 3 registros
│   ├── turmas: 3 registros
│   ├── matriculas: 7 registros
│   └── avaliacoes: 7 registros
├── Views (4)
│   ├── v_alunos
│   ├── v_professores
│   ├── v_disciplinas_professor
│   └── v_media_alunos
└── Procedures (2)
    ├── sp_calcular_media_aluno
    └── sp_alunos_aprovados_disciplina
```

---

## 🧪 Queries de Teste Rápido

```sql
-- 1. Verificar se banco existe
SHOW DATABASES LIKE 'SistemaBDGestaoAcademica';

-- 2. Contar alunos
SELECT COUNT(*) as alunos FROM alunos;

-- 3. Contar avaliações
SELECT COUNT(*) as avaliacoes FROM avaliacoes;

-- 4. Listar alunos aprovados
SELECT aluno_nome, media FROM v_media_alunos WHERE situacao = 'APROVADO';

-- 5. Média geral de Cálculo
SELECT AVG(media) as media_disciplina FROM v_media_alunos WHERE disciplina_nome = 'Cálculo I';
```

---

## ⚠️ Erros Comuns

| Erro | Solução |
|------|---------|
| `Access Denied` | Verifique senha em `App.config` |
| `Database doesn't exist` | Execute `01-CreateDatabase.sql` |
| `Table doesn't exist` | Confirme que os 3 scripts foram executados |
| `Duplicate key error` | Limpe dados: `DELETE FROM avaliacoes;` (depois views) |

---

**Tempo estimado: 5-10 minutos**
**Pronto para usar! ✨**
