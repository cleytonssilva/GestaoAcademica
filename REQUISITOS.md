# 📋 ESPECIFICAÇÃO DE REQUISITOS DO SISTEMA (ERS)

**Documento:** Especificação de Requisitos do Sistema  
**Projeto:** Sistema de Gestão Acadêmica  
**Versão:** 1.0  

---

## 1. INTRODUÇÃO

### 1.1 Objetivo

Este documento especifica os requisitos funcionais e não-funcionais do **Sistema de Gestão Acadêmica**, desenvolvido como aplicação prática da trilha de **Programação Orientada a Objetos em C#**.

### 1.2 Escopo

O sistema visa gerenciar o ciclo completo de uma instituição acadêmica:
- Cadastro de alunos, professores e disciplinas
- Matrícula de alunos em disciplinas
- Atribuição e controle de notas
- Geração de relatórios acadêmicos

### 1.3 Definições e Abreviações

| Termo | Definição |
|-------|-----------|
| **Aluno** | Pessoa matriculada em uma ou mais disciplinas |
| **Professor** | Responsável por uma ou mais disciplinas |
| **Disciplina** | Componente curricular com professor responsável |
| **Matrícula** | Registro de aluno em disciplina |
| **Nota** | Avaliação numérica do aluno (0-10) |
| **RF** | Requisito Funcional |
| **RNF** | Requisito Não-Funcional |
| **RN** | Regra de Negócio |

---

## 2. REQUISITOS FUNCIONAIS

### 2.1 Cadastro de Alunos (RF-001)

**Descrição:** O sistema deve permitir o cadastro de novos alunos com dados pessoais.

**Pré-condições:**
- Nenhuma

**Fluxo Principal:**
1. Usuário seleciona "Cadastrar Aluno"
2. Sistema solicita dados: Nome, Email, Data Nascimento, CPF, Matrícula
3. Sistema valida os dados (regras de negócio)
4. Sistema verifica duplicidades (CPF e Matrícula)
5. Sistema confirma cadastro com mensagem de sucesso
6. Aluno é adicionado ao repositório

**Fluxo Alternativo (Erro):**
- Se CPF inválido → Mensagem de erro + Novo pedido
- Se Matrícula duplicada → Mensagem de erro
- Se Idade < 18 → Rejeita cadastro

**Pós-condições:**
- Aluno cadastrado no sistema
- Dados persistidos em memória

**Exemplos:**
- ✅ João da Silva, 25 anos, CPF 123.456.789-00, Matrícula 2025001
- ❌ Maria (nome muito curto)
- ❌ pedro@email (email inválido)
- ❌ 01/01/2010 (menor de 18)

---

### 2.2 Cadastro de Professores (RF-002)

**Descrição:** O sistema deve permitir o cadastro de professores com dados profissionais.

**Pré-condições:**
- Nenhuma

**Fluxo Principal:**
1. Usuário seleciona "Cadastrar Professor"
2. Sistema solicita: Nome, Email, Data Nascimento, CPF, Disciplina, Salário
3. Sistema valida dados pessoais e profissionais
4. Sistema verifica duplicidade de CPF
5. Professor é cadastrado com sucesso

**Fluxo Alternativo (Erro):**
- Se CPF duplicado → Rejeita com mensagem
- Se Salário < R$ 1.000 → Rejeita
- Se Salário > R$ 100.000 → Rejeita

**Pós-condições:**
- Professor cadastrado e disponível para designação em disciplinas

---

### 2.3 Cadastro de Disciplinas (RF-003)

**Descrição:** O sistema deve permitir o cadastro de disciplinas com professor responsável.

**Pré-condições:**
- Existir pelo menos um professor cadastrado (RF-002)

**Fluxo Principal:**
1. Usuário seleciona "Cadastrar Disciplina"
2. Sistema lista professores cadastrados
3. Usuário seleciona nome e código da disciplina + professor
4. Sistema valida código (formato e duplicidade)
5. Sistema cria disciplina e vincula ao professor
6. Disciplina registrada no sistema

**Fluxo Alternativo (Erro):**
- Se Código duplicado → Rejeita
- Se Código inválido → Solicita novo formato
- Se Professor não selecionado → Rejeita

**Pós-condições:**
- Disciplina cadastrada e pronta para matrículas

---

### 2.4 Matrícula de Alunos (RF-004)

**Descrição:** O sistema deve permitir matricular alunos em disciplinas.

**Pré-condições:**
- Aluno cadastrado (RF-001)
- Disciplina cadastrada (RF-003)

**Fluxo Principal:**
1. Usuário seleciona "Matricular Aluno"
2. Sistema solicita matrícula do aluno
3. Sistema valida existência do aluno
4. Sistema lista disciplinas disponíveis
5. Usuário seleciona disciplina
6. Sistema verifica se aluno já não está matriculado nela
7. Matrícula é registrada

**Fluxo Alternativo (Erro):**
- Se Aluno não existe → Mensagem de erro
- Se Aluno já matriculado na disciplina → Rejeita com aviso
- Se Nenhuma disciplina disponível → Informa

**Pós-condições:**
- Matrícula registrada
- Aluno pode receber notas nesta disciplina

---

### 2.5 Atribuição de Notas (RF-005)

**Descrição:** O sistema deve permitir atribuir notas aos alunos.

**Pré-condições:**
- Aluno cadastrado (RF-001)
- Aluno matriculado em disciplina (RF-004)

**Fluxo Principal:**
1. Usuário seleciona "Atribuir Nota"
2. Sistema solicita matrícula do aluno
3. Sistema lista disciplinas em que aluno está matriculado
4. Usuário seleciona disciplina
5. Usuário informa nota (0-10)
6. Sistema valida nota
7. Sistema registra nota com data

**Fluxo Alternativo (Erro):**
- Se Nota < 0 ou > 10 → Rejeita
- Se Aluno não tem matrícula → Informa
- Se Aluno já tem nota na disciplina → Oferece atualizar

**Pós-condições:**
- Nota registrada
- Média do aluno atualizada automaticamente

---

### 2.6 Listar Alunos Aprovados (RF-006)

**Descrição:** O sistema deve listar alunos aprovados (média >= 6.0).

**Pré-condições:**
- Existir alunos com notas cadastradas

**Fluxo Principal:**
1. Usuário seleciona "Listar Alunos Aprovados"
2. Sistema calcula média de cada aluno
3. Sistema filtra alunos com média >= 6.0
4. Sistema exibe lista ordenada por média (decrescente)

**Fluxo Alternativo:**
- Se Nenhum aluno aprovado → Informa "Nenhum aluno aprovado"

**Pós-condições:**
- Lista exibida na tela
- Dados não são alterados

---

### 2.7 Relatório Individual de Aluno (RF-007)

**Descrição:** O sistema deve gerar relatório detalhado de um aluno.

**Pré-condições:**
- Aluno cadastrado (RF-001)

**Fluxo Principal:**
1. Usuário seleciona "Relatório de Aluno"
2. Sistema solicita matrícula do aluno
3. Sistema valida existência
4. Sistema gera relatório contendo:
   - Dados pessoais
   - Disciplinas matriculado
   - Notas por disciplina
   - Média geral
   - Situação (Aprovado/Recuperação/Reprovado)
5. Relatório é exibido formatado

**Pós-condições:**
- Relatório exibido
- Dados não são alterados

---

### 2.8 Estatísticas Gerais do Sistema (RF-008)

**Descrição:** O sistema deve gerar estatísticas gerais.

**Pré-condições:**
- Sistema possui dados cadastrados

**Fluxo Principal:**
1. Usuário seleciona "Estatísticas Gerais"
2. Sistema calcula:
   - Total de alunos
   - Total de professores
   - Total de disciplinas
   - Média geral dos alunos
   - Quantidade de aprovados
   - Quantidade de reprovados
   - Percentual de aprovação
3. Estatísticas são exibidas em formato tabular

**Pós-condições:**
- Dados não são alterados

---

## 3. REQUISITOS NÃO-FUNCIONAIS

### 3.1 Usabilidade (RNF-001)

- Sistema deve ter interface em terminal clara e intuitiva
- Mensagens de erro devem ser específicas e úteis
- Menus numerados para fácil navegação
- Confirmação de operações críticas (ex: deletar)

**Critério de Sucesso:**
- Novo usuário consegue usar o sistema sem documentação adicional

---

### 3.2 Performance (RNF-002)

- Qualquer operação deve responder em < 1 segundo
- Listas com 10.000 alunos devem ser filtradas em < 500ms
- Relatórios devem ser gerados em < 2 segundos

**Critério de Sucesso:**
- Cronômetro em operações críticas valida tempo

---

### 3.3 Confiabilidade (RNF-003)

- Sistema deve validar 100% dos dados de entrada
- Nenhuma operação deve deixar dados inconsistentes
- Erros devem ser tratados graciosamente

**Critério de Sucesso:**
- Teste de stress com dados inválidos não quebra o sistema

---

### 3.4 Manutenibilidade (RNF-004)

- Código deve seguir padrão Clean Code
- Comentários em classes e métodos públicos
- Nomes significativos para variáveis e funções
- Máximo 30 linhas por método

**Critério de Sucesso:**
- Code review aprova 100% do código

---

### 3.5 Portabilidade (RNF-005)

- Sistema deve executar em .NET Framework 4.7.2+
- Compatível com Windows, Linux (via Mono/Wine)
- Sem dependências externas (apenas BCL)

**Critério de Sucesso:**
- Compila e executa em múltiplas plataformas

---

## 4. REGRAS DE NEGÓCIO

### 4.1 Validações de Alunos

```
RN-001: Aluno deve ter mínimo 18 anos
        - Data de nascimento + 18 anos <= Hoje

RN-002: Matrícula deve ser única
        - Cada aluno tem exatamente 1 matrícula
        - Matrícula não pode ser duplicada

RN-003: CPF deve ser válido
        - Formato: XXX.XXX.XXX-XX
        - Validar dígitos verificadores
        - CPF não pode ser duplicado

RN-004: Email deve ser válido
        - Padrão: usuario@dominio.com
        - Não pode conter caracteres especiais inválidos

RN-005: Nome deve ter mínimo 3 caracteres
        - Apenas letras e espaços
        - Não pode conter números
```

### 4.2 Validações de Professores

```
RN-006: Professor obrigatório em Disciplina
        - Cada disciplina tem exatamente 1 professor
        - Professor deve estar cadastrado no sistema

RN-007: Salário deve estar entre R$ 1.000 e R$ 100.000
        - Validação de intervalo

RN-008: CPF de Professor segue as mesmas regras (RN-003)
```

### 4.3 Validações de Notas

```
RN-009: Nota deve estar entre 0 e 10
        - Aceita decimais (ex: 8.5)

RN-010: Aluno só recebe nota em disciplina que está matriculado
        - Validar matrícula antes de atribuir nota

RN-011: Apenas 1 nota por aluno/disciplina
        - Sistema permite atualizar nota existente

RN-012: Aprovação requer média >= 6.0
        - Média = (Σ notas) / (total de notas)
        - Aprovado: média >= 6.0
        - Recuperação: 4.0 <= média < 6.0
        - Reprovado: média < 4.0
```

### 4.4 Validações de Disciplinas

```
RN-013: Código de Disciplina deve ser único
        - Formato: 2-10 caracteres, maiúsculas + números
        - Exemplo: MAT001, PORT001

RN-014: Nome de Disciplina entre 3-100 caracteres
```

---

## 5. CRITÉRIOS DE ACEITAÇÃO

### Para Cada Requisito Funcional

| RF | Criterio | Status |
|----|----------|--------|
| RF-001 | Cadastra aluno com todos os dados | ✅ OK |
| RF-001 | Valida idade >= 18 anos | ✅ OK |
| RF-001 | Impede matrícula duplicada | ✅ OK |
| RF-002 | Cadastra professor com salário | ✅ OK |
| RF-002 | Valida salário (R$ 1k-100k) | ✅ OK |
| RF-003 | Cadastra disciplina com professor | ✅ OK |
| RF-003 | Impede código duplicado | ✅ OK |
| RF-004 | Matricula aluno em disciplina | ✅ OK |
| RF-004 | Impede matrícula duplicada | ✅ OK |
| RF-005 | Atribui nota 0-10 | ✅ OK |
| RF-005 | Calcula média automática | ✅ OK |
| RF-006 | Lista aprovados (média >= 6) | ✅ OK |
| RF-007 | Gera relatório individual | ✅ OK |
| RF-008 | Gera estatísticas gerais | ✅ OK |

---

## 6. CENÁRIOS DE TESTE

### Cenário 1: Fluxo Completo de Aprovação

```
1. Cadastrar Professor: João (Matemática, R$ 3.000)
2. Cadastrar Disciplina: MAT001 (Matemática I, Prof. João)
3. Cadastrar Aluno: Maria (25 anos, Matrícula 2025001)
4. Matricular Maria em MAT001
5. Atribuir Nota: Maria = 8.5 em MAT001
6. Listar Aprovados → Maria deve aparecer com média 8.5
7. Gerar Relatório de Maria → Deve mostrar APROVADO

RESULTADO ESPERADO: ✅ Maria aprovada com nota 8.5
```

### Cenário 2: Validação de CPF Inválido

```
1. Tentar cadastrar aluno com CPF: 111.111.111-11
2. Sistema deve rejeitar (CPF inválido)

RESULTADO ESPERADO: ❌ Mensagem de erro, novo pedido de CPF
```

### Cenário 3: Matrícula Duplicada

```
1. Cadastrar Aluno: João (Matrícula 2025001)
2. Tentar cadastrar outro aluno com Matrícula 2025001
3. Sistema deve rejeitar

RESULTADO ESPERADO: ❌ Matrícula já existe
```

---

## 7. CONCLUSÃO

Este documento especifica todos os requisitos necessários para o desenvolvimento do Sistema de Gestão Acadêmica. Todos os requisitos foram implementados e testados, garantindo conformidade com as regras de negócio e boas práticas de engenharia de software.

---
