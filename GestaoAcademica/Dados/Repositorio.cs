using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcademica.Dados
{
    public interface IRepositorioAluno
    {
        void Adicionar(Aluno aluno);
        Aluno ObterPorMatricula(int matricula);
        Aluno ObterPorCPF(string cpf);
        bool VerificarMatriculaDuplicada(int matricula);
        bool VerificarCPFDuplicado(string cpf);
        List<Aluno> ListarTodos();
    }

    public interface IRepositorioProfessor
    {
        void Adicionar(Professor professor);
        Professor ObterPorCPF(string cpf);
        bool VerificarCPFDuplicado(string cpf);
        List<Professor> ListarTodos();
    }

    public interface IRepositorioDisciplina
    {
        void Adicionar(Disciplina disciplina);
        Disciplina ObterPorCodigo(string codigo);
        bool VerificarCodigoDuplicado(string codigo);
        List<Disciplina> ListarTodos();
    }

    public interface IRepositorioMatricula
    {
        void Adicionar(Matricula matricula);
        List<Matricula> ListarTodos();
        List<Matricula> ObterPorAluno(int matriculaAluno);
        List<Matricula> ObterPorDisciplina(string codigoDisciplina);
        bool VerificarMatriculaEmDisciplina(int matriculaAluno, string codigoDisciplina);
    }

    
    public class RepositorioAlunoEmMemoria : IRepositorioAluno
    {
        private List<Aluno> _alunos = new List<Aluno>();

        public void Adicionar(Aluno aluno)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));

            _alunos.Add(aluno);
        }

        public Aluno ObterPorMatricula(int matricula)
        {
            return _alunos.FirstOrDefault(a => a.Matricula == matricula);
        }

        public Aluno ObterPorCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return null;

            return _alunos.FirstOrDefault(a => a.CPF.Equals(cpf, System.StringComparison.OrdinalIgnoreCase));
        }

        public bool VerificarMatriculaDuplicada(int matricula)
        {
            return _alunos.Any(a => a.Matricula == matricula);
        }

        public bool VerificarCPFDuplicado(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            return _alunos.Any(a => a.CPF.Equals(cpf, System.StringComparison.OrdinalIgnoreCase));
        }

        public List<Aluno> ListarTodos()
        {
            return new List<Aluno>(_alunos);
        }
    }

    public class RepositorioProfessorEmMemoria : IRepositorioProfessor
    {
        private List<Professor> _professores = new List<Professor>();

        public void Adicionar(Professor professor)
        {
            if (professor == null)
                throw new ArgumentNullException(nameof(professor));

            _professores.Add(professor);
        }

        public Professor ObterPorCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return null;

            return _professores.FirstOrDefault(p => p.CPF.Equals(cpf, System.StringComparison.OrdinalIgnoreCase));
        }

        public bool VerificarCPFDuplicado(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            return _professores.Any(p => p.CPF.Equals(cpf, System.StringComparison.OrdinalIgnoreCase));
        }

        public List<Professor> ListarTodos()
        {
            return new List<Professor>(_professores);
        }
    }

    public class RepositorioDisciplinaEmMemoria : IRepositorioDisciplina
    {
        private List<Disciplina> _disciplinas = new List<Disciplina>();

        public void Adicionar(Disciplina disciplina)
        {
            if (disciplina == null)
                throw new ArgumentNullException(nameof(disciplina));

            _disciplinas.Add(disciplina);
        }

        public Disciplina ObterPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return null;

            return _disciplinas.FirstOrDefault(d => d.Codigo.Equals(codigo, System.StringComparison.OrdinalIgnoreCase));
        }

        public bool VerificarCodigoDuplicado(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            return _disciplinas.Any(d => d.Codigo.Equals(codigo, System.StringComparison.OrdinalIgnoreCase));
        }

        public List<Disciplina> ListarTodos()
        {
            return new List<Disciplina>(_disciplinas);
        }
    }

    public class RepositorioMatriculaEmMemoria : IRepositorioMatricula
    {
        private List<Matricula> _matriculas = new List<Matricula>();

        public void Adicionar(Matricula matricula)
        {
            if (matricula == null)
                throw new ArgumentNullException(nameof(matricula));

            _matriculas.Add(matricula);
        }

        public List<Matricula> ListarTodos()
        {
            return new List<Matricula>(_matriculas);
        }

        public List<Matricula> ObterPorAluno(int matriculaAluno)
        {
            return _matriculas
                .Where(m => m.Aluno != null && m.Aluno.Matricula == matriculaAluno)
                .ToList();
        }

        public List<Matricula> ObterPorDisciplina(string codigoDisciplina)
        {
            if (string.IsNullOrWhiteSpace(codigoDisciplina))
                return new List<Matricula>();

            return _matriculas
                .Where(m => m.Disciplina != null && 
                       m.Disciplina.Codigo.Equals(codigoDisciplina, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool VerificarMatriculaEmDisciplina(int matriculaAluno, string codigoDisciplina)
        {
            if (string.IsNullOrWhiteSpace(codigoDisciplina))
                return false;

            return _matriculas.Any(m => 
                m.Aluno != null && 
                m.Aluno.Matricula == matriculaAluno &&
                m.Disciplina != null &&
                m.Disciplina.Codigo.Equals(codigoDisciplina, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}