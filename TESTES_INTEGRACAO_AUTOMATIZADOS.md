# 🤖 TESTE DE INTEGRAÇÃO AUTOMATIZADO

## O Que É

Um programa que testa a API de forma automatizada, sem precisar que você digite manualmente.

## Como Usar

### Opção 1: Adicionar ao Projeto (Recomendado)

Crie um novo arquivo: `GestaoAcademica/Testes/TestesIntegracao.cs`

```csharp
using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using GestaoAcademica.Servicos;
using System;
using System.Collections.Generic;

namespace GestaoAcademica.Testes
{
    /// <summary>
    /// Testes de integração automatizados
    /// Valida funcionalidades completas do sistema
    /// </summary>
    public class TestesIntegracao
    {
        private readonly IRepositorioAluno _repositorioAluno;
        private readonly IRepositorioProfessor _repositorioProfessor;
        private readonly IRepositorioDisciplina _repositorioDisciplina;
        private readonly IRepositorioMatricula _repositorioMatricula;
        private readonly ServicoCadastro _servicoCadastro;
        private readonly ServicoMatricula _servicoMatricula;
        private readonly ServicoAvaliacao _servicoAvaliacao;
        private readonly ServicoRelatorio _servicoRelatorio;

        public TestesIntegracao()
        {
            _repositorioAluno = new RepositorioAlunoEmMemoria();
            _repositorioProfessor = new RepositorioProfessorEmMemoria();
            _repositorioDisciplina = new RepositorioDisciplinaEmMemoria();
            _repositorioMatricula = new RepositorioMatriculaEmMemoria();

            _servicoCadastro = new ServicoCadastro(_repositorioAluno, _repositorioProfessor, _repositorioDisciplina);
            _servicoMatricula = new ServicoMatricula(_repositorioMatricula);
            _servicoAvaliacao = new ServicoAvaliacao();
            _servicoRelatorio = new ServicoRelatorio();
        }

        // ============== TESTES DE ALUNO ==============

        public void TestarCadastroAlunoValido()
        {
            Console.WriteLine("\n[TESTE 1] Cadastro de Aluno Válido");
            try
            {
                var aluno = _servicoCadastro.CadastrarAluno(
                    "João Silva",
                    "joao@email.com",
                    new DateTime(1998, 10, 5),
                    "111.444.777-35",
                    2025001
                );

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ SUCESSO: Aluno {aluno.Nome} cadastrado");
                Console.WriteLine($"  Matrícula: {aluno.Matricula}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ FALHA: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void TestarCadastroCPFDuplicado()
        {
            Console.WriteLine("\n[TESTE 2] Tentativa de CPF Duplicado");
            try
            {
                _servicoCadastro.CadastrarAluno("João", "joao@email.com", new DateTime(1998, 10, 5), "111.444.777-35", 2025001);
                _servicoCadastro.CadastrarAluno("Maria", "maria@email.com", new DateTime(2000, 3, 15), "111.444.777-35", 2025002);
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("✗ FALHA: Deveria ter rejeitado CPF duplicado");
                Console.ResetColor();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ SUCESSO: Rejeitou duplicidade - {ex.Message}");
                Console.ResetColor();
            }
        }

        public void TestarCadastroMenorIdade()
        {
            Console.WriteLine("\n[TESTE 3] Tentativa de Cadastrar Menor de Idade");
            try
            {
                _servicoCadastro.CadastrarAluno(
                    "Pedro",
                    "pedro@email.com",
                    new DateTime(2010, 3, 15),  // 15 anos
                    "222.555.888-46",
                    2025003
                );

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("✗ FALHA: Deveria ter rejeitado menor de idade");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ SUCESSO: Rejeitou menor de idade - {ex.Message}");
                Console.ResetColor();
            }
        }

        public void TestarValidacaoCPF()
        {
            Console.WriteLine("\n[TESTE 4] Validação de CPF com Algoritmo");
            try
            {
                // CPF inválido (dígitos iguais)
                _servicoCadastro.CadastrarAluno(
                    "Ana",
                    "ana@email.com",
                    new DateTime(2000, 1, 1),
                    "111.111.111-11",  // Inválido
                    2025004
                );

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("✗ FALHA: Deveria ter rejeitado CPF inválido");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ SUCESSO: Validou CPF - {ex.Message}");
                Console.ResetColor();
            }
        }

        // ============== TESTES DE PROFESSOR ==============

        public void TestarCadastroProfessor()
        {
            Console.WriteLine("\n[TESTE 5] Cadastro de Professor Válido");
            try
            {
                var professor = _servicoCadastro.CadastrarProfessor(
                    "Dr. Carlos",
                    "carlos@email.com",
                    new DateTime(1980, 1, 1),
                    "333.666.999-57",
                    "POO",
                    5000M
                );

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ SUCESSO: Professor {professor.Nome} cadastrado");
                Console.WriteLine($"  Salário: R$ {professor.Salario:F2}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ FALHA: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void TestarSalarioMinimo()
        {
            Console.WriteLine("\n[TESTE 6] Validação de Salário Mínimo");
            try
            {
                _servicoCadastro.CadastrarProfessor(
                    "Prof. Maria",
                    "maria@email.com",
                    new DateTime(1985, 5, 15),
                    "444.777.000-88",
                    "Matemática",
                    500M  // Abaixo do mínimo
                );

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("✗ FALHA: Deveria ter rejeitado salário baixo");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ SUCESSO: Rejeitou salário baixo - {ex.Message}");
                Console.ResetColor();
            }
        }

        // ============== TESTES DE VALIDAÇÕES ==============

        public void TestarValidacaoNome()
        {
            Console.WriteLine("\n[TESTE 7] Validação de Nome");
            var testes = new List<(string nome, bool devePassar)>
            {
                ("João Silva", true),      // ✓ Válido
                ("Jo", false),             // ✗ Muito curto
                ("João123", false),        // ✗ Contém números
                ("", false),               // ✗ Vazio
            };

            foreach (var (nome, devePassar) in testes)
            {
                try
                {
                    ValidadorAcademico.ValidarNome(nome);
                    if (devePassar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ SUCESSO: Nome '{nome}' aceito");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"✗ FALHA: Nome '{nome}' deveria ser rejeitado");
                    }
                }
                catch
                {
                    if (!devePassar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ SUCESSO: Nome '{nome}' rejeitado");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"✗ FALHA: Nome '{nome}' deveria ser aceito");
                    }
                }
                Console.ResetColor();
            }
        }

        public void TestarValidacaoEmail()
        {
            Console.WriteLine("\n[TESTE 8] Validação de Email");
            var testes = new List<(string email, bool devePassar)>
            {
                ("joao@email.com", true),      // ✓ Válido
                ("joao@", false),              // ✗ Sem domínio
                ("joao", false),               // ✗ Sem @
                ("@email.com", false),         // ✗ Sem usuário
            };

            foreach (var (email, devePassar) in testes)
            {
                try
                {
                    ValidadorAcademico.ValidarEmail(email);
                    if (devePassar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ SUCESSO: Email '{email}' aceito");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"✗ FALHA: Email '{email}' deveria ser rejeitado");
                    }
                }
                catch
                {
                    if (!devePassar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ SUCESSO: Email '{email}' rejeitado");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"✗ FALHA: Email '{email}' deveria ser aceito");
                    }
                }
                Console.ResetColor();
            }
        }

        public void TestarValidacaoNota()
        {
            Console.WriteLine("\n[TESTE 9] Validação de Nota (0-10)");
            var testes = new List<(double nota, bool devePassar)>
            {
                (0, true),      // ✓ Válido
                (5.0, true),    // ✓ Válido
                (10.0, true),   // ✓ Válido
                (-1, false),    // ✗ Negativa
                (10.5, false),  // ✗ Maior que 10
            };

            foreach (var (nota, devePassar) in testes)
            {
                try
                {
                    ValidadorAcademico.ValidarNota(nota);
                    if (devePassar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ SUCESSO: Nota {nota} aceita");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"✗ FALHA: Nota {nota} deveria ser rejeitada");
                    }
                }
                catch
                {
                    if (!devePassar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ SUCESSO: Nota {nota} rejeitada");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"✗ FALHA: Nota {nota} deveria ser aceita");
                    }
                }
                Console.ResetColor();
            }
        }

        public void TestarAprovacao()
        {
            Console.WriteLine("\n[TESTE 10] Cálculo de Aprovação");
            var testes = new List<(double media, string esperado)>
            {
                (7.0, "APROVADO"),
                (9.5, "APROVADO"),
                (6.9, "RECUPERAÇÃO"),
                (5.0, "RECUPERAÇÃO"),
                (3.9, "REPROVADO"),
            };

            foreach (var (media, esperado) in testes)
            {
                string resultado = ValidadorAcademico.CalcularSituacao(media);
                bool correto = resultado == esperado;

                Console.ForegroundColor = correto ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"{(correto ? "✓" : "✗")} Média {media} → {resultado} (esperado: {esperado})");
                Console.ResetColor();
            }
        }

        // ============== EXECUTAR TODOS OS TESTES ==============

        public void ExecutarTodosTestes()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   TESTES DE INTEGRAÇÃO - GESTÃO ACADÊMICA             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            TestarCadastroAlunoValido();
            TestarCadastroCPFDuplicado();
            TestarCadastroMenorIdade();
            TestarValidacaoCPF();
            TestarCadastroProfessor();
            TestarSalarioMinimo();
            TestarValidacaoNome();
            TestarValidacaoEmail();
            TestarValidacaoNota();
            TestarAprovacao();

            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   TESTES CONCLUÍDOS                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");
        }

        // ============== PONTO DE ENTRADA ==============

        public static void Main(string[] args)
        {
            var testes = new TestesIntegracao();
            testes.ExecutarTodosTestes();

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
```

---

## Como Executar Este Teste

### Opção 1: Via Visual Studio
```
1. Clique com botão direito em GestaoAcademica/Testes/TestesIntegracao.cs
2. Set as Startup File (ou similar)
3. Pressione F5
```

### Opção 2: Via Console
```powershell
cd GestaoAcademica
dotnet test --filter "TestesIntegracao"
```

---

## Resultado Esperado

```
╔════════════════════════════════════════════════════════╗
║   TESTES DE INTEGRAÇÃO - GESTÃO ACADÊMICA             ║
╚════════════════════════════════════════════════════════╝

[TESTE 1] Cadastro de Aluno Válido
✓ SUCESSO: Aluno João Silva cadastrado
  Matrícula: 2025001

[TESTE 2] Tentativa de CPF Duplicado
✓ SUCESSO: Rejeitou duplicidade - CPF 111.444.777-35 já foi cadastrado.

[TESTE 3] Tentativa de Cadastrar Menor de Idade
✓ SUCESSO: Rejeitou menor de idade - Aluno deve ter no mínimo 18 anos de idade.

[TESTE 4] Validação de CPF com Algoritmo
✓ SUCESSO: Validou CPF - CPF inválido: todos os dígitos são iguais.

[TESTE 5] Cadastro de Professor Válido
✓ SUCESSO: Professor Dr. Carlos cadastrado
  Salário: R$ 5000.00

[TESTE 6] Validação de Salário Mínimo
✓ SUCESSO: Rejeitou salário baixo - Salário não pode ser menor que R$ 1000.00.

[TESTE 7] Validação de Nome
✓ SUCESSO: Nome 'João Silva' aceito
✓ SUCESSO: Nome 'Jo' rejeitado
✓ SUCESSO: Nome 'João123' rejeitado
✓ SUCESSO: Nome '' rejeitado

[TESTE 8] Validação de Email
✓ SUCESSO: Email 'joao@email.com' aceito
✓ SUCESSO: Email 'joao@' rejeitado
✓ SUCESSO: Email 'joao' rejeitado
✓ SUCESSO: Email '@email.com' rejeitado

[TESTE 9] Validação de Nota (0-10)
✓ SUCESSO: Nota 0 aceita
✓ SUCESSO: Nota 5 aceita
✓ SUCESSO: Nota 10 aceita
✓ SUCESSO: Nota -1 rejeitada
✓ SUCESSO: Nota 10.5 rejeitada

[TESTE 10] Cálculo de Aprovação
✓ Média 7 → APROVADO (esperado: APROVADO)
✓ Média 9.5 → APROVADO (esperado: APROVADO)
✓ Média 6.9 → RECUPERAÇÃO (esperado: RECUPERAÇÃO)
✓ Média 5 → RECUPERAÇÃO (esperado: RECUPERAÇÃO)
✓ Média 3.9 → REPROVADO (esperado: REPROVADO)

╔════════════════════════════════════════════════════════╗
║   TESTES CONCLUÍDOS                                    ║
╚════════════════════════════════════════════════════════╝
```

---

**Todos os testes em ✓ VERDE = API 100% funcional!** 🎉
