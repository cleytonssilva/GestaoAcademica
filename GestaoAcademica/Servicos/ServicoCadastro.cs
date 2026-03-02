using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            var aluno = new Aluno(nome, email, dataNascimento, cpf, matricula);
            _repositorioAluno.Adicionar(aluno);
            return aluno;
        }

        public Professor CadastrarProfessor(string nome, string email, DateTime dataNascimento, string cpf, string disciplina, decimal salario)
        {
            var professor = new Professor(nome, email, dataNascimento, cpf, disciplina, salario);
            _repositorioProfessor.Adicionar(professor);
            return professor;
        }

        public Disciplina CadastrarDisciplina(string nome, string codigo, Professor professorResponsavel)
        {
            var disciplina = new Disciplina(nome, codigo, professorResponsavel);
            _repositorioDisciplina.Adicionar(disciplina);
            return disciplina;
        }
    }
}
    


