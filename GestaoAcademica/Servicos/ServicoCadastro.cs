using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;

namespace GestaoAcademica.Servicos
{
    /// <summary>
    /// Serviço responsável pelo cadastro de entidades acadêmicas.
    /// Aplicar validações e regras de negócio no cadastro.
    /// </summary>
    public class ServicoCadastro
    {
        private readonly IRepositorioAluno _repositorioAluno;
        private readonly IRepositorioProfessor _repositorioProfessor;
        private readonly IRepositorioDisciplina _repositorioDisciplina;


        public ServicoCadastro(IRepositorioAluno repositorioAluno, IRepositorioProfessor repositorioProfessor, IRepositorioDisciplina repositorioDisciplina)
        {
            _repositorioAluno = repositorioAluno;
            _repositorioProfessor = repositorioProfessor;
            _repositorioDisciplina = repositorioDisciplina;
        }

        /// <summary>
        /// Cadastra um novo aluno com validações de regras de negócio.
        /// </summary>
        public Aluno CadastrarAluno(string nome, string email, DateTime dataNascimento, string cpf, int matricula)
        {
            // Validações de negócio
            ValidadorAcademico.ValidarNome(nome);
            ValidadorAcademico.ValidarEmail(email);
            ValidadorAcademico.ValidarDataNascimento(dataNascimento);
            ValidadorAcademico.ValidarCPF(cpf);
            ValidadorAcademico.ValidarMatricula(matricula);

            // Verificar duplicidades
            if (_repositorioAluno.VerificarMatriculaDuplicada(matricula))
                throw new InvalidOperationException($"Matrícula {matricula} já existe no sistema.");

            if (_repositorioAluno.VerificarCPFDuplicado(cpf))
                throw new InvalidOperationException($"CPF {cpf} já foi cadastrado.");

            var aluno = new Aluno(nome, email, dataNascimento, cpf, matricula);
            _repositorioAluno.Adicionar(aluno);
            return aluno;
        }

        /// <summary>
        /// Cadastra um novo professor com validações de regras de negócio.
        /// </summary>
        public Professor CadastrarProfessor(string nome, string email, DateTime dataNascimento, string cpf, string disciplina, decimal salario)
        {
            // Validações de negócio
            ValidadorAcademico.ValidarNome(nome);
            ValidadorAcademico.ValidarEmail(email);
            ValidadorAcademico.ValidarDataNascimento(dataNascimento);
            ValidadorAcademico.ValidarCPF(cpf);
            ValidadorAcademico.ValidarNomeDisciplina(disciplina);
            ValidadorAcademico.ValidarSalario(salario);

            // Verificar duplicidade de CPF
            if (_repositorioProfessor.VerificarCPFDuplicado(cpf))
                throw new InvalidOperationException($"CPF {cpf} já foi cadastrado.");

            var professor = new Professor(nome, email, dataNascimento, cpf, disciplina, salario);
            _repositorioProfessor.Adicionar(professor);
            return professor;
        }

        /// <summary>
        /// Cadastra uma nova disciplina com validações de regras de negócio.
        /// </summary>
        public Disciplina CadastrarDisciplina(string nome, string codigo, Professor professorResponsavel)
        {
            // Validações de negócio
            ValidadorAcademico.ValidarNomeDisciplina(nome);
            ValidadorAcademico.ValidarCodigoDisciplina(codigo);

            if (professorResponsavel == null)
                throw new ArgumentNullException(nameof(professorResponsavel), "Professor responsável é obrigatório.");

            // Verificar se professor existe no sistema
            var professorExistente = _repositorioProfessor.ObterPorCPF(professorResponsavel.CPF);
            if (professorExistente == null)
                throw new InvalidOperationException($"Professor com CPF {professorResponsavel.CPF} não foi encontrado.");

            // Verificar código duplicado
            if (_repositorioDisciplina.VerificarCodigoDuplicado(codigo))
                throw new InvalidOperationException($"Código de disciplina {codigo} já existe.");

            var disciplina = new Disciplina(nome, codigo, professorExistente);
            _repositorioDisciplina.Adicionar(disciplina);
            return disciplina;
        }

        /// <summary>
        /// Obtém um aluno pela matrícula.
        /// </summary>
        public Aluno ObterAluno(int matricula)
        {
            var aluno = _repositorioAluno.ObterPorMatricula(matricula);
            if (aluno == null)
                throw new InvalidOperationException($"Aluno com matrícula {matricula} não foi encontrado.");
            return aluno;
        }

        /// <summary>
        /// Obtém um professor pelo CPF.
        /// </summary>
        public Professor ObterProfessor(string cpf)
        {
            var professor = _repositorioProfessor.ObterPorCPF(cpf);
            if (professor == null)
                throw new InvalidOperationException($"Professor com CPF {cpf} não foi encontrado.");
            return professor;
        }

        /// <summary>
        /// Obtém uma disciplina pelo código.
        /// </summary>
        public Disciplina ObterDisciplina(string codigo)
        {
            var disciplina = _repositorioDisciplina.ObterPorCodigo(codigo);
            if (disciplina == null)
                throw new InvalidOperationException($"Disciplina com código {codigo} não foi encontrada.");
            return disciplina;
        }

        /// <summary>
        /// Lista todos os alunos cadastrados.
        /// </summary>
        public List<Aluno> ListarAlunos()
        {
            return _repositorioAluno.ListarTodos();
        }

        /// <summary>
        /// Lista todos os professores cadastrados.
        /// </summary>
        public List<Professor> ListarProfessores()
        {
            return _repositorioProfessor.ListarTodos();
        }

        /// <summary>
        /// Lista todas as disciplinas cadastradas.
        /// </summary>
        public List<Disciplina> ListarDisciplinas()
        {
            return _repositorioDisciplina.ListarTodos();
        }
    }
}
