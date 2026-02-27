using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using GestaoAcademica.Servicos;
using GestaoAcademica.UI;
using System;
using System.Collections.Generic;
using System.Linq;

// GestaoAcademica.Dominio
namespace GestaoAcademica.Dominio
{
    public abstract class Pessoa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }

        public Pessoa(string nome, string email, DateTime dataNascimento, string cpf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            CPF = cpf;
        }

        public abstract string ObterInformacoes();
    }

    public class Aluno : Pessoa
    {
        public int Matricula { get; set; }
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();

        public Aluno(string nome, string email, DateTime dataNascimento, string cpf, int matricula) : base(nome, email, dataNascimento, cpf)
        {
            Matricula = matricula;
        }

        public double CalcularMedia()
        {
            if (Notas.Count == 0) return 0;
            return Notas.Average(n => n.Valor);
        }

        public override string ObterInformacoes()
        {
            return $"Aluno: {Nome}, Matrícula: {Matricula}, Média: {CalcularMedia()}";
        }
    }

    public class Professor : Pessoa
    {
        public string Disciplina { get; set; }
        public decimal Salario { get; set; }

        public Professor(string nome, string email, DateTime dataNascimento, string cpf, string disciplina, decimal salario) : base(nome, email, dataNascimento, cpf)
        {
            Disciplina = disciplina;
            Salario = salario;
        }

        public override string ObterInformacoes()
        {
            return $"Professor: {Nome}, Disciplina: {Disciplina}, Salário: {Salario:C}";
        }
    }

    public class Disciplina
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public Professor ProfessorResponsavel { get; set; }

        public Disciplina(string nome, string codigo, Professor professorResponsavel)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Codigo = codigo;
            ProfessorResponsavel = professorResponsavel;
        }
    }

    public class Matricula
    {
        public Guid Id { get; set; }
        public Aluno Aluno { get; set; }
        public Disciplina Disciplina { get; set; }
        public DateTime DataMatricula { get; set; }

        public Matricula(Aluno aluno, Disciplina disciplina)
        {
            Id = Guid.NewGuid();
            Aluno = aluno;
            Disciplina = disciplina;
            DataMatricula = DateTime.UtcNow;
        }
    }

    public class Nota
    {
        public Guid Id { get; set; }
        public Disciplina Disciplina { get; set; }
        public double Valor { get; set; }
        public DateTime DataAtribuicao { get; set; }

        public Nota(Disciplina disciplina, double valor)
        {
            Id = Guid.NewGuid();
            Disciplina = disciplina;
            Valor = valor;
            DataAtribuicao = DateTime.UtcNow;
        }
    }
}

// GestaoAcademica.Servicos
namespace GestaoAcademica.Servicos
{
    using GestaoAcademica.Dados;
    using GestaoAcademica.Dominio;
    using System;
    using System.Collections.Generic;

    public class ServicoCadastro
    {
        private readonly IRepositorioAluno _repositorioAluno;
        private readonly IRepositorioProfessor _repositorioProfessor;
        private readonly IRepositorioDisciplina _repositorioDisciplina;


        public ServicoCadastro(IRepositorioAluno repositorioAluno, IRepositorioProfessor repositorioProfessor, IRepositorioDisciplina repositorioDisciplina)
        {
            _repositorioAluno = repositorioAluno;
            _repositorioProfessor = repositorioProfessor;
            _repositorioDisciplina = repositorioDisciplina;
        }

        public Aluno CadastrarAluno(string nome, string email, DateTime dataNascimento, string cpf, int matricula)
        {
            var aluno = new Aluno(nome, email, dataNascimento, cpf, matricula);
            _repositorioAluno.Adicionar(aluno);
            return aluno;
        }

        public Professor CadastrarProfessor(string nome, string email, DateTime dataNascimento, string cpf, string disciplina, decimal salario)
        {
            var professor = new Professor(nome, email, dataNascimento, cpf, disciplina, salario);
            _repositorioProfessor.Adicionar(professor);
            return professor;
        }

        public Disciplina CadastrarDisciplina(string nome, string codigo, Professor professorResponsavel)
        {
            var disciplina = new Disciplina(nome, codigo, professorResponsavel);
            _repositorioDisciplina.Adicionar(disciplina);
            return disciplina;
        }
    }

    public class ServicoMatricula
    {
        private readonly IRepositorioMatricula _repositorioMatricula;

        public ServicoMatricula(IRepositorioMatricula repositorioMatricula)
        {
            _repositorioMatricula = repositorioMatricula;
        }

        public Matricula MatricularAluno(Aluno aluno, Disciplina disciplina)
        {
            var matricula = new Matricula(aluno, disciplina);
            aluno.Matriculas.Add(matricula);
            _repositorioMatricula.Adicionar(matricula);
            return matricula;
        }
    }

    public class ServicoAvaliacao
    {
        public void AtribuirNota(Aluno aluno, Disciplina disciplina, double valor)
        {
            var nota = new Nota(disciplina, valor);
            aluno.Notas.Add(nota);
        }
    }

    public class ServicoRelatorio
    {
        public List<Aluno> ListarAlunosAprovados(Disciplina disciplina)
        {
            // Lógica para listar alunos aprovados em uma disciplina
            return new List<Aluno>(); // Implementar a lógica real
        }
    }
}



// GestaoAcademica.Dados
namespace GestaoAcademica.Dados
{
    using GestaoAcademica.Dominio;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepositorioAluno
    {
        void Adicionar(Aluno aluno);
        Aluno ObterPorMatricula(int matricula);
        List<Aluno> ListarTodos();
    }

    public interface IRepositorioProfessor
    {
        void Adicionar(Professor professor);
        Professor ObterPorCPF(string cpf);
        List<Professor> ListarTodos();
    }

    public interface IRepositorioDisciplina
    {
        void Adicionar(Disciplina disciplina);
        Disciplina ObterPorCodigo(string codigo);
        List<Disciplina> ListarTodos();
    }

    public interface IRepositorioMatricula
    {
        void Adicionar(Matricula matricula);
        List<Matricula> ListarTodos();
    }

    // Implementações em memória (para simplificar o exemplo)

    public class RepositorioAlunoEmMemoria : IRepositorioAluno
    {
        private List<Aluno> _alunos = new List<Aluno>();

        public void Adicionar(Aluno aluno)
        {
            _alunos.Add(aluno);
        }

        public Aluno ObterPorMatricula(int matricula)
        {
            return _alunos.FirstOrDefault(a => a.Matricula == matricula);
        }

        public List<Aluno> ListarTodos()
        {
            return _alunos;
        }
    }

    public class RepositorioProfessorEmMemoria : IRepositorioProfessor
    {
        private List<Professor> _professores = new List<Professor>();

        public void Adicionar(Professor professor)
        {
            _professores.Add(professor);
            

}

      
    public Professor ObterPorCPF(string cpf)
        {
            return _professores.FirstOrDefault(p => p.CPF == cpf);
        }

        public List<Professor> ListarTodos()
        {
            return _professores;
        }
    }

    public class RepositorioDisciplinaEmMemoria : IRepositorioDisciplina
    {
        private List<Disciplina> _disciplinas = new List<Disciplina>();

        public void Adicionar(Disciplina disciplina)
        {
            _disciplinas.Add(disciplina);
        }

        public Disciplina ObterPorCodigo(string codigo)
        {
            return _disciplinas.FirstOrDefault(d => d.Codigo == codigo);
        }

        public List<Disciplina> ListarTodos()
        {
            return _disciplinas;
        }
    }

    public class RepositorioMatriculaEmMemoria : IRepositorioMatricula
    {
        private List<Matricula> _matriculas = new List<Matricula>();

        public void Adicionar(Matricula matricula)
        {
            _matriculas.Add(matricula);
        }

        public List<Matricula> ListarTodos()
        {
            return _matriculas;
        }
    }
}


// GestaoAcademica.UI
namespace GestaoAcademica.UI
{
    using GestaoAcademica.Dados;
    using GestaoAcademica.Dominio;
    using GestaoAcademica.Servicos;
    using System;
    using System.Collections.Generic;

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



// Program.cs
//using GestaoAcademica.UI;
//using GestaoAcademica.Servicos;
//using GestaoAcademica.Dados;

public class Program
{
    public static void Main(string[] args)
    {
        // Inicialização dos repositórios
        IRepositorioAluno repositorioAluno = new RepositorioAlunoEmMemoria();
        IRepositorioProfessor repositorioProfessor = new RepositorioProfessorEmMemoria();
        IRepositorioDisciplina repositorioDisciplina = new RepositorioDisciplinaEmMemoria();
        IRepositorioMatricula repositorioMatricula = new RepositorioMatriculaEmMemoria();

        // Inicialização dos serviços
        ServicoCadastro servicoCadastro = new ServicoCadastro(repositorioAluno, repositorioProfessor, repositorioDisciplina);
        ServicoMatricula servicoMatricula = new ServicoMatricula(repositorioMatricula);
        ServicoAvaliacao servicoAvaliacao = new ServicoAvaliacao();
        ServicoRelatorio servicoRelatorio = new ServicoRelatorio();

        // Inicialização da interface do usuário
        TerminalUI terminalUI = new TerminalUI(servicoCadastro, servicoMatricula, servicoAvaliacao, servicoRelatorio);

        // Execução do sistema
        terminalUI.Executar();
    }
}