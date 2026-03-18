using System;
using GestaoAcademica.Dominio;
using System.Collections.Generic;

namespace GestaoAcademica.Dados
{
    /// <summary>
    /// Contexto de dados para persistência com MySQL
    /// Fornece acesso às coleções de entidades
    /// </summary>
    public class GestaoAcademicaContext
    {
        // Coleções em memória para compatibilidade imediata
        // Serão substituídas por DbSet quando EF6 estiver configurado
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public List<Professor> Professores { get; set; } = new List<Professor>();
        public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
        public List<Turma> Turmas { get; set; } = new List<Turma>();
        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

        private string _connectionString;

        public GestaoAcademicaContext()
        {
            _connectionString = ConexaoBancoDados.ObterConnectionString();
            Inicializar();
        }

        public GestaoAcademicaContext(string connectionString)
        {
            _connectionString = connectionString;
            Inicializar();
        }

        private void Inicializar()
        {
            // Inicializa as coleções
            Alunos = new List<Aluno>();
            Professores = new List<Professor>();
            Disciplinas = new List<Disciplina>();
            Turmas = new List<Turma>();
            Matriculas = new List<Matricula>();
            Avaliacoes = new List<Avaliacao>();
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public void SaveChanges()
        {
            // TODO: Implementar persistência em MySQL quando EF6 estiver configurado
            // Por enquanto, os dados estão em memória
        }
    }
}
