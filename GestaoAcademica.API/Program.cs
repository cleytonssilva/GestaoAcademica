using Microsoft.EntityFrameworkCore;
using GestaoAcademica.Dados;
using GestaoAcademica.Dominio;

var builder = WebApplication.CreateBuilder(args);

// Adicionar DbContext com InMemory Database
builder.Services.AddDbContext<GestaoAcademicaContext>(options =>
    options.UseInMemoryDatabase("GestaoAcademicaDb"));

// Adicionar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// ==================== ENDPOINTS ====================

// Health Check
app.MapGet("/health", () => 
    new { status = "OK", timestamp = DateTime.UtcNow })
    .WithName("Health Check");

// ==================== ALUNOS ====================

// GET - Listar todos os alunos
app.MapGet("/api/alunos", async (GestaoAcademicaContext db) =>
{
    var alunos = await db.Alunos.ToListAsync();
    return Results.Ok(alunos);
})
.WithName("ListarAlunos");

// GET - Buscar aluno por ID
app.MapGet("/api/alunos/{id:guid}", async (Guid id, GestaoAcademicaContext db) =>
{
    var aluno = await db.Alunos
        .Include(a => a.Matriculas)
        .ThenInclude(m => m.Turma)
        .FirstOrDefaultAsync(a => a.Id == id);
    
    return aluno is not null ? Results.Ok(aluno) : Results.NotFound();
})
.WithName("BuscarAlunoPorId");

// POST - Criar novo aluno
app.MapPost("/api/alunos", async (CreateAlunoRequest request, GestaoAcademicaContext db) =>
{
    var aluno = new Aluno
    {
        Nome = request.Nome,
        Email = request.Email,
        CPF = request.CPF,
        DataNascimento = request.DataNascimento,
        Matricula = new Random().Next(100000, 999999)
    };
    
    db.Alunos.Add(aluno);
    await db.SaveChangesAsync();
    
    return Results.Created($"/api/alunos/{aluno.Id}", aluno);
})
.WithName("CriarAluno");

// ==================== PROFESSORES ====================

// GET - Listar todos os professores
app.MapGet("/api/professores", async (GestaoAcademicaContext db) =>
{
    var professores = await db.Professores.ToListAsync();
    return Results.Ok(professores);
})
.WithName("ListarProfessores");

// ==================== DISCIPLINAS ====================

// GET - Listar todas as disciplinas
app.MapGet("/api/disciplinas", async (GestaoAcademicaContext db) =>
{
    var disciplinas = await db.Disciplinas
        .Include(d => d.ProfessorResponsavel)
        .ToListAsync();
    return Results.Ok(disciplinas);
})
.WithName("ListarDisciplinas");

// ==================== TURMAS ====================

// GET - Listar todas as turmas
app.MapGet("/api/turmas", async (GestaoAcademicaContext db) =>
{
    var turmas = await db.Turmas
        .Include(t => t.Disciplina)
        .ToListAsync();
    return Results.Ok(turmas);
})
.WithName("ListarTurmas");

// ==================== ESTATÍSTICAS ====================

// GET - Estatísticas gerais
app.MapGet("/api/estatisticas", async (GestaoAcademicaContext db) =>
{
    var stats = new
    {
        total_alunos = await db.Alunos.CountAsync(),
        total_professores = await db.Professores.CountAsync(),
        total_disciplinas = await db.Disciplinas.CountAsync(),
        total_turmas = await db.Turmas.CountAsync(),
        total_matriculas = await db.Matriculas.CountAsync(),
        timestamp = DateTime.UtcNow
    };
    return Results.Ok(stats);
})
.WithName("Estatísticas");

app.Run();

// ==================== DTOs ====================

public record CreateAlunoRequest(
    string Nome,
    string Email,
    string CPF,
    DateTime DataNascimento
);
