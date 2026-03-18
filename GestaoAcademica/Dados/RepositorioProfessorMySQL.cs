using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcademica.Dados
{
    /// <summary>
    /// Implementação do repositório de Professores com persistência em MySQL
    /// Nota: Requer instalação do pacote NuGet MySql.Data para funcionamento completo
    /// </summary>
    public class RepositorioProfessorMySQL : IRepositorioProfessor
    {
        private readonly GestaoAcademicaContext _context;

        public RepositorioProfessorMySQL(GestaoAcademicaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Adicionar(Professor professor)
        {
            if (professor == null)
                throw new ArgumentNullException(nameof(professor), "Professor não pode ser nulo");

            if (VerificarCPFDuplicado(professor.CPF))
                throw new InvalidOperationException($"Professor com CPF {professor.CPF} já existe");

            try
            {
                _context.Professores.Add(professor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao adicionar professor no banco de dados", ex);
            }
        }

        public Professor ObterPorCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser nulo ou vazio", nameof(cpf));

            try
            {
                return _context.Professores
                    .FirstOrDefault(p => p.CPF == cpf);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter professor do banco de dados", ex);
            }
        }

        public bool VerificarCPFDuplicado(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser nulo ou vazio", nameof(cpf));

            try
            {
                return _context.Professores.Any(p => p.CPF == cpf);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao verificar CPF no banco de dados", ex);
            }
        }

        public List<Professor> ListarTodos()
        {
            try
            {
                return _context.Professores
                    .Where(p => p.Ativo)
                    .OrderBy(p => p.Nome)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao listar professores do banco de dados", ex);
            }
        }
    }
}
