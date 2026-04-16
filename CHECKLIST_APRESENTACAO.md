# ✅ CHECKLIST PRÉ-APRESENTAÇÃO

## 🎓 ANTES DE APRESENTAR PARA O PROFESSOR

### Semana Anterior

- [ ] **Compilar sem erros** 
  ```
  Build → Rebuild Solution (Ctrl+Alt+B)
  Resultado esperado: "Build bem-sucedido"
  ```

- [ ] **Limpar cache e binários temporários**
  ```
  Build → Clean Solution
  Depois Rebuild
  ```

- [ ] **Verificar Git**
  ```
  Commits limpos com mensagens descritivas
  Nenhum arquivo temporário (.vs, bin, obj) no repositório
  git status (deve estar clean)
  ```

- [ ] **Testar funcionalidades básicas** (sem banco de dados)
  ```
  F5 → Debug
  1. Cadastrar aluno válido ✓
  2. Tentar CPF duplicado ✓ (deve rejeitar)
  3. Tentar menor de 18 anos ✓ (deve rejeitar)
  4. Ver relatório ✓
  5. Sair (pressionar X na janela)
  ```

---

### Dia da Apresentação - Manhã

- [ ] **Abrir Visual Studio**
  ```
  Verificar que abre sem erros
  Console Output limpo (sem warnings em amarelo)
  ```

- [ ] **Backup em Pen Drive**
  ```
  Pasta inteira do GestaoAcademica copiada
  Backup em nuvem (GitHub) sincronizado
  ```

- [ ] **Revisar Documentos**
  ```
  ✓ README.md
  ✓ ARQUITETURA.md
  ✓ REQUISITOS.md
  ✓ IMPLEMENTACAO_TECNICA.md
  ✓ GUIA_APRESENTACAO.md
  ```

- [ ] **Preparar Exemplos de Dados**
  ```
  Nomes para cadastrar:
    - João Silva (CPF fictício mas válido para teste)
    - Maria Santos
    - Pedro Oliveira
  
  Disciplinas:
    - POO (Programação OOP)
    - BD (Banco de Dados)
  
  Notas para demonstrar:
    - Aprovado: 8.5
    - Recuperação: 5.0
    - Reprovado: 3.5
  ```

---

### Dia da Apresentação - Antes da Aula

- [ ] **Configurar Resolução de Tela**
  ```
  Texto legível (120% recomendado)
  Fonte de código adequada
  ```

- [ ] **Fechar Abas Desnecessárias**
  ```
  Deixar apenas:
    - Program.cs
    - ValidadorAcademico.cs
    - ServicoCadastro.cs
    - Algum modelo (Aluno.cs)
  ```

- [ ] **Preparar Apresentação Silenciosa**
  ```
  Sem música ao fundo
  Microfone testado
  Câmera (se online) testada
  ```

- [ ] **Ter Resposta Pronta Para Pergunta: "Por quê essa arquitetura?"**
  ```
  Resposta sugerida:
  "Separação de responsabilidades. Cada camada tem um trabalho:
  - UI: mostrar dados
  - Services: aplicar regras de negócio
  - Data: persistir dados
  Isso facilita testes, manutenção e mudanças futuras."
  ```

---

## 🖥️ DURANTE A APRESENTAÇÃO

### Estrutura Recomendada (15-20 minutos)

#### Minuto 1-2: Visão Geral
```
"Este é um sistema de gestão acadêmica que implementa
os conceitos de OOP (Orientação a Objetos) através de
um caso real de uso: cadastro de alunos, professores,
disciplinas e notas."
```

#### Minuto 3-5: Arquitetura
```
Mostrar diagrama em ARQUITETURA.md
Explicar as 4 camadas:
1. UI (Terminal)
2. Services (Lógica)
3. Data (Repositórios)
4. Domain (Modelos)
```

#### Minuto 6-8: Código - Herança
```
Abrir Dominio/Pessoa.cs
Mostrar classe abstrata Pessoa
Depois Dominio/Aluno.cs mostrando herança
```

#### Minuto 9-11: Código - Validação
```
Abrir Servicos/ValidadorAcademico.cs
Mostrar método ValidarCPF (algoritmo)
Mostrar constantes de regras de negócio
```

#### Minuto 12-14: Código - Repository Pattern
```
Abrir Dados/Repositorio.cs
Mostrar interfaces (IRepositorioAluno, etc)
Mostrar fallback automático MySQL → Em-memória
```

#### Minuto 15-17: Demonstração Prática
```
Compilar (F5)
Cadastrar um aluno
Tentar duplicado (deve falhar)
Ver relatório
```

#### Minuto 18-20: Padrões e Conclusão
```
Design Patterns: Repository, Dependency Injection, Service Layer
OOP Concepts: Herança, Polimorfismo, Encapsulamento
Conclusão: "Código profissional, educacional, escalável"
```

---

### O Que Levar Impressa

- [ ] Cópia de REQUISITOS.md
- [ ] Diagrama de arquitetura (print de ARQUITETURA.md)
- [ ] Lista de regras de negócio validadas
- [ ] Exemplos de CPF válidos para demonstração

---

### Respostas Preparadas

**P: "Este é um projeto real ou só para aprendizado?"**
- R: "Ambos! É educacional porque implementa OOP, mas a arquitetura é profissional. Poderia ser escalado com banco de dados real, autenticação, API REST."

**P: "Por que não usar Entity Framework automaticamente?"**
- R: "Repository Pattern fornece abstração. Posso trocar de EF para Dapper sem alterar serviços. É desacoplamento."

**P: "Como você validou o CPF?"**
- R: "Algoritmo padrão brasileiro: módulo 11 com dois dígitos verificadores. Testado com CPFs reais."

**P: "E a segurança?"**
- R: "Este é POO educacional, então foco em lógica. Em produção, teria autenticação, criptografia de senha, validação de entrada contra SQL injection."

**P: "Tempo de execução?"**
- R: "Instantâneo para em-memória (~ms). Com MySQL, depende da rede. Recomendaria índices em CPF e Matrícula."

---

## 🎯 PONTOS PARA IMPRESSIONAR

1. **Menção do algoritmo de CPF**
   - Poucos alunos implementam
   - Mostra conhecimento de regras reais

2. **Fallback automático de banco de dados**
   - Demonstra pensamento em produção
   - Resiliência

3. **Exceções personalizadas**
   - Tratamento específico de cada tipo de erro
   - Melhor que Exception genérica

4. **Logging centralizado**
   - Rastreamento de operações
   - Essencial em produção

5. **Injeção de dependência manual**
   - Mesmo sem framework di (como Autofac)
   - Mostra compreensão de padrão

---

## 🚨 ERROS COMUNS A EVITAR

- ❌ Não compilar antes de mostrar ao professor
- ❌ Deixar warnings em amarelo no build
- ❌ Abrir janela do git durante apresentação
- ❌ Ter arquivo temporário (Class1.cs) no projeto
- ❌ Dizer "achei na internet" sem entender
- ❌ Gastar muito tempo em UI (foco em código)
- ❌ Não ter resposta pronta para pergunta comum

---

## 📊 NOTA ESPERADA: 9-10 / 10

### Se Professor Disser:
- "Bem estruturado" → Mais 0.5
- "Boas práticas" → Mais 0.5
- "Explique a arquitetura" → Responder bem → Mais 1.0
- "Por que esse padrão?" → Justificar → Mais 0.5

---

**ÓTIMA SORTE! 🍀 Você vai arrasar! 🚀**
