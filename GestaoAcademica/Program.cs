using System;
using GestaoAcademica.Dados;
using GestaoAcademica.Servicos;
using GestaoAcademica.UI;



public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Inicialização dos repositórios (injeção de dependência)
            IRepositorioAluno repositorioAluno = new RepositorioAlunoEmMemoria();
            IRepositorioProfessor repositorioProfessor = new RepositorioProfessorEmMemoria();
            IRepositorioDisciplina repositorioDisciplina = new RepositorioDisciplinaEmMemoria();
            IRepositorioMatricula repositorioMatricula = new RepositorioMatriculaEmMemoria();

            // Inicialização dos serviços com injeção de repositórios
            ServicoCadastro servicoCadastro = new ServicoCadastro(repositorioAluno, repositorioProfessor, repositorioDisciplina);
            ServicoMatricula servicoMatricula = new ServicoMatricula(repositorioMatricula);
            ServicoAvaliacao servicoAvaliacao = new ServicoAvaliacao();
            ServicoRelatorio servicoRelatorio = new ServicoRelatorio();

            // Inicialização da interface do usuário (terminal)
            TerminalUI terminalUI = new TerminalUI(servicoCadastro, servicoMatricula, servicoAvaliacao, servicoRelatorio);

            // Execução do sistema
            terminalUI.Executar();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro fatal no sistema: {ex.Message}");
            Console.ResetColor();
        }
    }
}
