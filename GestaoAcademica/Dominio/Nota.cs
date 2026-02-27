using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    public class Nota
    {
        public Guid Id { get; set; }
        public Disciplina Disciplina { get; set; }
        public double Valor { get; set; }
        public DateTime DataAtribuicao { get; set; }

        public Nota(Disciplina disciplina, double valor)
        {
            Id = Guid.NewGuid();
            Disciplina = disciplina;
            Valor = valor;
            DataAtribuicao = DateTime.UtcNow;
        }
    }
}
