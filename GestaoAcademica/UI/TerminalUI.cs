using System;
using System.Globalization;
using GestaoAcademica.Servicos;
using System.Linq;


namespace GestaoAcademica.UI
{
    /// <summary>
    /// Interface com usuário em terminal para o sistema de Gestão Acadêmica.
    /// Responsável por toda interação com o usuário e apresentação de dados.
    /// </summary>
    public class TerminalUI
    {
        private readonly ServicoCadastro _servicoCadastro;
        private readonly ServicoMatricula _servicoMatricula;
        private readonly ServicoAvaliacao _servicoAvaliacao;
        private readonly ServicoRelatorio _servicoRelatorio;

        public TerminalUI(ServicoCadastro servicoCadastro, ServicoMatricula servicoMatricula, ServicoAvaliacao servicoAvaliacao, ServicoRelatorio servicoRelatorio)
        {
            _servicoCadastro = servicoCadastro;
            _servicoMatricula = servicoMatricula;
            _servicoAvaliacao = servicoAvaliacao;
            _servicoRelatorio = servicoRelatorio;
        }

        public void Executar()
        {
            ExibirMensagemBoasVindas();

            while (true)
            {
                try
                {
                    ExibirMenuPrincipal();
                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            CadastrarAluno();
                            break;
                        case "2":
                            CadastrarProfessor();
                            break;
                        case "3":
                            CadastrarDisciplina();
                            break;
                        case "4":
                            MatricularAluno();
                            break;
                        case "5":
                            AtribuirNota();
                            break;
                        case "6":
                            ListarAlunosAprovados();
                            break;
                        case "7":
                            ExibirRelatorios();
                            break;
                        case "8":
                            Console.WriteLine("\n✓ Sistema finalizado. Até logo!");
                            return;
                        default:
                            ExibirErro("Opção inválida. Tente novamente.");
                            break;
                    }

                    PausarTela();
                }
                catch (Exception ex)
                {
                    ExibirErro($"Erro inesperado: {ex.Message}");
                    PausarTela();
                }
            }
        }

        private void ExibirMensagemBoasVindas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  SISTEMA DE GESTÃO ACADÊMICA v1.0      ║");
            Console.WriteLine("║  Apresentação Acadêmica                ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");
            Console.ResetColor();
        }

        private void ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("\n═══════ MENU PRINCIPAL ═══════");
            Console.WriteLine("1. Cadastrar Aluno");
            Console.WriteLine("2. Cadastrar Professor");
            Console.WriteLine("3. Cadastrar Disciplina");
            Console.WriteLine("4. Matricular Aluno em Disciplina");
            Console.WriteLine("5. Atribuir Nota");
            Console.WriteLine("6. Listar Alunos Aprovados");
            Console.WriteLine("7. Exibir Relatórios");
            Console.WriteLine("8. Sair");
            Console.WriteLine("══════════════════════════════");
            Console.Write("\nOpção: ");
        }

        private void CadastrarAluno()
        {
            Console.Clear();
            Console.WriteLine("═══════ CADASTRO DE ALUNO ═══════\n");

            try
            {
                Console.Write("Nome Completo: ");
                string nome = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Data de Nascimento (dd/MM/yyyy): ");
                DateTime dataNascimento = LerDataValida();

                Console.Write("CPF (XXX.XXX.XXX-XX): ");
                string cpf = Console.ReadLine();

                Console.Write("Matrícula (número único): ");
                int matricula = LerInteiro();

                // Chamar serviço com validações
                var aluno = _servicoCadastro.CadastrarAluno(nome, email, dataNascimento, cpf, matricula);

                ExibirSucesso($"Aluno {aluno.Nome} cadastrado com sucesso! (Matrícula: {aluno.Matricula})");
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        private void CadastrarProfessor()
        {
            Console.Clear();
            Console.WriteLine("═══════ CADASTRO DE PROFESSOR ═══════\n");

            try
            {
                Console.Write("Nome Completo: ");
                string nome = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Data de Nascimento (dd/MM/yyyy): ");
                DateTime dataNascimento = LerDataValida();

                Console.Write("CPF (XXX.XXX.XXX-XX): ");
                string cpf = Console.ReadLine();

                Console.Write("Disciplina Principal: ");
                string disciplina = Console.ReadLine();

                Console.Write("Salário (R$): ");
                decimal salario = LerDecimal();

                var professor = _servicoCadastro.CadastrarProfessor(nome, email, dataNascimento, cpf, disciplina, salario);

                ExibirSucesso($"Professor {professor.Nome} cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        private void CadastrarDisciplina()
        {
            Console.Clear();
            Console.WriteLine("═══════ CADASTRO DE DISCIPLINA ═══════\n");

            try
            {
                Console.Write("Nome da Disciplina: ");
                string nome = Console.ReadLine();

                Console.Write("Código (ex: MAT001): ");
                string codigo = Console.ReadLine();

                // Listar professores disponíveis
                var professores = _servicoCadastro.ListarProfessores();

                if (professores.Count == 0)
                {
                    ExibirErro("Não há professores cadastrados. Cadastre um professor primeiro.");
                    return;
                }

                Console.WriteLine("\n--- Professores Disponíveis ---");
                for (int i = 0; i < professores.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {professores[i].Nome} - {professores[i].Disciplina}");
                }

                Console.Write("\nSelecione o número do professor: ");
                int indiceProfessor = LerInteiro() - 1;

                if (indiceProfessor < 0 || indiceProfessor >= professores.Count)
                {
                    ExibirErro("Opção inválida!");
                    return;
                }

                var professor = professores[indiceProfessor];
                var disciplina = _servicoCadastro.CadastrarDisciplina(nome, codigo, professor);

                ExibirSucesso($"Disciplina {disciplina.Nome} cadastrada com sucesso! (Código: {disciplina.Codigo})");
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        private void MatricularAluno()
        {
            Console.Clear();
            Console.WriteLine("═══════ MATRÍCULA EM DISCIPLINA ═══════\n");

            try
            {
                Console.Write("Matrícula do Aluno: ");
                int matriculaAluno = LerInteiro();

                var aluno = _servicoCadastro.ObterAluno(matriculaAluno);

                // Listar disciplinas disponíveis
                var disciplinas = _servicoCadastro.ListarDisciplinas();

                if (disciplinas.Count == 0)
                {
                    ExibirErro("Não há disciplinas cadastradas.");
                    return;
                }

                Console.WriteLine("\n--- Disciplinas Disponíveis ---");
                for (int i = 0; i < disciplinas.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {disciplinas[i].Nome} (Código: {disciplinas[i].Codigo})");
                }

                Console.Write("\nSelecione o número da disciplina: ");
                int indiceDisciplina = LerInteiro() - 1;

                if (indiceDisciplina < 0 || indiceDisciplina >= disciplinas.Count)
                {
                    ExibirErro("Opção inválida!");
                    return;
                }

                var disciplina = disciplinas[indiceDisciplina];

                // Verificar se o aluno já está matriculado na disciplina
                var matriculasExistentes = _servicoMatricula.ObterMatriculasAluno(matriculaAluno);
                if (matriculasExistentes.Any(m => m.Disciplina.Codigo == disciplina.Codigo))
                {
                    ExibirErro("O aluno já está matriculado nesta disciplina.");
                    return;
                }

                var matricula = _servicoMatricula.MatricularAluno(aluno, disciplina);

                ExibirSucesso($"Aluno {aluno.Nome} matriculado em {disciplina.Nome}!");
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        private void AtribuirNota()
        {
            Console.Clear();
            Console.WriteLine("═══════ ATRIBUIÇÃO DE NOTA ═══════\n");

            try
            {
                Console.Write("Matrícula do Aluno: ");
                int matriculaAluno = LerInteiro();

                var aluno = _servicoCadastro.ObterAluno(matriculaAluno);

                // Listar disciplinas em que o aluno está matriculado
                var matriculas = _servicoMatricula.ObterMatriculasAluno(matriculaAluno);

                if (matriculas.Count == 0)
                {
                    ExibirErro($"Aluno {aluno.Nome} não está matriculado em nenhuma disciplina.");
                    return;
                }

                Console.WriteLine($"\n--- Disciplinas de {aluno.Nome} ---");
                for (int i = 0; i < matriculas.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {matriculas[i].Disciplina.Nome}");
                }

                Console.Write("\nSelecione o número da disciplina: ");
                int indiceDisciplina = LerInteiro() - 1;

                if (indiceDisciplina < 0 || indiceDisciplina >= matriculas.Count)
                {
                    ExibirErro("Opção inválida!");
                    return;
                }

                var disciplina = matriculas[indiceDisciplina].Disciplina;

                Console.Write($"\nNota em {disciplina.Nome} (0 a 10): ");
                double nota = LerDouble();

                // Validar faixa de nota
                if (nota < 0 || nota > 10)
                {
                    ExibirErro("A nota deve estar entre 0 e 10.");
                    return;
                }

                var notaAtribuida = _servicoAvaliacao.AtribuirNota(aluno, disciplina, nota);

                ExibirSucesso($"Nota {notaAtribuida.Valor} atribuída com sucesso!");
                Console.WriteLine($"Situação Atual: {_servicoAvaliacao.ObterSituacao(aluno)}");
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        private void ListarAlunosAprovados()
        {
            Console.Clear();
            Console.WriteLine("═══════ ALUNOS APROVADOS ═══════\n");

            try
            {
                var alunos = _servicoCadastro.ListarAlunos();

                if (alunos.Count == 0)
                {
                    ExibirErro("Nenhum aluno cadastrado.");
                    return;
                }

                var alunosAprovados = _servicoRelatorio.ListarAlunosAprovadosGeral(alunos);

                if (alunosAprovados.Count == 0)
                {
                    Console.WriteLine("Nenhum aluno aprovado no sistema.\n");
                }
                else
                {
                    Console.WriteLine($"Total de Alunos Aprovados: {alunosAprovados.Count}\n");
                    Console.WriteLine("┌────────────────────────────────────────────────┐");

                    foreach (var aluno in alunosAprovados)
                    {
                        double media = aluno.CalcularMedia();
                        Console.WriteLine($"│ {aluno.Nome.PadRight(30)} Média: {media:F2}");
                    }

                    Console.WriteLine("└────────────────────────────────────────────────┘");
                }
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        private void ExibirRelatorios()
        {
            Console.Clear();
            Console.WriteLine("═══════ RELATÓRIOS ═══════\n");
            Console.WriteLine("1. Relatório de Aluno");
            Console.WriteLine("2. Estatísticas Gerais");
            Console.WriteLine("3. Voltar ao Menu");
            Console.Write("\nOpção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    RelatorioAluno();
                    break;
                case "2":
                    EstatisticasGerais();
                    break;
                case "3":
                    return;
                default:
                    ExibirErro("Opção inválida!");
                    break;
            }

            PausarTela();
        }

        private void RelatorioAluno()
        {
            Console.Clear();
            Console.WriteLine("═══════ RELATÓRIO DO ALUNO ═══════\n");

            try
            {
                Console.Write("Matrícula do Aluno: ");
                int matricula = LerInteiro();

                var aluno = _servicoCadastro.ObterAluno(matricula);
                string relatorio = _servicoRelatorio.GerarRelatórioAluno(aluno);

                Console.WriteLine("\n" + relatorio);
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        private void EstatisticasGerais()
        {
            Console.Clear();
            Console.WriteLine("═══════ ESTATÍSTICAS GERAIS ═══════\n");

            try
            {
                var alunos = _servicoCadastro.ListarAlunos();
                var professores = _servicoCadastro.ListarProfessores();
                var disciplinas = _servicoCadastro.ListarDisciplinas();

                string stats = _servicoRelatorio.GerarEstatisticas(alunos, professores, disciplinas);
                Console.WriteLine(stats);
            }
            catch (Exception ex)
            {
                ExibirErro(ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // MÉTODOS AUXILIARES PARA ENTRADA DE DADOS
        // ═══════════════════════════════════════════════════════════════

        private DateTime LerDataValida()
        {
            while (true)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                {
                    return data;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Formato inválido! Use dd/MM/yyyy");
                Console.ResetColor();
                Console.Write("Data de Nascimento (dd/MM/yyyy): ");
            }
        }

        private int LerInteiro()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    return valor;
                }

                ExibirErroEntrada("número inteiro");
            }
        }

        private double LerDouble()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture, out double valor))
                {
                    return valor;
                }

                ExibirErroEntrada("número decimal");
            }
        }

        private decimal LerDecimal()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture, out decimal valor))
                {
                    return valor;
                }

                ExibirErroEntrada("valor decimal");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // MÉTODOS AUXILIARES PARA EXIBIÇÃO
        // ═══════════════════════════════════════════════════════════════

        private void ExibirSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {mensagem}");
            Console.ResetColor();
        }

        private void ExibirErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ {mensagem}");
            Console.ResetColor();
        }

        private void ExibirErroEntrada(string tipo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Entrada inválida! Digite um(a) {tipo} válido(a):");
            Console.ResetColor();
        }

        private void PausarTela()
        {
            Console.Write("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
