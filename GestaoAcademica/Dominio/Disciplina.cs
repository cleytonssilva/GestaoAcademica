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
        public Professor ProfessorResponsavel { get; set; }

        public Disciplina(string nome, string codigo, Professor professorResponsavel)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Codigo = codigo;
            ProfessorResponsavel = professorResponsavel;
        }
    }
}
