-- Script de população de dados de exemplo
-- Use SistemaBDGestaoAcademica;

-- INSERIR PROFESSORES
-- Professor 1: João Silva - Física
INSERT INTO pessoas (tipo, nome, email, cpf, data_nascimento) 
VALUES ('professor', 'João Silva', 'joao.silva@escola.com', '12345678901', '1980-05-15');

INSERT INTO professores (id_professor, disciplina_principal, salario) 
VALUES (1, 'Física', 5200.00);

-- Professor 2: Ana Oliveira - Matemática
INSERT INTO pessoas (tipo, nome, email, cpf, data_nascimento) 
VALUES ('professor', 'Ana Oliveira', 'ana.oliveira@escola.com', '10293847566', '1988-10-12');

INSERT INTO professores (id_professor, disciplina_principal, salario) 
VALUES (2, 'Matemática', 5500.00);

-- Professor 3: Carlos Souza - Química
INSERT INTO pessoas (tipo, nome, email, cpf, data_nascimento) 
VALUES ('professor', 'Carlos Souza', 'carlos.souza@escola.com', '11122233344', '1985-07-20');

INSERT INTO professores (id_professor, disciplina_principal, salario) 
VALUES (3, 'Química', 5300.00);

-- INSERIR ALUNOS
-- Aluno 1: Maria Santos
INSERT INTO pessoas (tipo, nome, email, cpf, data_nascimento) 
VALUES ('aluno', 'Maria Santos', 'maria.santos@aluno.com', '98765432100', '2005-03-10');

INSERT INTO alunos (id_aluno, matricula) 
VALUES (4, 2024001);

-- Aluno 2: Pedro Silva
INSERT INTO pessoas (tipo, nome, email, cpf, data_nascimento) 
VALUES ('aluno', 'Pedro Silva', 'pedro.silva@aluno.com', '88776655544', '2004-11-22');

INSERT INTO alunos (id_aluno, matricula) 
VALUES (5, 2024002);

-- Aluno 3: Juliana Costa
INSERT INTO pessoas (tipo, nome, email, cpf, data_nascimento) 
VALUES ('aluno', 'Juliana Costa', 'juliana.costa@aluno.com', '77665544332', '2005-06-15');

INSERT INTO alunos (id_aluno, matricula) 
VALUES (6, 2024003);

-- Aluno 4: Ricardo Oliveira
INSERT INTO pessoas (tipo, nome, email, cpf, data_nascimento) 
VALUES ('aluno', 'Ricardo Oliveira', 'ricardo.oliveira@aluno.com', '66554433221', '2004-09-08');

INSERT INTO alunos (id_aluno, matricula) 
VALUES (7, 2024004);

-- INSERIR DISCIPLINAS
-- Disciplina 1: Física Geral (Prof. João Silva)
INSERT INTO disciplinas (nome, codigo, professor_responsavel_id, descricao, ativa) 
VALUES ('Física Geral', 'FIS001', 1, 'Introdução aos conceitos fundamentais de Física', TRUE);

-- Disciplina 2: Cálculo I (Prof. Ana Oliveira)
INSERT INTO disciplinas (nome, codigo, professor_responsavel_id, descricao, ativa) 
VALUES ('Cálculo I', 'MAT001', 2, 'Fundamentos de Cálculo Diferencial', TRUE);

-- Disciplina 3: Química Orgânica (Prof. Carlos Souza)
INSERT INTO disciplinas (nome, codigo, professor_responsavel_id, descricao, ativa) 
VALUES ('Química Orgânica', 'QUI001', 3, 'Princípios de Química Orgânica', TRUE);

-- INSERIR TURMAS
-- Turma 1: Física Geral - Turma A - 2025/1
INSERT INTO turmas (id_disciplina, numero_turma, semestre, ano, capacidade_maxima, ativa) 
VALUES (1, 'A', 20251, 2025, 40, TRUE);

-- Turma 2: Cálculo I - Turma B - 2025/1
INSERT INTO turmas (id_disciplina, numero_turma, semestre, ano, capacidade_maxima, ativa) 
VALUES (2, 'B', 20251, 2025, 35, TRUE);

-- Turma 3: Química Orgânica - Turma A - 2025/1
INSERT INTO turmas (id_disciplina, numero_turma, semestre, ano, capacidade_maxima, ativa) 
VALUES (3, 'A', 20251, 2025, 30, TRUE);

-- INSERIR MATRÍCULAS
-- Maria Santos em Física Geral (Turma 1)
INSERT INTO matriculas (id_aluno, id_turma, situacao) 
VALUES (1, 1, 'ativa');

-- Maria Santos em Cálculo I (Turma 2)
INSERT INTO matriculas (id_aluno, id_turma, situacao) 
VALUES (1, 2, 'ativa');

-- Pedro Silva em Física Geral (Turma 1)
INSERT INTO matriculas (id_aluno, id_turma, situacao) 
VALUES (2, 1, 'ativa');

-- Pedro Silva em Química Orgânica (Turma 3)
INSERT INTO matriculas (id_aluno, id_turma, situacao) 
VALUES (2, 3, 'ativa');

-- Juliana Costa em Cálculo I (Turma 2)
INSERT INTO matriculas (id_aluno, id_turma, situacao) 
VALUES (3, 2, 'ativa');

-- Ricardo Oliveira em Física Geral (Turma 1)
INSERT INTO matriculas (id_aluno, id_turma, situacao) 
VALUES (4, 1, 'ativa');

-- Ricardo Oliveira em Química Orgânica (Turma 3)
INSERT INTO matriculas (id_aluno, id_turma, situacao) 
VALUES (4, 3, 'ativa');

-- INSERIR AVALIAÇÕES
-- Avaliação de Maria Santos em Física Geral - Prova 1
INSERT INTO avaliacoes (id_matricula, id_professor, id_turma, valor, tipo_avaliacao, peso, observacoes) 
VALUES (1, 1, 1, 8.5, 'Prova', 2.0, 'Bom desempenho');

-- Avaliação de Maria Santos em Física Geral - Trabalho
INSERT INTO avaliacoes (id_matricula, id_professor, id_turma, valor, tipo_avaliacao, peso, observacoes) 
VALUES (1, 1, 1, 9.0, 'Trabalho', 1.0, 'Excelente trabalho');

-- Avaliação de Maria Santos em Cálculo I - Prova 1
INSERT INTO avaliacoes (id_matricula, id_professor, id_turma, valor, tipo_avaliacao, peso, observacoes) 
VALUES (2, 2, 2, 7.5, 'Prova', 2.0, 'Satisfatório');

-- Avaliação de Pedro Silva em Física Geral - Prova 1
INSERT INTO avaliacoes (id_matricula, id_professor, id_turma, valor, tipo_avaliacao, peso, observacoes) 
VALUES (3, 1, 1, 6.0, 'Prova', 2.0, 'Necessita melhorar');

-- Avaliação de Pedro Silva em Física Geral - Participação
INSERT INTO avaliacoes (id_matricula, id_professor, id_turma, valor, tipo_avaliacao, peso, observacoes) 
VALUES (3, 1, 1, 7.0, 'Participação', 0.5, 'Participação regular');

-- Avaliação de Juliana Costa em Cálculo I - Prova 1
INSERT INTO avaliacoes (id_matricula, id_professor, id_turma, valor, tipo_avaliacao, peso, observacoes) 
VALUES (5, 2, 2, 9.0, 'Prova', 2.0, 'Excelente desempenho');

-- Avaliação de Ricardo Oliveira em Física Geral - Prova 1
INSERT INTO avaliacoes (id_matricula, id_professor, id_turma, valor, tipo_avaliacao, peso, observacoes) 
VALUES (6, 1, 1, 5.5, 'Prova', 2.0, 'Precisa estudar mais');

-- CONSULTAS PARA VALIDAÇÃO
-- Listar todos os alunos
SELECT * FROM v_alunos;

-- Listar todos os professores
SELECT * FROM v_professores;

-- Listar médias dos alunos
SELECT * FROM v_media_alunos;

-- Listar disciplinas com professor responsável
SELECT * FROM v_disciplinas_professor;
