using System;
using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using GestaoAcademica.Servicos;
using GestaoAcademica.UI;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcademica.Dados
{
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