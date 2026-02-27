using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAcademica.Servicos
{
    public class ServicoMatricula
    {
        private readonly IRepositorioMatricula _repositorioMatricula;

        public ServicoMatricula(IRepositorioMatricula repositorioMatricula)
        {
            _repositorioMatricula = repositorioMatricula;
        }

        public Matricula MatricularAluno(Aluno aluno, Disciplina disciplina)
        {
            var matricula = new Matricula(aluno, disciplina);
            aluno.Matriculas.Add(matricula);
            _repositorioMatricula.Adicionar(matricula);
            return matricula;
        }
    }
}
