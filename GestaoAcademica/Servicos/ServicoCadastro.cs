using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;

namespace GestaoAcademica.Servicos
{
    
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

        
        public Aluno ObterAluno(int matricula)
        {
            var aluno = _repositorioAluno.ObterPorMatricula(matricula);
            if (aluno == null)
                throw new InvalidOperationException($"Aluno com matrícula {matricula} não foi encontrado.");
            return aluno;
        }

        
        public Professor ObterProfessor(string cpf)
        {
            var professor = _repositorioProfessor.ObterPorCPF(cpf);
            if (professor == null)
                throw new InvalidOperationException($"Professor com CPF {cpf} não foi encontrado.");
            return professor;
        }

        
        public Disciplina ObterDisciplina(string codigo)
        {
            var disciplina = _repositorioDisciplina.ObterPorCodigo(codigo);
            if (disciplina == null)
                throw new InvalidOperationException($"Disciplina com código {codigo} não foi encontrada.");
            return disciplina;
        }

     
        public List<Aluno> ListarAlunos()
        {
            return _repositorioAluno.ListarTodos();
        }

        
        public List<Professor> ListarProfessores()
        {
            return _repositorioProfessor.ListarTodos();
        }

        public List<Disciplina> ListarDisciplinas()
        {
            return _repositorioDisciplina.ListarTodos();
        }
    }
}
