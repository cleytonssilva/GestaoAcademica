using System;
using System.Collections.Generic;
using System.Globalization;
using GestaoAcademica.Dominio;
using GestaoAcademica.Servicos;

namespace GestaoAcademica.UI
{
    /// <summary>
    /// Interface com usuário em terminal para o sistema de Gestão Acadêmica.
    /// Responsável pela interação com o usuário e apresentação de dados.
    /// </summary>
    public class TerminalUI
    {
        private readonly ServicoCadastro _servicoCadastro;
        private readonly ServicoMatricula _servicoMatricula;
        private readonly ServicoAvaliacao _servicoAvaliacao;
        private readonly ServicoRelatorio _servicoRelatorio;

        public TerminalUI(ServicoCadastro servicoCadastro, ServicoMatricula servicoMatricula, 
                         ServicoAvaliacao servicoAvaliacao, ServicoRelatorio servicoRelatorio)
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

                    ExecutarOpcaoMenu(opcao);
                }
                catch (Exception ex)
                {
                    ExibirErro($"Erro inesperado: {ex.Message}");
                    PausarTela();
                }
            }
        }

        private void ExecutarOpcaoMenu(string opcao)
        {
            switch (opcao)
            {
                case "1":
                    CadastrarAluno();
                    PausarTela();
                    break;
                case "2":
                    CadastrarProfessor();
                    PausarTela();
                    break;
                case "3":
                    CadastrarDisciplina();
                    PausarTela();
                    break;
                case "4":
                    MatricularAluno();
                    PausarTela();
                    break;
                case "5":
                    AtribuirNota();
                    PausarTela();
                    break;
                case "6":
                    ListarAlunosAprovados();
                    PausarTela();
                    break;
                case "7":
                    ExibirRelatorios();
                    break;
                case "8":
                    FinalizarSistema();
                    break;
                default:
                    ExibirErro("Opção inválida. Tente novamente.");
                    PausarTela();
                    break;
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
            Console.WriteLine("4. Matricular Aluno");
            Console.WriteLine("5. Atribuir Nota");
            Console.WriteLine("6. Listar Alunos Aprovados");
            Console.WriteLine("7. Relatórios");
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
                string nome = SolicitarEntrada("Nome Completo");
                string email = SolicitarEntrada("Email");
                DateTime dataNascimento = SolicitarData("Data de Nascimento (dd/MM/yyyy)");
                string cpf = SolicitarEntrada("CPF (XXX.XXX.XXX-XX)");
                int matricula = SolicitarInteiro("Matrícula (número único)");

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
                string nome = SolicitarEntrada("Nome Completo");
                string email = SolicitarEntrada("Email");
                DateTime dataNascimento = SolicitarData("Data de Nascimento (dd/MM/yyyy)");
                string cpf = SolicitarEntrada("CPF (XXX.XXX.XXX-XX)");
                string disciplina = SolicitarEntrada("Disciplina Principal");
                decimal salario = SolicitarDecimal("Salário (R$)");

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
                string nome = SolicitarEntrada("Nome da Disciplina");
                string codigo = SolicitarEntrada("Código (ex: MAT001)");

                var professor = SelecionarProfessor();
                if (professor == null)
                    return;

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
                int matriculaAluno = SolicitarInteiro("Matrícula do Aluno");
                var aluno = _servicoCadastro.ObterAluno(matriculaAluno);

                var disciplina = SelecionarDisciplina();
                if (disciplina == null)
                    return;

                _servicoMatricula.MatricularAluno(aluno, disciplina);
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
                int matriculaAluno = SolicitarInteiro("Matrícula do Aluno");
                var aluno = _servicoCadastro.ObterAluno(matriculaAluno);

                var matriculas = _servicoMatricula.ObterMatriculasAluno(matriculaAluno);
                if (matriculas.Count == 0)
                {
                    ExibirErro($"Aluno {aluno.Nome} não está matriculado em nenhuma disciplina.");
                    return;
                }

                var disciplina = SelecionarDisciplinaAluno(matriculas);
                if (disciplina == null)
                    return;

                double nota = SolicitarNota($"Nota em {disciplina.Nome} (0 a 10)");
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
                    ExibirTabelaAlunosAprovados(alunosAprovados);
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
                    PausarTela();
                    break;
                case "2":
                    EstatisticasGerais();
                    PausarTela();
                    break;
                case "3":
                    return;
                default:
                    ExibirErro("Opção inválida!");
                    PausarTela();
                    break;
            }
        }

        private void RelatorioAluno()
        {
            Console.Clear();
            Console.WriteLine("═══════ RELATÓRIO DO ALUNO ═══════\n");

            try
            {
                int matricula = SolicitarInteiro("Matrícula do Aluno");
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

        private void FinalizarSistema()
        {
            Console.WriteLine("\nSistema finalizado. Até logo!");
            Environment.Exit(0);
        }

        private Professor SelecionarProfessor()
        {
            var professores = _servicoCadastro.ListarProfessores();

            if (professores.Count == 0)
            {
                ExibirErro("Não há professores cadastrados.");
                return null;
            }

            var nomesProfessores = new List<string>();
            foreach (var professor in professores)
            {
                nomesProfessores.Add($"{professor.Nome} - {professor.Disciplina}");
            }

            ExibirLista("Professores Disponíveis", nomesProfessores);

            int indice = SolicitarIndice(professores.Count) - 1;
            return professores[indice];
        }

        private Disciplina SelecionarDisciplina()
        {
            var disciplinas = _servicoCadastro.ListarDisciplinas();

            if (disciplinas.Count == 0)
            {
                ExibirErro("Não há disciplinas cadastradas.");
                return null;
            }

            var nomesDisciplinas = new List<string>();
            foreach (var disciplina in disciplinas)
            {
                nomesDisciplinas.Add($"{disciplina.Nome} (Código: {disciplina.Codigo})");
            }

            ExibirLista("Disciplinas Disponíveis", nomesDisciplinas);

            int indice = SolicitarIndice(disciplinas.Count) - 1;
            return disciplinas[indice];
        }

        private Disciplina SelecionarDisciplinaAluno(List<Matricula> matriculas)
        {
            Console.WriteLine("\n--- Disciplinas do Aluno ---");
            for (int i = 0; i < matriculas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {matriculas[i].Disciplina.Nome}");
            }

            int indice = SolicitarIndice(matriculas.Count) - 1;
            return matriculas[indice].Disciplina;
        }

        private void ExibirTabelaAlunosAprovados(List<Aluno> alunosAprovados)
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

        private string SolicitarEntrada(string descricao)
        {
            Console.Write($"{descricao}: ");
            return Console.ReadLine();
        }

        private DateTime SolicitarData(string descricao)
        {
            while (true)
            {
                Console.Write($"{descricao}: ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                {
                    return data;
                }

                ExibirErroEntrada("formato dd/MM/yyyy");
            }
        }

        private int SolicitarInteiro(string descricao)
        {
            while (true)
            {
                Console.Write($"{descricao}: ");
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    return valor;
                }

                ExibirErroEntrada("número inteiro");
            }
        }

        private decimal SolicitarDecimal(string descricao)
        {
            while (true)
            {
                Console.Write($"{descricao}: ");
                if (decimal.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture, out decimal valor))
                {
                    return valor;
                }

                ExibirErroEntrada("valor decimal");
            }
        }

        private double SolicitarNota(string descricao)
        {
            while (true)
            {
                Console.Write($"{descricao}: ");
                if (double.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture, out double valor))
                {
                    return valor;
                }

                ExibirErroEntrada("valor de nota");
            }
        }

        private int SolicitarIndice(int maximo)
        {
            while (true)
            {
                Console.Write("\nSelecione o número: ");
                if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= maximo)
                {
                    return indice;
                }

                ExibirErro($"Opção inválida! Digite um número entre 1 e {maximo}.");
            }
        }

        private void ExibirLista(string titulo, List<string> itens)
        {
            Console.WriteLine($"\n--- {titulo} ---");
            for (int i = 0; i < itens.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {itens[i]}");
            }
        }

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
            Console.WriteLine($"Entrada inválida! Digite um {tipo} válido:");
            Console.ResetColor();
        }

        private void PausarTela()
        {
            Console.Write("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
