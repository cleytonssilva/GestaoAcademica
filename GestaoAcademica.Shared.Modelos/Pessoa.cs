using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    public abstract class Pessoa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public DateTime DataCadastro { get; set; }

        public Pessoa()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.UtcNow;
        }

        public Pessoa(string nome, string email, DateTime dataNascimento, string cpf)
            : this()
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            CPF = cpf;
        }

        public abstract string ObterInformacoes();
    }
}
