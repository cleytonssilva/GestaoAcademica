# 🧪 GUIA COMPLETO: VALIDAÇÃO E TESTE DO SISTEMA

## 📊 STATUS ATUAL

```
✅ Compilação: BEM-SUCEDIDA
✅ Program.cs: Agora chama TerminalUI.Executar()
✅ Sistema pronto para testes
```

---

## 🎯 COMO TESTAR AGORA (5 PASSOS)

### Passo 1: Compilar
```
Ctrl + Shift + B
Resultado esperado: ✅ Build succeeded
```

### Passo 2: Executar
```
F5 (Start Debugging)
ou
Ctrl + F5 (Run without debugging)
```

### Passo 3: Você verá o Menu
```
=== MENU PRINCIPAL ===
1. Cadastrar Aluno
2. Cadastrar Professor
3. Cadastrar Disciplina
4. Matricular Aluno
5. Atribuir Nota
6. Ver Relatório
7. Ver Estatísticas
0. Sair

Digite sua escolha:
```

### Passo 4: Teste Cada Funcionalidade
Veja a seção "CASOS DE TESTE" abaixo

### Passo 5: Valide os Resultados
Compare com as respostas esperadas

---

## ✅ CASOS DE TESTE - CENÁRIOS COMPLETOS

### TESTE 1: Cadastrar Aluno (Sucesso)

**Entrada:**
```
Opção: 1
Nome: João Silva
Email: joao@email.com
Data Nascimento (dd/MM/yyyy): 05/10/1998
CPF (formato 123.456.789-00): 111.444.777-35
Matrícula: 2025001
```

**Resultado Esperado:**
```
✓ Aluno cadastrado com sucesso!
Nome: João Silva
Matrícula: 2025001
```

**Validação:**
- ✅ Nome aceito
- ✅ Email válido
- ✅ Idade calculada corretamente (25 anos)
- ✅ CPF validado com algoritmo
- ✅ Matrícula única aceita

---

### TESTE 2: Cadastrar Aluno (Falha - CPF Inválido)

**Entrada:**
```
Opção: 1
Nome: Maria Santos
Email: maria@email.com
Data Nascimento: 15/03/2000
CPF: 111.111.111-11  ← Todos os dígitos iguais
Matrícula: 2025002
```

**Resultado Esperado:**
```
❌ CPF inválido: todos os dígitos são iguais.
Tente novamente.
```

**Validação:**
- ✅ Rejeitou CPF com dígitos iguais
- ✅ Retornou ao menu

---

### TESTE 3: Cadastrar Aluno (Falha - Menor de Idade)

**Entrada:**
```
Opção: 1
Nome: Pedro Costa
Email: pedro@email.com
Data Nascimento: 15/03/2010  ← Apenas 14 anos
CPF: 222.555.888-46
Matrícula: 2025003
```

**Resultado Esperado:**
```
❌ Aluno deve ter no mínimo 18 anos de idade.
```

**Validação:**
- ✅ Rejeitou menor de idade
- ✅ Regra de negócio RN-001 funcionando

---

### TESTE 4: Cadastrar Aluno (Falha - Matrícula Duplicada)

**Entrada (1ª vez):**
```
Opção: 1
Nome: Ana Silva
Email: ana@email.com
Data: 01/01/2000
CPF: 333.666.999-57
Matrícula: 2025001  ← Já existe!
```

**Resultado Esperado:**
```
❌ Matrícula 2025001 já existe no sistema.
```

**Validação:**
- ✅ Detectou duplicidade
- ✅ Prevenção de dados duplicados funcionando

---

### TESTE 5: Cadastrar Professor (Sucesso)

**Entrada:**
```
Opção: 2
Nome: Dr. Carlos
Email: carlos@email.com
Data Nascimento: 01/01/1980
CPF: 444.777.000-88
Disciplina: Programação OOP
Salário: 5000
```

**Resultado Esperado:**
```
✓ Professor cadastrado com sucesso!
Nome: Dr. Carlos
Salário: R$ 5.000,00
```

**Validação:**
- ✅ Professor criado
- ✅ Salário validado (entre 1.000 e 100.000)

---

### TESTE 6: Cadastrar Professor (Falha - Salário Baixo)

**Entrada:**
```
Opção: 2
Nome: Prof. Maria
Email: maria@email.com
Data: 15/05/1985
CPF: 555.888.111-99
Disciplina: Matemática
Salário: 500  ← Abaixo do mínimo (1.000)
```

**Resultado Esperado:**
```
❌ Salário não pode ser menor que R$ 1.000,00.
```

**Validação:**
- ✅ Rejeitou salário baixo
- ✅ Regra de negócio RN-007 funcionando

---

### TESTE 7: Cadastrar Disciplina

**Entrada:**
```
Opção: 3
Nome: Programação OOP
Código: POO
Professor CPF: 111.444.777-35  ← Aluno registrado anteriormente
```

**Resultado Esperado:**
```
❌ Professor com CPF 111.444.777-35 não foi encontrado.
```

**OU (se professor existe):**

```
Opção: 3
Nome: Programação OOP
Código: POO
Professor CPF: 444.777.000-88  ← Professor registrado
```

**Resultado Esperado:**
```
✓ Disciplina cadastrada com sucesso!
Código: POO
Nome: Programação OOP
Professor: Dr. Carlos
```

**Validação:**
- ✅ Validou existência do professor
- ✅ Código de disciplina aceito (POO = válido)

---

### TESTE 8: Listar Aprovados e Ver Estatísticas

**Entrada:**
```
Opção: 6 (Ver Relatório)
```

**Resultado Esperado:**
```
=== RELATÓRIOS DO SISTEMA ===
1. Listar Alunos Aprovados
2. Relatório Individual de Aluno
3. Estatísticas Gerais
0. Voltar

Digite sua escolha:
```

**Validação:**
- ✅ Menu de relatórios apareceu
- ✅ Opções disponíveis

---

## 🧪 TESTE DE VALIDAÇÕES (Manual)

### Validar Nome
```
❌ FALHA: "" (vazio)
❌ FALHA: "Jo" (menos de 3 caracteres)
❌ FALHA: "João123" (contém números)
✅ SUCESSO: "João Silva" (3+ caracteres, apenas letras/espaços)
```

### Validar Email
```
❌ FALHA: "joao@" (sem domínio)
❌ FALHA: "joao" (sem @)
❌ FALHA: "@email.com" (sem usuário)
✅ SUCESSO: "joao@email.com" (formato válido)
```

### Validar CPF
```
❌ FALHA: "111.111.111-11" (todos dígitos iguais)
❌ FALHA: "111.444.777-00" (dígitos verificadores errados)
❌ FALHA: "123.456.789" (menos de 11 dígitos)
✅ SUCESSO: "111.444.777-35" (algoritmo módulo 11 correto)
```

### Validar Nota
```
❌ FALHA: -1 (menor que 0)
❌ FALHA: 10.5 (maior que 10)
✅ SUCESSO: 0 a 10 (intervalo válido)
```

### Validar Aprovação
```
Nota 7.0 → ✅ APROVADO
Nota 6.9 → 🔄 RECUPERAÇÃO
Nota 3.9 → ❌ REPROVADO
```

---

## 📊 CHECKLIST DE VALIDAÇÃO

Marque conforme testa:

### Funcionalidades
- [ ] Cadastrar Aluno (sucesso)
- [ ] Cadastrar Aluno (CPF inválido)
- [ ] Cadastrar Aluno (menor de idade)
- [ ] Cadastrar Aluno (matrícula duplicada)
- [ ] Cadastrar Professor (sucesso)
- [ ] Cadastrar Professor (salário baixo)
- [ ] Cadastrar Disciplina
- [ ] Matricular Aluno
- [ ] Atribuir Nota
- [ ] Ver Relatórios
- [ ] Ver Estatísticas

### Validações
- [ ] Nome validado (3+ caracteres)
- [ ] Email validado (padrão @)
- [ ] CPF validado (algoritmo módulo 11)
- [ ] Data nascimento validada (idade mínima 18)
- [ ] Matrícula validada (única)
- [ ] Salário validado (1.000-100.000)
- [ ] Nota validada (0-10)
- [ ] Aprovação calculada corretamente (≥7.0)

### Regras de Negócio
- [ ] RN-001: Idade mínima 18 anos
- [ ] RN-002: Matrícula única
- [ ] RN-003: CPF único
- [ ] RN-004: Nota 0-10
- [ ] RN-005: Aprovação ≥ 7.0
- [ ] RN-006: Professor obrigatório em disciplina
- [ ] RN-007: Salário 1.000-100.000
- [ ] RN-008: Email válido

---

## 🎯 TESTE DE ARQUITETURA

### Teste: Injeção de Dependência
```csharp
// Repositórios podem ser substituídos
IRepositorioAluno repo = new RepositorioAlunoEmMemoria();
// OU
IRepositorioAluno repo = new RepositorioAlunoMySQL(context);

// Serviço não sabe qual implementação
ServicoCadastro servico = new ServicoCadastro(repo, ...);
```

**Validação:**
- ✅ Mesmo serviço funciona com diferentes repositórios
- ✅ Desacoplamento confirmado

### Teste: Repository Pattern
```csharp
// Interface define contrato
public interface IRepositorioAluno
{
    void Adicionar(Aluno aluno);
    Aluno ObterPorMatricula(int matricula);
}

// Múltiplas implementações
public class RepositorioAlunoEmMemoria : IRepositorioAluno { }
public class RepositorioAlunoMySQL : IRepositorioAluno { }
```

**Validação:**
- ✅ Interface bem definida
- ✅ Múltiplas implementações possíveis

### Teste: Service Layer
```csharp
// Serviço contém lógica de negócio
public class ServicoCadastro
{
    public Aluno CadastrarAluno(...)
    {
        ValidadorAcademico.Validar...();
        repositorio.Adicionar();
        return aluno;
    }
}

// UI não conhece lógica
public class TerminalUI
{
    public void Executar()
    {
        servicoCadastro.CadastrarAluno(...);
    }
}
```

**Validação:**
- ✅ Lógica centralizada em serviço
- ✅ UI apenas exibe dados

---

## 🔍 COMO VER OS LOGS

Depois de executar, logs são salvos em:
```
GestaoAcademica\bin\Debug\net10.0\Logs\GestaoAcademica_YYYYMMDD.log
```

**Exemplo de conteúdo:**
```
[2026-04-09 14:53:30] [INFO] === INICIANDO SISTEMA GESTÃO ACADÊMICA ===
[2026-04-09 14:53:30] [INFO] Horário: 09/04/2026 14:53:30
[2026-04-09 14:53:30] [INFO] Usuário: 011.215094
[2026-04-09 14:53:31] [INFO] ✓ Repositórios em-memória inicializados
[2026-04-09 14:53:31] [INFO] ✓ Serviços inicializados com sucesso
[2026-04-09 14:53:32] [ERROR] CPF inválido: todos os dígitos são iguais.
[2026-04-09 14:54:00] [INFO] === SISTEMA FINALIZADO COM SUCESSO ===
```

---

## 🚀 RESULTADO ESPERADO

Ao executar (F5), você verá:

1. **Logs iniciais** (em verde)
2. **Menu principal** no console
3. **Possibilidade de interagir** com o sistema
4. **Validações funcionando** em tempo real
5. **Logs registrados** em arquivo

---

## ✨ RESUMO

**Seu sistema está 100% funcional quando:**

- ✅ Menu aparece ao executar
- ✅ Aceita entrada do usuário
- ✅ Valida dados corretamente
- ✅ Registra logs
- ✅ Retorna mensagens apropriadas
- ✅ Executa operações CRUD

**Tudo isto JÁ EXISTE no seu projeto!** 🎉

---

**Próximo passo:** Execute (F5) e teste! 🚀
