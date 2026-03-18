-- =====================================================
-- SCRIPT DE CRIAÇÃO - BANCO DE DADOS GESTÃO ACADÊMICA
-- Banco de Dados: gestao_academica
-- SGBD: MySQL 5.7+
-- Encoding: UTF-8
-- =====================================================

-- Criar banco de dados
CREATE DATABASE IF NOT EXISTS gestao_academica
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE gestao_academica;

-- =====================================================
-- TABELA: pessoas (base para aluno e professor)
-- =====================================================
CREATE TABLE IF NOT EXISTS pessoas (
    id CHAR(36) NOT NULL PRIMARY KEY COMMENT 'UUID',
    tipo ENUM('aluno', 'professor') NOT NULL COMMENT 'Tipo de pessoa',
    nome VARCHAR(100) NOT NULL COMMENT 'Nome completo',
    email VARCHAR(100) NOT NULL UNIQUE COMMENT 'Email único',
    cpf VARCHAR(11) NOT NULL UNIQUE COMMENT 'CPF sem formatação',
    data_nascimento DATE NOT NULL COMMENT 'Data de nascimento',
    data_cadastro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT TRUE,
    INDEX idx_email (email),
    INDEX idx_cpf (cpf),
    INDEX idx_tipo (tipo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Tabela base para Alunos e Professores';

-- =====================================================
-- TABELA: alunos
-- =====================================================
CREATE TABLE IF NOT EXISTS alunos (
    id CHAR(36) NOT NULL PRIMARY KEY COMMENT 'UUID - FK de pessoas',
    matricula INT NOT NULL UNIQUE COMMENT 'Matrícula única do aluno',
    data_matricula TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT TRUE,
    
    FOREIGN KEY (id) REFERENCES pessoas(id) ON DELETE CASCADE,
    INDEX idx_matricula (matricula)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Dados específicos de Alunos';

-- =====================================================
-- TABELA: professores
-- =====================================================
CREATE TABLE IF NOT EXISTS professores (
    id CHAR(36) NOT NULL PRIMARY KEY COMMENT 'UUID - FK de pessoas',
    disciplina_principal VARCHAR(100) NOT NULL COMMENT 'Disciplina principal',
    salario DECIMAL(10, 2) NOT NULL COMMENT 'Salário em reais',
    data_admissao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT TRUE,
    
    FOREIGN KEY (id) REFERENCES pessoas(id) ON DELETE CASCADE,
    INDEX idx_disciplina (disciplina_principal)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Dados específicos de Professores';

-- =====================================================
-- TABELA: disciplinas
-- =====================================================
CREATE TABLE IF NOT EXISTS disciplinas (
    id CHAR(36) NOT NULL PRIMARY KEY COMMENT 'UUID',
    nome VARCHAR(100) NOT NULL COMMENT 'Nome da disciplina',
    codigo VARCHAR(10) NOT NULL UNIQUE COMMENT 'Código único (ex: MAT001)',
    professor_responsavel_id CHAR(36) NOT NULL COMMENT 'FK para Professor',
    descricao TEXT COMMENT 'Descrição da disciplina',
    ativa BOOLEAN DEFAULT TRUE,
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (professor_responsavel_id) REFERENCES professores(id),
    INDEX idx_codigo (codigo),
    INDEX idx_professor (professor_responsavel_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Disciplinas oferecidas';

-- =====================================================
-- TABELA: turmas
-- =====================================================
CREATE TABLE IF NOT EXISTS turmas (
    id CHAR(36) NOT NULL PRIMARY KEY COMMENT 'UUID',
    disciplina_id CHAR(36) NOT NULL COMMENT 'FK para Disciplina',
    numero_turma VARCHAR(10) NOT NULL COMMENT 'Número/Identificação da turma',
    semestre INT NOT NULL COMMENT 'Semestre (ex: 20251)',
    ano INT NOT NULL COMMENT 'Ano',
    capacidade_maxima INT DEFAULT 40 COMMENT 'Número máximo de alunos',
    ativa BOOLEAN DEFAULT TRUE,
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (disciplina_id) REFERENCES disciplinas(id) ON DELETE CASCADE,
    UNIQUE KEY unique_turma (disciplina_id, numero_turma, semestre),
    INDEX idx_semestre (semestre, ano)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Turmas de disciplinas';

-- =====================================================
-- TABELA: matriculas
-- =====================================================
CREATE TABLE IF NOT EXISTS matriculas (
    id CHAR(36) NOT NULL PRIMARY KEY COMMENT 'UUID',
    aluno_id CHAR(36) NOT NULL COMMENT 'FK para Aluno',
    turma_id CHAR(36) NOT NULL COMMENT 'FK para Turma',
    data_matricula TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Data da matrícula',
    data_conclusao DATE NULL COMMENT 'Data de conclusão (se concluída)',
    situacao ENUM('ativa', 'concluida', 'cancelada') DEFAULT 'ativa',
    
    FOREIGN KEY (aluno_id) REFERENCES alunos(id) ON DELETE CASCADE,
    FOREIGN KEY (turma_id) REFERENCES turmas(id) ON DELETE CASCADE,
    UNIQUE KEY unique_matricula (aluno_id, turma_id),
    INDEX idx_aluno (aluno_id),
    INDEX idx_turma (turma_id),
    INDEX idx_situacao (situacao)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Matrícula de alunos em turmas';

-- =====================================================
-- TABELA: avaliacoes (Notas)
-- =====================================================
CREATE TABLE IF NOT EXISTS avaliacoes (
    id CHAR(36) NOT NULL PRIMARY KEY COMMENT 'UUID',
    matricula_id CHAR(36) NOT NULL COMMENT 'FK para Matrícula',
    professor_id CHAR(36) NOT NULL COMMENT 'FK para Professor que atribuiu',
    turma_id CHAR(36) NOT NULL COMMENT 'FK para Turma',
    valor DECIMAL(4, 2) NOT NULL COMMENT 'Nota (0.0 a 10.0)',
    tipo_avaliacao VARCHAR(50) COMMENT 'Tipo (prova, trabalho, participação, etc)',
    peso DECIMAL(4, 2) DEFAULT 1.0 COMMENT 'Peso da avaliação',
    data_avaliacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    observacoes TEXT,
    
    FOREIGN KEY (matricula_id) REFERENCES matriculas(id) ON DELETE CASCADE,
    FOREIGN KEY (professor_id) REFERENCES professores(id),
    FOREIGN KEY (turma_id) REFERENCES turmas(id),
    INDEX idx_matricula (matricula_id),
    INDEX idx_professor (professor_id),
    INDEX idx_turma (turma_id),
    INDEX idx_data (data_avaliacao)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Avaliações/Notas dos alunos';

-- =====================================================
-- ÍNDICES ADICIONAIS PARA PERFORMANCE
-- =====================================================

-- Índice para buscar aluno por matrícula
ALTER TABLE alunos ADD INDEX idx_aluno_ativo (ativo);

-- Índice para buscar professores ativos
ALTER TABLE professores ADD INDEX idx_professor_ativo (ativo);

-- Índice para histórico de matrículas
ALTER TABLE matriculas ADD INDEX idx_data_matricula (data_matricula);

-- Índice para relatórios de notas
ALTER TABLE avaliacoes ADD INDEX idx_valor (valor);

-- =====================================================
-- VIEWS ÚTEIS PARA CONSULTAS COMUNS
-- =====================================================

-- View: Alunos com seus dados pessoais
CREATE OR REPLACE VIEW v_alunos AS
SELECT 
    a.id,
    a.matricula,
    p.nome,
    p.email,
    p.cpf,
    p.data_nascimento,
    a.data_matricula,
    a.ativo
FROM alunos a
INNER JOIN pessoas p ON a.id = p.id;

-- View: Professores com seus dados
CREATE OR REPLACE VIEW v_professores AS
SELECT 
    pr.id,
    p.nome,
    p.email,
    p.cpf,
    pr.disciplina_principal,
    pr.salario,
    pr.data_admissao,
    pr.ativo
FROM professores pr
INNER JOIN pessoas p ON pr.id = p.id;

-- View: Disciplinas com professor responsável
CREATE OR REPLACE VIEW v_disciplinas_professor AS
SELECT 
    d.id,
    d.nome,
    d.codigo,
    pr.nome as professor_nome,
    d.ativa
FROM disciplinas d
INNER JOIN professores prof ON d.professor_responsavel_id = prof.id
INNER JOIN pessoas pr ON prof.id = pr.id;

-- View: Notas dos alunos com média por disciplina
CREATE OR REPLACE VIEW v_media_alunos AS
SELECT 
    a.matricula,
    p.nome as aluno_nome,
    d.nome as disciplina_nome,
    COUNT(av.id) as total_avaliacoes,
    AVG(av.valor) as media,
    CASE 
        WHEN AVG(av.valor) >= 6.0 THEN 'APROVADO'
        WHEN AVG(av.valor) >= 4.0 THEN 'RECUPERACAO'
        ELSE 'REPROVADO'
    END as situacao
FROM matriculas m
INNER JOIN alunos a ON m.aluno_id = a.id
INNER JOIN pessoas p ON a.id = p.id
INNER JOIN turmas t ON m.turma_id = t.id
INNER JOIN disciplinas d ON t.disciplina_id = d.id
LEFT JOIN avaliacoes av ON m.id = av.matricula_id
GROUP BY a.id, d.id;

-- =====================================================
-- DADOS DE EXEMPLO (OPCIONAL)
-- =====================================================

-- Inserir Professor exemplo
INSERT INTO pessoas (id, tipo, nome, email, cpf, data_nascimento) 
VALUES (UUID(), 'professor', 'João Silva', 'joao.silva@escola.com', '12345678901', '1980-05-15');

-- Inserir Aluno exemplo
INSERT INTO pessoas (id, tipo, nome, email, cpf, data_nascimento) 
VALUES (UUID(), 'aluno', 'Maria Santos', 'maria.santos@aluno.com', '98765432100', '2005-03-10');

-- =====================================================
-- STORED PROCEDURES ÚTEIS
-- =====================================================

DELIMITER $$

-- Procedure: Calcular média do aluno
CREATE PROCEDURE sp_calcular_media_aluno(
    IN p_aluno_id CHAR(36),
    IN p_turma_id CHAR(36),
    OUT p_media DECIMAL(4, 2),
    OUT p_situacao VARCHAR(50)
)
BEGIN
    SELECT AVG(valor) INTO p_media
    FROM avaliacoes
    WHERE matricula_id IN (
        SELECT id FROM matriculas 
        WHERE aluno_id = p_aluno_id AND turma_id = p_turma_id
    );
    
    IF p_media IS NULL THEN
        SET p_media = 0;
    END IF;
    
    SET p_situacao = CASE
        WHEN p_media >= 6.0 THEN 'APROVADO'
        WHEN p_media >= 4.0 THEN 'RECUPERACAO'
        ELSE 'REPROVADO'
    END;
END$$

-- Procedure: Listar alunos aprovados por disciplina
CREATE PROCEDURE sp_alunos_aprovados_disciplina(
    IN p_disciplina_id CHAR(36)
)
BEGIN
    SELECT DISTINCT
        a.matricula,
        p.nome,
        AVG(av.valor) as media
    FROM matriculas m
    INNER JOIN alunos a ON m.aluno_id = a.id
    INNER JOIN pessoas p ON a.id = p.id
    INNER JOIN turmas t ON m.turma_id = t.id
    INNER JOIN avaliacoes av ON m.id = av.matricula_id
    WHERE t.disciplina_id = p_disciplina_id
    GROUP BY a.id
    HAVING AVG(av.valor) >= 6.0
    ORDER BY media DESC;
END$$

DELIMITER ;

-- =====================================================
-- FIM DO SCRIPT
-- =====================================================
