using System;
using System.Collections.Generic;

namespace GestaoAcademica.Dominio
{
    /// <summary>
    /// Representa uma turma (classe/seção) de uma disciplina em um semestre
    /// </summary>
    public class Turma
    {
        public Guid Id { get; set; }
        public Guid DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public string NumeroTurma { get; set; }
        public int Semestre { get; set; }
        public int Ano { get; set; }
        public int CapacidadeMaxima { get; set; } = 40;
        public bool Ativa { get; set; } = true;
        public DateTime DataCriacao { get; set; }

        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

        public Turma()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
        }

        public Turma(Disciplina disciplina, string numeroTurma, int semestre, int ano)
            : this()
        {
            Disciplina = disciplina;
            DisciplinaId = disciplina.Id;
            NumeroTurma = numeroTurma;
            Semestre = semestre;
            Ano = ano;
        }

        public override string ToString()
        {
            return $"Turma {NumeroTurma} - {Disciplina?.Nome} ({Semestre}/{Ano})";
        }
    }
}
