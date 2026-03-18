using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    public class Aluno : Pessoa
    {
        public int Matricula { get; set; }
        public DateTime DataMatricula { get; set; }
        public bool Ativo { get; set; } = true;

        // Coleções
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();

        public Aluno()
        {
            DataMatricula = DateTime.UtcNow;
        }

        public Aluno(string nome, string email, DateTime dataNascimento, string cpf, int matricula)
            : base(nome, email, dataNascimento, cpf)
        {
            Matricula = matricula;
            DataMatricula = DateTime.UtcNow;
        }

        /// <summary>
        /// Calcula a média geral do aluno (compatível com coleção Notas)
        /// </summary>
        public double CalcularMedia()
        {
            // Tenta usar Notas primeiro (compatibilidade com código antigo)
            if (Notas.Count > 0) 
                return Notas.Average(n => n.Valor);

            // Se não houver Notas, calcula a partir de Matriculas/Avaliacoes
            if (Matriculas.Count == 0) return 0;

            var todasAsAvaliacoes = Matriculas.SelectMany(m => m.Avaliacoes).ToList();
            if (todasAsAvaliacoes.Count == 0) return 0;

            return todasAsAvaliacoes.Average(a => (double)a.Valor);
        }

        /// <summary>
        /// Calcula a média em uma disciplina específica
        /// </summary>
        public double CalcularMediaDisciplina(Guid disciplinaId)
        {
            var avaliacoesMatricula = Matriculas
                .Where(m => m.Turma.DisciplinaId == disciplinaId)
                .SelectMany(m => m.Avaliacoes)
                .ToList();

            if (avaliacoesMatricula.Count == 0) return 0;
            return avaliacoesMatricula.Average(a => (double)a.Valor);
        }

        public override string ToString()
        {
            return $"Aluno: {Nome}, Matrícula: {Matricula}, Média: {CalcularMedia():F2}";
        }

        public override string ObterInformacoes()
        {
            return ToString();
        }
    }
}
