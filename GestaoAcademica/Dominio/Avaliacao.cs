using System;

namespace GestaoAcademica.Dominio
{
    /// <summary>
    /// Representa uma avaliação/nota atribuída a um aluno em uma matrícula
    /// </summary>
    public class Avaliacao
    {
        public Guid Id { get; set; }
        public Guid MatriculaId { get; set; }
        public Matricula Matricula { get; set; }
        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public Guid TurmaId { get; set; }
        public Turma Turma { get; set; }
        
        public decimal Valor { get; set; }
        public string TipoAvaliacao { get; set; }
        public decimal Peso { get; set; } = 1.0M;
        public DateTime DataAvaliacao { get; set; }
        public string Observacoes { get; set; }

        public Avaliacao()
        {
            Id = Guid.NewGuid();
            DataAvaliacao = DateTime.UtcNow;
        }

        public Avaliacao(Matricula matricula, Professor professor, decimal valor, string tipoAvaliacao = "Prova")
            : this()
        {
            Matricula = matricula;
            MatriculaId = matricula.Id;
            Professor = professor;
            ProfessorId = professor.Id;
            Turma = matricula.Turma;
            TurmaId = matricula.TurmaId;
            Valor = valor;
            TipoAvaliacao = tipoAvaliacao;
        }

        public override string ToString()
        {
            return $"Avaliação: {TipoAvaliacao} - Valor: {Valor} ({DataAvaliacao:dd/MM/yyyy})";
        }
    }
}
