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
        public List<Nota> Notas { get; set; } = new List<Nota>();
        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();

        public Aluno(string nome, string email, DateTime dataNascimento, string cpf, int matricula) : base(nome, email, dataNascimento, cpf)
        {
            Matricula = matricula;
        }

        public double CalcularMedia()
        {
            if (Notas.Count == 0) return 0;
            return Notas.Average(n => n.Valor);
        }

        public override string ObterInformacoes()
        {
            return $"Aluno: {Nome}, Matrícula: {Matricula}, Média: {CalcularMedia()}";
        }
    }
}
