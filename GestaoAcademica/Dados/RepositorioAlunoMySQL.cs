using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcademica.Dados
{
    /// <summary>
    /// Implementação do repositório de Alunos com persistência em MySQL
    /// Nota: Requer instalação do pacote NuGet MySql.Data para funcionamento completo
    /// </summary>
    public class RepositorioAlunoMySQL : IRepositorioAluno
    {
        private readonly GestaoAcademicaContext _context;

        public RepositorioAlunoMySQL(GestaoAcademicaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Adicionar(Aluno aluno)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno), "Aluno não pode ser nulo");

            if (VerificarMatriculaDuplicada(aluno.Matricula))
                throw new InvalidOperationException($"Aluno com matrícula {aluno.Matricula} já existe");

            if (VerificarCPFDuplicado(aluno.CPF))
                throw new InvalidOperationException($"Aluno com CPF {aluno.CPF} já existe");

            try
            {
                _context.Alunos.Add(aluno);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao adicionar aluno no banco de dados", ex);
            }
        }

        public Aluno ObterPorMatricula(int matricula)
        {
            try
            {
                return _context.Alunos
                    .FirstOrDefault(a => a.Matricula == matricula);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter aluno do banco de dados", ex);
            }
        }

        public Aluno ObterPorCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser nulo ou vazio", nameof(cpf));

            try
            {
                return _context.Alunos
                    .FirstOrDefault(a => a.CPF == cpf);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter aluno do banco de dados", ex);
            }
        }

        public bool VerificarMatriculaDuplicada(int matricula)
        {
            try
            {
                return _context.Alunos.Any(a => a.Matricula == matricula);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao verificar matrícula no banco de dados", ex);
            }
        }

        public bool VerificarCPFDuplicado(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser nulo ou vazio", nameof(cpf));

            try
            {
                return _context.Alunos.Any(a => a.CPF == cpf);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao verificar CPF no banco de dados", ex);
            }
        }

        public List<Aluno> ListarTodos()
        {
            try
            {
                return _context.Alunos
                    .Where(a => a.Ativo)
                    .OrderBy(a => a.Nome)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao listar alunos do banco de dados", ex);
            }
        }
    }
}
