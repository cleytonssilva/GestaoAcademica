using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Servicos
{
    public class ServicoAvaliacao
    {
        public void AtribuirNota(Aluno aluno, Disciplina disciplina, double valor)
        {
            var nota = new Nota(disciplina, valor);
            aluno.Notas.Add(nota);
        }
    }
}
