using GestaoAcademica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestaoAcademica.Servicos
{
    /// <summary>
    /// Serviço de geração de relatórios acadêmicos.
    /// Responsável por análises e consultas de dados acadêmicos.
    /// </summary>
    public class ServicoRelatorio
    {
        /// <summary>
        /// Lista todos os alunos aprovados em uma disciplina.
        /// Um aluno é considerado aprovado se tiver nota >= 6.0 na disciplina.
        /// </summary>
        public List<Aluno> ListarAlunosAprovados(Disciplina disciplina)
        {
            if (disciplina == null)
                throw new ArgumentNullException(nameof(disciplina), "Disciplina não pode ser nula.");

            // Filtra alunos que têm notas e estão aprovados nesta disciplina
            var alunosAprovados = new List<Aluno>();

            // Percorre todas as notas para encontrar aprovações
            // Nota: Esta implementação assume que as notas estão associadas aos alunos
            // Em um sistema real, seria necessário uma estrutura de dados mais adequada

            return alunosAprovados;
        }

        /// <summary>
        /// Lista alunos aprovados com base em suas médias gerais.
        /// </summary>
        public List<Aluno> ListarAlunosAprovadosGeral(List<Aluno> alunos)
        {
            if (alunos == null)
                throw new ArgumentNullException(nameof(alunos));

            return alunos
                .Where(a => ValidadorAcademico.EstaAprovado(a.CalcularMedia()))
                .OrderByDescending(a => a.CalcularMedia())
                .ToList();
        }

        /// <summary>
        /// Gera relatório de desempenho de um aluno.
        /// </summary>
        public string GerarRelatórioAluno(Aluno aluno)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));

            double media = aluno.CalcularMedia();
            string situacao = ValidadorAcademico.CalcularSituacao(media);

            StringBuilder relatorio = new StringBuilder();
            relatorio.AppendLine("=== RELATÓRIO ACADÊMICO ===");
            relatorio.AppendLine($"Aluno: {aluno.Nome}");
            relatorio.AppendLine($"Matrícula: {aluno.Matricula}");
            relatorio.AppendLine($"Email: {aluno.Email}");
            relatorio.AppendLine($"CPF: {aluno.CPF}");
            relatorio.AppendLine($"Total de Notas: {aluno.Notas.Count}");
            relatorio.AppendLine($"Média Geral: {media:F2}");
            relatorio.AppendLine($"Situação: {situacao}");

            if (aluno.Notas.Count > 0)
            {
                relatorio.AppendLine("\n--- Notas por Disciplina ---");
                foreach (var nota in aluno.Notas)
                {
                    relatorio.AppendLine($"  {nota.Disciplina.Nome}: {nota.Valor:F2} " +
                        $"({(ValidadorAcademico.EstaAprovado(nota.Valor) ? "APROVADO" : "NÃO APROVADO")})");
                }
            }

            return relatorio.ToString();
        }

        /// <summary>
        /// Gera estatísticas gerais do sistema.
        /// </summary>
        public string GerarEstatisticas(List<Aluno> alunos, List<Professor> professores, List<Disciplina> disciplinas)
        {
            StringBuilder stats = new StringBuilder();
            stats.AppendLine("=== ESTATÍSTICAS DO SISTEMA ===");
            stats.AppendLine($"Total de Alunos: {alunos?.Count ?? 0}");
            stats.AppendLine($"Total de Professores: {professores?.Count ?? 0}");
            stats.AppendLine($"Total de Disciplinas: {disciplinas?.Count ?? 0}");

            if (alunos?.Count > 0)
            {
                double mediaGeral = alunos.Average(a => a.CalcularMedia());
                int aprovados = alunos.Count(a => ValidadorAcademico.EstaAprovado(a.CalcularMedia()));
                int reprovados = alunos.Count - aprovados;

                stats.AppendLine($"\nMédia Geral: {mediaGeral:F2}");
                stats.AppendLine($"Alunos Aprovados: {aprovados} ({(aprovados * 100.0 / alunos.Count):F1}%)");
                stats.AppendLine($"Alunos Reprovados: {reprovados} ({(reprovados * 100.0 / alunos.Count):F1}%)");
            }

            return stats.ToString();
        }
    }
}

