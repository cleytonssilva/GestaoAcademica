-- Script de criação do banco de dados SistemaBDGestaoAcademica
-- Compatível com MySQL 5.7+

CREATE DATABASE IF NOT EXISTS SistemaBDGestaoAcademica
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE SistemaBDGestaoAcademica;

-- Tabela base: Pessoas (Alunos e Professores)
CREATE TABLE IF NOT EXISTS pessoas (
    id_pessoa INT AUTO_INCREMENT PRIMARY KEY,
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

-- Tabela: Alunos
CREATE TABLE IF NOT EXISTS alunos (
    id_aluno INT AUTO_INCREMENT PRIMARY KEY,
    matricula INT NOT NULL UNIQUE COMMENT 'Matrícula única do aluno',
    data_matricula TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT TRUE,
    
    FOREIGN KEY (id_aluno) REFERENCES pessoas(id_pessoa) ON DELETE CASCADE,
    INDEX idx_matricula (matricula),
    INDEX idx_aluno_ativo (ativo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Dados específicos de Alunos';

-- Tabela: Professores
CREATE TABLE IF NOT EXISTS professores (
    id_professor INT AUTO_INCREMENT PRIMARY KEY,
    disciplina_principal VARCHAR(100) NOT NULL COMMENT 'Disciplina principal',
    salario DECIMAL(10, 2) NOT NULL COMMENT 'Salário em reais',
    data_admissao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT TRUE,
    
    FOREIGN KEY (id_professor) REFERENCES pessoas(id_pessoa) ON DELETE CASCADE,
    INDEX idx_disciplina (disciplina_principal),
    INDEX idx_professor_ativo (ativo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Dados específicos de Professores';

-- Tabela: Disciplinas
CREATE TABLE IF NOT EXISTS disciplinas (
    id_disciplina INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL COMMENT 'Nome da disciplina',
    codigo VARCHAR(10) NOT NULL UNIQUE COMMENT 'Código único (ex: MAT001)',
    professor_responsavel_id INT NOT NULL COMMENT 'FK para Professor',
    descricao TEXT COMMENT 'Descrição da disciplina',
    ativa BOOLEAN DEFAULT TRUE,
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (professor_responsavel_id) REFERENCES professores(id_professor),
    INDEX idx_codigo (codigo),
    INDEX idx_professor (professor_responsavel_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Disciplinas oferecidas';

-- Tabela: Turmas
CREATE TABLE IF NOT EXISTS turmas (
    id_turma INT AUTO_INCREMENT PRIMARY KEY,
    id_disciplina INT NOT NULL COMMENT 'FK para Disciplina',
    numero_turma VARCHAR(10) NOT NULL COMMENT 'Número/Identificação da turma',
    semestre INT NOT NULL COMMENT 'Semestre (ex: 20251)',
    ano INT NOT NULL COMMENT 'Ano',
    capacidade_maxima INT DEFAULT 40 COMMENT 'Número máximo de alunos',
    ativa BOOLEAN DEFAULT TRUE,
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (id_disciplina) REFERENCES disciplinas(id_disciplina) ON DELETE CASCADE,
    UNIQUE KEY unique_turma (id_disciplina, numero_turma, semestre),
    INDEX idx_semestre (semestre, ano)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Turmas de disciplinas';

-- Tabela: Matrículas
CREATE TABLE IF NOT EXISTS matriculas (
    id_matricula INT AUTO_INCREMENT PRIMARY KEY,
    id_aluno INT NOT NULL COMMENT 'FK para Aluno',
    id_turma INT NOT NULL COMMENT 'FK para Turma',
    data_matricula TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Data da matrícula',
    data_conclusao DATE NULL COMMENT 'Data de conclusão (se concluída)',
    situacao ENUM('ativa', 'concluida', 'cancelada') DEFAULT 'ativa',
    
    FOREIGN KEY (id_aluno) REFERENCES alunos(id_aluno) ON DELETE CASCADE,
    FOREIGN KEY (id_turma) REFERENCES turmas(id_turma) ON DELETE CASCADE,
    UNIQUE KEY unique_matricula (id_aluno, id_turma),
    INDEX idx_aluno (id_aluno),
    INDEX idx_turma (id_turma),
    INDEX idx_situacao (situacao),
    INDEX idx_data_matricula (data_matricula)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Matrícula de alunos em turmas';

-- Tabela: Avaliações
CREATE TABLE IF NOT EXISTS avaliacoes (
    id_avaliacao INT AUTO_INCREMENT PRIMARY KEY,
    id_matricula INT NOT NULL COMMENT 'FK para Matrícula',
    id_professor INT NOT NULL COMMENT 'FK para Professor que atribuiu',
    id_turma INT NOT NULL COMMENT 'FK para Turma',
    valor DECIMAL(4, 2) NOT NULL COMMENT 'Nota (0.0 a 10.0)',
    tipo_avaliacao VARCHAR(50) COMMENT 'Tipo (prova, trabalho, participação, etc)',
    peso DECIMAL(4, 2) DEFAULT 1.0 COMMENT 'Peso da avaliação',
    data_avaliacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    observacoes TEXT,
    
    FOREIGN KEY (id_matricula) REFERENCES matriculas(id_matricula) ON DELETE CASCADE,
    FOREIGN KEY (id_professor) REFERENCES professores(id_professor),
    FOREIGN KEY (id_turma) REFERENCES turmas(id_turma),
    INDEX idx_matricula (id_matricula),
    INDEX idx_professor (id_professor),
    INDEX idx_turma (id_turma),
    INDEX idx_data (data_avaliacao),
    INDEX idx_valor (valor)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
COMMENT='Avaliações/Notas dos alunos';
