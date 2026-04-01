-- Views para consultas facilitadas
USE SistemaBDGestaoAcademica;

-- View: Alunos com seus dados pessoais
CREATE OR REPLACE VIEW v_alunos AS
SELECT 
    a.id_aluno,
    a.matricula,
    p.nome,
    p.email,
    p.cpf,
    p.data_nascimento,
    a.data_matricula,
    a.ativo
FROM alunos a
INNER JOIN pessoas p ON a.id_aluno = p.id_pessoa;

-- View: Professores com seus dados
CREATE OR REPLACE VIEW v_professores AS
SELECT 
    pr.id_professor,
    p.nome,
    p.email,
    p.cpf,
    pr.disciplina_principal,
    pr.salario,
    pr.data_admissao,
    pr.ativo
FROM professores pr
INNER JOIN pessoas p ON pr.id_professor = p.id_pessoa;

-- View: Disciplinas com professor responsável
CREATE OR REPLACE VIEW v_disciplinas_professor AS
SELECT 
    d.id_disciplina,
    d.nome,
    d.codigo,
    p.nome as professor_nome,
    d.ativa
FROM disciplinas d
INNER JOIN professores prof ON d.professor_responsavel_id = prof.id_professor
INNER JOIN pessoas p ON prof.id_professor = p.id_pessoa;

-- View: Notas dos alunos com média por disciplina
CREATE OR REPLACE VIEW v_media_alunos AS
SELECT 
    a.id_aluno,
    p.nome as aluno_nome,
    d.nome as disciplina_nome,
    COUNT(av.id_avaliacao) as total_avaliacoes,
    AVG(av.valor) as media,
    CASE 
        WHEN AVG(av.valor) >= 7.0 THEN 'APROVADO'
        WHEN AVG(av.valor) >= 4.0 THEN 'RECUPERACAO'
        ELSE 'REPROVADO'
    END as situacao
FROM matriculas m
INNER JOIN alunos a ON m.id_aluno = a.id_aluno
INNER JOIN pessoas p ON a.id_aluno = p.id_pessoa
INNER JOIN turmas t ON m.id_turma = t.id_turma
INNER JOIN disciplinas d ON t.id_disciplina = d.id_disciplina
LEFT JOIN avaliacoes av ON m.id_matricula = av.id_matricula
GROUP BY a.id_aluno, d.id_disciplina;

-- STORED PROCEDURES

DELIMITER $$

-- Procedure: Calcular média do aluno
CREATE PROCEDURE sp_calcular_media_aluno(
    IN p_id_aluno INT,
    IN p_id_turma INT,
    OUT p_media DECIMAL(4, 2),
    OUT p_situacao VARCHAR(50)
)
BEGIN
    SELECT AVG(valor) INTO p_media
    FROM avaliacoes
    WHERE id_matricula IN (
        SELECT id_matricula FROM matriculas 
        WHERE id_aluno = p_id_aluno AND id_turma = p_id_turma
    );
    
    IF p_media IS NULL THEN
        SET p_media = 0;
    END IF;
    
    SET p_situacao = CASE
        WHEN p_media >= 7.0 THEN 'APROVADO'
        WHEN p_media >= 4.0 THEN 'RECUPERACAO'
        ELSE 'REPROVADO'
    END;
END$$

-- Procedure: Listar alunos aprovados por disciplina
CREATE PROCEDURE sp_alunos_aprovados_disciplina(
    IN p_id_disciplina INT
)
BEGIN
    SELECT DISTINCT
        a.matricula,
        p.nome,
        AVG(av.valor) as media
    FROM matriculas m
    INNER JOIN alunos a ON m.id_aluno = a.id_aluno
    INNER JOIN pessoas p ON a.id_aluno = p.id_pessoa
    INNER JOIN turmas t ON m.id_turma = t.id_turma
    INNER JOIN avaliacoes av ON m.id_matricula = av.id_matricula
    WHERE t.id_disciplina = p_id_disciplina
    GROUP BY a.id_aluno
    HAVING AVG(av.valor) >= 7.0
    ORDER BY media DESC;
END$$

DELIMITER ;
