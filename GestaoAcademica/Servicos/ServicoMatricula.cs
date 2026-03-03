using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;

namespace GestaoAcademica.Servicos
{
    /// <summary>
    /// Serviço responsável pelo gerenciamento de matrículas de alunos em disciplinas.
    /// Aplica regras de negócio para matrícula e controle de duplicidades.
    /// </summary>
    public class ServicoMatricula
    {
        private readonly IRepositorioMatricula _repositorioMatricula;

        public ServicoMatricula(IRepositorioMatricula repositorioMatricula)
        {
            _repositorioMatricula = repositorioMatricula;
        }

        /// <summary>
        /// Matricula um aluno em uma disciplina com validações.
        /// </summary>
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

        /// <summary>
        /// Obtém todas as matrículas de um aluno.
        /// </summary>
        public List<Matricula> ObterMatriculasAluno(int matriculaAluno)
        {
            return _repositorioMatricula.ObterPorAluno(matriculaAluno);
        }

        /// <summary>
        /// Obtém todas as matrículas de uma disciplina.
        /// </summary>
        public List<Matricula> ObterMatriculasDisciplina(string codigoDisciplina)
        {
            return _repositorioMatricula.ObterPorDisciplina(codigoDisciplina);
        }

        /// <summary>
        /// Verifica se um aluno está matriculado em uma disciplina.
        /// </summary>
        public bool VerificarMatriculaEmDisciplina(int matriculaAluno, string codigoDisciplina)
        {
            return _repositorioMatricula.VerificarMatriculaEmDisciplina(matriculaAluno, codigoDisciplina);
        }

        /// <summary>
        /// Lista todas as matrículas do sistema.
        /// </summary>
        public List<Matricula> ListarTodasAsMatriculas()
        {
            return _repositorioMatricula.ListarTodos();
        }
    }
}
