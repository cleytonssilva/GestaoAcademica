using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    public class Matricula
    {
        public Guid Id { get; set; }
        public Aluno Aluno { get; set; }
        public Disciplina Disciplina { get; set; }
        public DateTime DataMatricula { get; set; }

        public Matricula(Aluno aluno, Disciplina disciplina)
        {
            Id = Guid.NewGuid();
            Aluno = aluno;
            Disciplina = disciplina;
            DataMatricula = DateTime.UtcNow;
        }
    }
}
