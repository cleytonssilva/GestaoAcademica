using System;
using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;
using GestaoAcademica.Servicos;
using GestaoAcademica.UI;
using System.Collections.Generic;
using System.Linq;


public class Program
{
    public static void Main(string[] args)
    {
        // Inicialização dos repositórios
        IRepositorioAluno repositorioAluno = new RepositorioAlunoEmMemoria();
        IRepositorioProfessor repositorioProfessor = new RepositorioProfessorEmMemoria();
        IRepositorioDisciplina repositorioDisciplina = new RepositorioDisciplinaEmMemoria();
        IRepositorioMatricula repositorioMatricula = new RepositorioMatriculaEmMemoria();

        // Inicialização dos serviços
        ServicoCadastro servicoCadastro = new ServicoCadastro(repositorioAluno, repositorioProfessor, repositorioDisciplina);
        ServicoMatricula servicoMatricula = new ServicoMatricula(repositorioMatricula);
        ServicoAvaliacao servicoAvaliacao = new ServicoAvaliacao();
        ServicoRelatorio servicoRelatorio = new ServicoRelatorio();

        // Inicialização da interface do usuário
        TerminalUI terminalUI = new TerminalUI(servicoCadastro, servicoMatricula, servicoAvaliacao, servicoRelatorio);

        // Execução do sistema
        terminalUI.Executar();
    }
}