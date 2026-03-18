using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcademica.Dados
{
    /// <summary>
    /// Implementação do repositório de Disciplinas com persistência em MySQL
    /// Nota: Requer instalação do pacote NuGet MySql.Data para funcionamento completo
    /// </summary>
    public class RepositorioDisciplinaMySQL : IRepositorioDisciplina
    {
        private readonly GestaoAcademicaContext _context;

        public RepositorioDisciplinaMySQL(GestaoAcademicaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Adicionar(Disciplina disciplina)
        {
            if (disciplina == null)
                throw new ArgumentNullException(nameof(disciplina), "Disciplina não pode ser nula");

            if (VerificarCodigoDuplicado(disciplina.Codigo))
                throw new InvalidOperationException($"Disciplina com código {disciplina.Codigo} já existe");

            try
            {
                _context.Disciplinas.Add(disciplina);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao adicionar disciplina no banco de dados", ex);
            }
        }

        public Disciplina ObterPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Código não pode ser nulo ou vazio", nameof(codigo));

            try
            {
                return _context.Disciplinas
                    .FirstOrDefault(d => d.Codigo == codigo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter disciplina do banco de dados", ex);
            }
        }

        public bool VerificarCodigoDuplicado(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Código não pode ser nulo ou vazio", nameof(codigo));

            try
            {
                return _context.Disciplinas.Any(d => d.Codigo == codigo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao verificar código no banco de dados", ex);
            }
        }

        public List<Disciplina> ListarTodos()
        {
            try
            {
                return _context.Disciplinas
                    .Where(d => d.Ativa)
                    .OrderBy(d => d.Nome)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao listar disciplinas do banco de dados", ex);
            }
        }
    }
}
