using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using GestaoAcademica.Servicos;
using System.Collections.Generic;

namespace GestaoAcademica.UI
{
    

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
            while (true)
            {
                Console.WriteLine("\nSistema de Gestão Acadêmica");
                Console.WriteLine("1. Cadastrar Aluno");
                Console.WriteLine("2. Cadastrar Professor");
                Console.WriteLine("3. Cadastrar Disciplina");
                Console.WriteLine("4. Matricular Aluno");
                Console.WriteLine("5. Atribuir Nota");
                Console.WriteLine("6. Listar Alunos Aprovados");
                Console.WriteLine("7. Sair");

                Console.Write("Opção: ");
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
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        private void CadastrarAluno()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Data de Nascimento (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("Matrícula: ");
            int matricula = int.Parse(Console.ReadLine());

            try
            {
                _servicoCadastro.CadastrarAluno(nome, email, dataNascimento, cpf, matricula);
                Console.WriteLine("Aluno cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar aluno: {ex.Message}");
            }
        }


        private void CadastrarProfessor()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Data de Nascimento (dd/MM/yyyy): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("Disciplina: ");
            string disciplina = Console.ReadLine();
            Console.Write("Salário: ");
            decimal salario = decimal.Parse(Console.ReadLine());

            try
            {
                _servicoCadastro.CadastrarProfessor(nome, email, dataNascimento, cpf, disciplina, salario);
                Console.WriteLine("Professor cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar professor: {ex.Message}");
            }
        }

        private void CadastrarDisciplina()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Código: ");
            string codigo = Console.ReadLine();

            Console.WriteLine("Selecione o professor responsável:");
            // Implementar a lógica para listar os professores cadastrados e permitir que o usuário selecione um
            Console.WriteLine("Professor não selecionado (implementar a lógica)");
            Professor professorResponsavel = null; // Substituir por um professor selecionado

            try
            {
                _servicoCadastro.CadastrarDisciplina(nome, codigo, professorResponsavel);
                Console.WriteLine("Disciplina cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar disciplina: {ex.Message}");
            }
        }

        private void MatricularAluno()
        {
            Console.Write("Matrícula do aluno: ");
            int matriculaAluno = int.Parse(Console.ReadLine());

            Console.Write("Código da disciplina: ");
            string codigoDisciplina = Console.ReadLine();

            // Implementar a lógica para obter o aluno e a disciplina com base nos dados informados

            Console.WriteLine("Aluno e disciplina não encontrados (implementar a lógica)");
            Aluno aluno = null; // Substituir pelo aluno encontrado
            Disciplina disciplina = null; // Substituir pela disciplina encontrada

            try
            {
                _servicoMatricula.MatricularAluno(aluno, disciplina);
                Console.WriteLine("Aluno matriculado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao matricular aluno: {ex.Message}");
            }
        }

        private void AtribuirNota()
        {
            Console.Write("Matrícula do aluno: ");
            int matriculaAluno = int.Parse(Console.ReadLine());

            Console.Write("Código da disciplina: ");
            string codigoDisciplina = Console.ReadLine();

            Console.Write("Nota: ");
            double nota = double.Parse(Console.ReadLine());

            // Implementar a lógica para obter o aluno e a disciplina com base nos dados informados
            Console.WriteLine("Aluno e disciplina não encontrados (implementar a lógica)");
            Aluno aluno = null; // Substituir pelo aluno encontrado
            Disciplina disciplina = null; // Substituir pela disciplina encontrada

            try
            {
                _servicoAvaliacao.AtribuirNota(aluno, disciplina, nota);
                Console.WriteLine("Nota atribuída com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atribuir nota: {ex.Message}");
            }
        }

        private void ListarAlunosAprovados()
        {
            Console.Write("Código da disciplina: ");
            string codigoDisciplina = Console.ReadLine();

            // Implementar a lógica para obter a disciplina com base no código informado
            Console.WriteLine("Disciplina não encontrada (implementar a lógica)");
            Disciplina disciplina = null; // Substituir pela disciplina encontrada

            try
            {
                List<Aluno> alunosAprovados = _servicoRelatorio.ListarAlunosAprovados(disciplina);

                if (alunosAprovados.Count == 0)
                {
                    Console.WriteLine("Nenhum aluno aprovado nesta disciplina.");
                }
                else
                {
                    Console.WriteLine("Alunos Aprovados:");
                    foreach (Aluno aluno in alunosAprovados)
                    {



                        Console.WriteLine(aluno.ObterInformacoes());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar alunos aprovados: {ex.Message}");
            }
        }
    }
}
