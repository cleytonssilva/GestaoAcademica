using System;
using System.Configuration;

namespace GestaoAcademica.Dados
{
    /// <summary>
    /// Fornecedor de conexão com banco de dados MySQL
    /// Nota: Requer instalação do pacote NuGet MySql.Data para funcionamento completo
    /// </summary>
    public class ConexaoBancoDados
    {
        private static readonly string _connectionString;
        private static readonly bool _conexaoDisponivel;

        static ConexaoBancoDados()
        {
            _conexaoDisponivel = false;

            try
            {
                // Tenta obter a connection string da configuração
                var configConnection = ConfigurationManager.ConnectionStrings["GestaoAcademica"]?.ConnectionString;

                if (!string.IsNullOrWhiteSpace(configConnection))
                {
                    _connectionString = configConnection;
                    _conexaoDisponivel = true;
                }
                else
                {
                    // Usar padrão se não encontrar na config
                    _connectionString = "Server=localhost;Database=gestao_academica;User=root;Password=;";
                }
            }
            catch (Exception ex)
            {
                // Se houver erro ao ler App.config, usar padrão
                System.Diagnostics.Debug.WriteLine($"Erro ao ler App.config: {ex.Message}");
                _connectionString = "Server=localhost;Database=gestao_academica;User=root;Password=;";
            }
        }

        /// <summary>
        /// Obtém a string de conexão configurada
        /// </summary>
        public static string ObterConnectionString()
        {
            return _connectionString;
        }

        /// <summary>
        /// Verifica se a conexão com banco de dados está disponível
        /// </summary>
        public static bool EstaDisponivel()
        {
            return _conexaoDisponivel;
        }

        /// <summary>
        /// Testa a conexão com o banco de dados
        /// Nota: Requer MySql.Data instalado para teste real
        /// </summary>
        public static bool TestarConexao()
        {
            try
            {
                // Verificação básica - implementação completa requer MySql.Data
                // Por enquanto, retorna false para indicar que a conexão não foi testada
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
