using System;
using GestaoAcademica.Dados;
using GestaoAcademica.Servicos;
using GestaoAcademica.UI;

/// <summary>
/// Ponto de entrada da aplicação Gestão Acadêmica
/// Configura injeção de dependência e inicializa o sistema
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Iniciando sistema Gestão Acadêmica...");
            Console.WriteLine();

            // ========== VERIFICAÇÃO DE CONEXÃO COM BANCO DE DADOS ==========
            Console.WriteLine("Verificando conexão com banco de dados MySQL...");

            string connectionString = ConexaoBancoDados.ObterConnectionString();
            Console.WriteLine($"String de Conexão: {connectionString}");

            if (!ConexaoBancoDados.TestarConexao())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠ AVISO: Não foi possível conectar ao banco de dados MySQL");
                Console.WriteLine("Certifique-se de que:");
                Console.WriteLine("  1. MySQL Server está em execução na máquina local");
                Console.WriteLine("  2. O banco de dados 'gestao_academica' foi criado");
                Console.WriteLine("  3. Execute o script SQL: BD_Scripts/01_create_database.sql");
                Console.WriteLine();
                Console.WriteLine("O sistema continuará em modo demonstração com dados em memória.");
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Conexão com banco de dados MySQL estabelecida com sucesso");
                Console.ResetColor();
                Console.WriteLine();
            }

            // ========== INICIALIZAÇÃO DOS REPOSITÓRIOS (INJEÇÃO DE DEPENDÊNCIA) ==========
            // Tenta usar repositórios MySQL, se falhar usa in-memory
            IRepositorioAluno repositorioAluno;
            IRepositorioProfessor repositorioProfessor;
            IRepositorioDisciplina repositorioDisciplina;
            IRepositorioMatricula repositorioMatricula;

            try
            {
                // Criar DbContext
                var context = new GestaoAcademicaContext();

                // Usar repositórios MySQL
                repositorioAluno = new RepositorioAlunoMySQL(context);
                repositorioProfessor = new RepositorioProfessorMySQL(context);
                repositorioDisciplina = new RepositorioDisciplinaMySQL(context);
                repositorioMatricula = new RepositorioMatriculaMySQL(context);

                Console.WriteLine("Repositórios MySQL inicializados");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠ Usando modo em memória (erro ao inicializar MySQL): {ex.Message}");
                Console.ResetColor();

                // Fallback para in-memory
                repositorioAluno = new RepositorioAlunoEmMemoria();
                repositorioProfessor = new RepositorioProfessorEmMemoria();
                repositorioDisciplina = new RepositorioDisciplinaEmMemoria();
                repositorioMatricula = new RepositorioMatriculaEmMemoria();
            }

            Console.WriteLine();

            // ========== INICIALIZAÇÃO DOS SERVIÇOS COM INJEÇÃO DE REPOSITÓRIOS ==========
            ServicoCadastro servicoCadastro = new ServicoCadastro(repositorioAluno, repositorioProfessor, repositorioDisciplina);
            ServicoMatricula servicoMatricula = new ServicoMatricula(repositorioMatricula);
            ServicoAvaliacao servicoAvaliacao = new ServicoAvaliacao();
            ServicoRelatorio servicoRelatorio = new ServicoRelatorio();

            // ========== INICIALIZAÇÃO DA INTERFACE DO USUÁRIO (TERMINAL) ==========
            TerminalUI terminalUI = new TerminalUI(servicoCadastro, servicoMatricula, servicoAvaliacao, servicoRelatorio);

            // ========== EXECUÇÃO DO SISTEMA ==========
            terminalUI.Executar();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro fatal no sistema: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.ResetColor();
        }
    }
}
