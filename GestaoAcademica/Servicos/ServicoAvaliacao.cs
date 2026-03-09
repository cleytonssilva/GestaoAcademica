using GestaoAcademica.Dominio;
using System;
using System.Linq;


namespace GestaoAcademica.Servicos
{
    
    public class ServicoAvaliacao
    {
        
        public Nota AtribuirNota(Aluno aluno, Disciplina disciplina, double valor)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno), "Aluno não pode ser nulo.");

            if (disciplina == null)
                throw new ArgumentNullException(nameof(disciplina), "Disciplina não pode ser nula.");

            // Validar valor da nota
            ValidadorAcademico.ValidarNota(valor);

            // Verificar se aluno está matriculado na disciplina
            var jaExisteNota = aluno.Notas.FirstOrDefault(n => n.Disciplina.Codigo == disciplina.Codigo);
            if (jaExisteNota != null)
            {
                throw new InvalidOperationException(
                    $"Aluno já possui nota em {disciplina.Nome}. Considere atualizar a nota existente.");
            }

            var nota = new Nota(disciplina, valor);
            aluno.Notas.Add(nota);
            return nota;
        }

        
        public Nota AtualizarNota(Aluno aluno, string codigoDisciplina, double novoValor)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno), "Aluno não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(codigoDisciplina))
                throw new ArgumentException("Código da disciplina não pode ser vazio.");

            
            ValidadorAcademico.ValidarNota(novoValor);

            var nota = aluno.Notas.FirstOrDefault(n => n.Disciplina.Codigo == codigoDisciplina);
            if (nota == null)
                throw new InvalidOperationException(
                    $"Aluno não possui nota para a disciplina {codigoDisciplina}.");

            nota.Valor = novoValor;
            return nota;
        }

        
        public Nota ObterNota(Aluno aluno, string codigoDisciplina)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));

            return aluno.Notas.FirstOrDefault(n => n.Disciplina.Codigo == codigoDisciplina);
        }

       
        public bool RemoverNota(Aluno aluno, string codigoDisciplina)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));

            var nota = aluno.Notas.FirstOrDefault(n => n.Disciplina.Codigo == codigoDisciplina);
            if (nota == null)
                return false;

            return aluno.Notas.Remove(nota);
        }

        
        public double CalcularMedia(Aluno aluno)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));

            return aluno.CalcularMedia();
        }

        public string ObterSituacao(Aluno aluno)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));

            return ValidadorAcademico.CalcularSituacao(aluno.CalcularMedia());
        }
    }
}
