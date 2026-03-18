using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Dominio
{
    public class Professor : Pessoa
    {
        // Propriedades para compatibilidade com código antigo
        public string Disciplina { get; set; }

        // Novas propriedades para persistência completa
        public string DisciplinaPrincipal { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }
        public bool Ativo { get; set; } = true;

        public List<Disciplina> DisciplinasResponsavel { get; set; } = new List<Disciplina>();
        public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

        public Professor()
        {
            DataAdmissao = DateTime.UtcNow;
        }

        public Professor(string nome, string email, DateTime dataNascimento, string cpf, string disciplinaPrincipal, decimal salario)
            : base(nome, email, dataNascimento, cpf)
        {
            Disciplina = disciplinaPrincipal; // Para compatibilidade
            DisciplinaPrincipal = disciplinaPrincipal;
            Salario = salario;
            DataAdmissao = DateTime.UtcNow;
        }

        public override string ObterInformacoes()
        {
            var disciplina = !string.IsNullOrEmpty(DisciplinaPrincipal) ? DisciplinaPrincipal : Disciplina;
            return $"Professor: {Nome}, Disciplina: {disciplina}, Salário: {Salario:C}";
        }
    }
}

