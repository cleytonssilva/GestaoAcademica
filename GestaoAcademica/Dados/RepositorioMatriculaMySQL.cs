using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcademica.Dados
{
    /// <summary>
    /// Implementação do repositório de Matrículas com persistência em MySQL
    /// Nota: Requer instalação do pacote NuGet MySql.Data para funcionamento completo
    /// </summary>
    public class RepositorioMatriculaMySQL : IRepositorioMatricula
    {
        private readonly GestaoAcademicaContext _context;

        public RepositorioMatriculaMySQL(GestaoAcademicaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Adicionar(Matricula matricula)
        {
            if (matricula == null)
                throw new ArgumentNullException(nameof(matricula), "Matrícula não pode ser nula");

            try
            {
                _context.Matriculas.Add(matricula);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao adicionar matrícula no banco de dados", ex);
            }
        }

        public List<Matricula> ListarTodos()
        {
            try
            {
                return _context.Matriculas
                    .Where(m => m.Situacao == SituacaoMatricula.Ativa)
                    .OrderBy(m => m.Aluno.Nome)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao listar matrículas do banco de dados", ex);
            }
        }

        public List<Matricula> ObterPorAluno(int matriculaAluno)
        {
            if (matriculaAluno <= 0)
                throw new ArgumentException("Matrícula do aluno deve ser maior que zero", nameof(matriculaAluno));

            try
            {
                return _context.Matriculas
                    .Where(m => m.Aluno.Matricula == matriculaAluno && m.Situacao == SituacaoMatricula.Ativa)
                    .OrderBy(m => m.DataMatricula)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter matrículas do aluno", ex);
            }
        }

        public List<Matricula> ObterPorDisciplina(string codigoDisciplina)
        {
            if (string.IsNullOrWhiteSpace(codigoDisciplina))
                throw new ArgumentException("Código da disciplina não pode ser nulo ou vazio", nameof(codigoDisciplina));

            try
            {
                return _context.Matriculas
                    .Where(m => m.Turma.Disciplina.Codigo == codigoDisciplina && m.Situacao == SituacaoMatricula.Ativa)
                    .OrderBy(m => m.Aluno.Nome)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter matrículas da disciplina", ex);
            }
        }

        public bool VerificarMatriculaEmDisciplina(int matriculaAluno, string codigoDisciplina)
        {
            if (matriculaAluno <= 0)
                throw new ArgumentException("Matrícula do aluno deve ser maior que zero", nameof(matriculaAluno));

            if (string.IsNullOrWhiteSpace(codigoDisciplina))
                throw new ArgumentException("Código da disciplina não pode ser nulo ou vazio", nameof(codigoDisciplina));

            try
            {
                return _context.Matriculas
                    .Any(m => m.Aluno.Matricula == matriculaAluno &&
                              m.Turma.Disciplina.Codigo == codigoDisciplina &&
                              m.Situacao == SituacaoMatricula.Ativa);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao verificar matrícula em disciplina", ex);
            }
        }
    }
}
