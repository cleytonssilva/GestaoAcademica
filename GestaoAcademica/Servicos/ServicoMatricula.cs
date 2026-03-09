using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno), "Aluno não pode ser nulo.");

            if (disciplina == null)
                throw new ArgumentNullException(nameof(disciplina), "Disciplina não pode ser nula.");

            // Verificar se aluno já está matriculado nesta disciplina
            if (_repositorioMatricula.VerificarMatriculaEmDisciplina(aluno.Matricula, disciplina.Codigo))
                throw new InvalidOperationException(
                    $"Aluno {aluno.Nome} já está matriculado em {disciplina.Nome}.");

            var matricula = new Matricula(aluno, disciplina);
            aluno.Matriculas.Add(matricula);
            _repositorioMatricula.Adicionar(matricula);
            return matricula;
        }

        
        public List<Matricula> ObterMatriculasAluno(int matriculaAluno)
        {
            return _repositorioMatricula.ObterPorAluno(matriculaAluno);
        }

        public List<Matricula> ObterMatriculasDisciplina(string codigoDisciplina)
        {
            return _repositorioMatricula.ObterPorDisciplina(codigoDisciplina);
        }

        
        public bool VerificarMatriculaEmDisciplina(int matriculaAluno, string codigoDisciplina)
        {
            return _repositorioMatricula.VerificarMatriculaEmDisciplina(matriculaAluno, codigoDisciplina);
        }

        public List<Matricula> ListarTodasAsMatriculas()
        {
            return _repositorioMatricula.ListarTodos();
        }
    }
}
