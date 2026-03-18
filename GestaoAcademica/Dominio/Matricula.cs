using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    /// <summary>
    /// Representa a matrícula (inscrição) de um aluno em uma turma
    /// </summary>
    public class Matricula
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public Guid TurmaId { get; set; }
        public Turma Turma { get; set; }

        // Para compatibilidade com código antigo
        public Disciplina Disciplina { get; set; }

        public DateTime DataMatricula { get; set; }
        public DateTime? DataConclusao { get; set; }
        public SituacaoMatricula Situacao { get; set; } = SituacaoMatricula.Ativa;

        public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

        public Matricula()
        {
            Id = Guid.NewGuid();
            DataMatricula = DateTime.UtcNow;
        }

        public Matricula(Aluno aluno, Turma turma)
            : this()
        {
            Aluno = aluno;
            AlunoId = aluno.Id;
            Turma = turma;
            TurmaId = turma.Id;
            Disciplina = turma.Disciplina; // Para compatibilidade
        }

        /// <summary>
        /// Construtor compatível com código antigo que usa Disciplina
        /// </summary>
        public Matricula(Aluno aluno, Disciplina disciplina)
            : this()
        {
            Aluno = aluno;
            AlunoId = aluno.Id;
            Disciplina = disciplina;
            // Nota: Turma não será preenchida, para compatibilidade com código antigo
        }

        /// <summary>
        /// Calcula a média do aluno nesta matrícula
        /// </summary>
        public double CalcularMedia()
        {
            if (Avaliacoes.Count == 0) return 0;

            var totalPeso = Avaliacoes.Sum(a => (double)a.Peso);
            if (totalPeso == 0) return 0;

            var mediasPonderadas = Avaliacoes.Sum(a => (double)a.Valor * (double)a.Peso);
            return mediasPonderadas / totalPeso;
        }
    }

    /// <summary>
    /// Enumeração para situação de matrícula
    /// </summary>
    public enum SituacaoMatricula
    {
        Ativa,
        Concluida,
        Cancelada
    }
}
