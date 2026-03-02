using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    public class Professor : Pessoa
    {
        public string Disciplina { get; set; }
        public decimal Salario { get; set; }

        public Professor(string nome, string email, DateTime dataNascimento, string cpf, string disciplina, decimal salario) : base(nome, email, dataNascimento, cpf)
        {
            Disciplina = disciplina;
            Salario = salario;
        }

        public override string ObterInformacoes()
        {
            return $"Professor: {Nome}, Disciplina: {Disciplina}, Salário: {Salario:C}";
        }

    }
}
