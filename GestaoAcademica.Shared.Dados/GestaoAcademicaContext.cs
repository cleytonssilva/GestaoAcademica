using Microsoft.EntityFrameworkCore;
using GestaoAcademica.Dominio;

namespace GestaoAcademica.Dados
{
    public class GestaoAcademicaContext : DbContext
    {
        public GestaoAcademicaContext(DbContextOptions<GestaoAcademicaContext> options) 
            : base(options)
        {
        }

        // DbSets
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração Pessoa (TPH - Table Per Hierarchy)
            modelBuilder.Entity<Pessoa>()
                .HasDiscriminator<string>("tipo")
                .HasValue<Aluno>("aluno")
                .HasValue<Professor>("professor");

            // Configuração Aluno
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.Matriculas)
                .WithOne(m => m.Aluno)
                .HasForeignKey(m => m.AlunoId);

            // Configuração Professor
            modelBuilder.Entity<Professor>()
                .HasMany(p => p.DisciplinasResponsavel)
                .WithOne(d => d.ProfessorResponsavel)
                .HasForeignKey(d => d.ProfessorResponsavelId);

            modelBuilder.Entity<Professor>()
                .HasMany(p => p.Avaliacoes)
                .WithOne(a => a.Professor)
                .HasForeignKey(a => a.ProfessorId);

            // Configuração Disciplina
            modelBuilder.Entity<Disciplina>()
                .HasMany(d => d.Turmas)
                .WithOne(t => t.Disciplina)
                .HasForeignKey(t => t.DisciplinaId);

            // Configuração Turma
            modelBuilder.Entity<Turma>()
                .HasMany(t => t.Matriculas)
                .WithOne(m => m.Turma)
                .HasForeignKey(m => m.TurmaId);

            modelBuilder.Entity<Turma>()
                .HasMany(t => t.Avaliacoes)
                .WithOne(a => a.Turma)
                .HasForeignKey(a => a.TurmaId);

            // Configuração Matrícula
            modelBuilder.Entity<Matricula>()
                .HasMany(m => m.Avaliacoes)
                .WithOne(a => a.Matricula)
                .HasForeignKey(a => a.MatriculaId);

            // Seed - Dados iniciais para teste
            SeedDados(modelBuilder);
        }

        private static void SeedDados(ModelBuilder modelBuilder)
        {
            var professorId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var alunoId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var disciplinaId = Guid.Parse("00000000-0000-0000-0000-000000000003");
            var turmaId = Guid.Parse("00000000-0000-0000-0000-000000000004");

            // Professor
            modelBuilder.Entity<Professor>().HasData(
                new Professor
                {
                    Id = professorId,
                    Nome = "Prof. João Silva",
                    Email = "joao@email.com",
                    CPF = "12345678901",
                    DataNascimento = new DateTime(1980, 5, 15),
                    DataCadastro = DateTime.UtcNow,
                    DisciplinaPrincipal = "Matemática",
                    Disciplina = "Matemática",
                    Salario = 4500M,
                    DataAdmissao = DateTime.UtcNow,
                    Ativo = true
                }
            );

            // Aluno
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = alunoId,
                    Nome = "Ana Santos",
                    Email = "ana@email.com",
                    CPF = "98765432109",
                    DataNascimento = new DateTime(2005, 3, 20),
                    DataCadastro = DateTime.UtcNow,
                    Matricula = 2024001,
                    DataMatricula = DateTime.UtcNow,
                    Ativo = true
                }
            );

            // Disciplina
            modelBuilder.Entity<Disciplina>().HasData(
                new Disciplina
                {
                    Id = disciplinaId,
                    Nome = "Cálculo I",
                    Codigo = "MAT101",
                    ProfessorResponsavelId = professorId,
                    Descricao = "Fundamentos de Cálculo Diferencial",
                    Ativa = true,
                    DataCriacao = DateTime.UtcNow
                }
            );

            // Turma
            modelBuilder.Entity<Turma>().HasData(
                new Turma
                {
                    Id = turmaId,
                    DisciplinaId = disciplinaId,
                    NumeroTurma = "A",
                    Semestre = 1,
                    Ano = 2024,
                    CapacidadeMaxima = 40,
                    Ativa = true,
                    DataCriacao = DateTime.UtcNow
                }
            );

            // Matrícula
            modelBuilder.Entity<Matricula>().HasData(
                new Matricula
                {
                    Id = Guid.NewGuid(),
                    AlunoId = alunoId,
                    TurmaId = turmaId,
                    DataMatricula = DateTime.UtcNow,
                    Situacao = SituacaoMatricula.Ativa
                }
            );
        }
    }
}
