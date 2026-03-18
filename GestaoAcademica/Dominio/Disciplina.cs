using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    public class Disciplina
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public Guid ProfessorResponsavelId { get; set; }
        public Professor ProfessorResponsavel { get; set; }
        public string Descricao { get; set; }
        public bool Ativa { get; set; } = true;
        public DateTime DataCriacao { get; set; }

        public List<Turma> Turmas { get; set; } = new List<Turma>();

        public Disciplina()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
        }

        public Disciplina(string nome, string codigo, Professor professorResponsavel)
            : this()
        {
            Nome = nome;
            Codigo = codigo;
            ProfessorResponsavel = professorResponsavel;
            ProfessorResponsavelId = professorResponsavel.Id;
        }

        public override string ToString()
        {
            return $"{Codigo} - {Nome}";
        }
    }
}

