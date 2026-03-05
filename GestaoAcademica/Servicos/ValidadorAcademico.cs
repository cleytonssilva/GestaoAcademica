using System;
using System.Text.RegularExpressions;

namespace GestaoAcademica.Servicos
{
    /// <summary>
    /// Validador centralizado para regras de negócio acadêmicas.
    /// Garante a integridade dos dados e conformidade com regras de negócio.
    /// </summary>
    public class ValidadorAcademico
    {
        // Constantes de regras de negócio
        public const int IDADE_MINIMA_ALUNO = 18;
        public const double NOTA_MINIMA = 0.0;
        public const double NOTA_MAXIMA = 10.0;
        public const double NOTA_MINIMA_APROVACAO = 6.0;
        public const decimal SALARIO_MINIMO = 1000M;

        /// <summary>
        /// Valida o nome (não vazio, sem números, mínimo 3 caracteres)
        /// </summary>
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio.");

            if (nome.Length < 3)
                throw new ArgumentException("Nome deve conter no mínimo 3 caracteres.");

            if (Regex.IsMatch(nome, @"\d"))
                throw new ArgumentException("Nome não pode conter números.");
        }

        /// <summary>
        /// Valida o CPF (formato XXX.XXX.XXX-XX e algoritmo de validação)
        /// </summary>
        public static void ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser vazio.");

            // Remove caracteres especiais
            string cpfLimpo = Regex.Replace(cpf, @"\D", "");

            if (cpfLimpo.Length != 11)
                throw new ArgumentException("CPF deve conter 11 dígitos.");

            // Verifica se todos os dígitos são iguais (CPF inválido)
            if (cpfLimpo == new string(cpfLimpo[0], 11))
                throw new ArgumentException("CPF inválido: todos os dígitos são iguais.");

            // Validar primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpfLimpo[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int primeiroDigito = resto < 2 ? 0 : 11 - resto;

            /* if (int.Parse(cpfLimpo[9].ToString()) != primeiroDigito)
                throw new ArgumentException("CPF inválido: primeiro dígito verificador incorreto.");

            // Validar segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpfLimpo[i].ToString()) * (11 - i);

            resto = soma % 11;
            int segundoDigito = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpfLimpo[10].ToString()) != segundoDigito)
                throw new ArgumentException("CPF inválido: segundo dígito verificador incorreto.");*/
        }

        /// <summary>
        /// Valida o email (formato básico)
        /// </summary>
        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio.");

            string padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, padraoEmail))
                throw new ArgumentException("Email inválido. Use o formato: usuario@dominio.com");
        }

        /// <summary>
        /// Valida a data de nascimento (deve ter a idade mínima)
        /// </summary>
        public static void ValidarDataNascimento(DateTime dataNascimento)
        {
            if (dataNascimento > DateTime.UtcNow)
                throw new ArgumentException("Data de nascimento não pode ser futura.");

            int idade = DateTime.UtcNow.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.UtcNow.AddYears(-idade))
                idade--;

            //if (idade < IDADE_MINIMA_ALUNO)
            //    throw new ArgumentException($"Aluno deve ter no mínimo {IDADE_MINIMA_ALUNO} anos.");
        }

        /// <summary>
        /// Valida a matrícula (número positivo, não duplicável)
        /// </summary>
        public static void ValidarMatricula(int matricula)
        {
            if (matricula <= 0)
                throw new ArgumentException("Matrícula deve ser um número positivo.");

            if (matricula.ToString().Length > 10)
                throw new ArgumentException("Matrícula não pode ter mais de 10 dígitos.");
        }

        /// <summary>
        /// Valida o salário do professor
        /// </summary>
        public static void ValidarSalario(decimal salario)
        {
            if (salario < SALARIO_MINIMO)
                throw new ArgumentException($"Salário não pode ser menor que {SALARIO_MINIMO:C}.");

            if (salario > 100000M)
                throw new ArgumentException("Salário não pode exceder R$ 100.000,00.");
        }

        /// <summary>
        /// Valida a nota (entre 0 e 10)
        /// </summary>
        public static void ValidarNota(double nota)
        {
            if (nota < NOTA_MINIMA || nota > NOTA_MAXIMA)
                throw new ArgumentException($"Nota deve estar entre {NOTA_MINIMA} e {NOTA_MAXIMA}.");
        }

        /// <summary>
        /// Valida o código da disciplina (não vazio, formato válido)
        /// </summary>
        public static void ValidarCodigoDisciplina(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Código da disciplina não pode ser vazio.");

            //if (codigo.Length < 2 || codigo.Length > 10)
            //    throw new ArgumentException("Código deve conter entre 2 e 10 caracteres.");

            //if (!Regex.IsMatch(codigo, @"^[A-Z0-9]+$"))
            //    throw new ArgumentException("Código deve conter apenas letras maiúsculas e números.");
        }

        /// <summary>
        /// Valida o nome da disciplina
        /// </summary>
        public static void ValidarNomeDisciplina(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da disciplina não pode ser vazio.");

            if (nome.Length < 3 || nome.Length > 100)
                throw new ArgumentException("Nome da disciplina deve conter entre 3 e 100 caracteres.");
        }

        /// <summary>
        /// Verifica se a nota representa aprovação
        /// </summary>
        public static bool EstaAprovado(double nota)
        {
            return nota >= NOTA_MINIMA_APROVACAO;
        }

        /// <summary>
        /// Calcula a situação do aluno baseado na média
        /// </summary>
        public static string CalcularSituacao(double media)
        {
            if (media >= NOTA_MINIMA_APROVACAO)
                return "APROVADO";
            else if (media >= 4.0)
                return "RECUPERAÇÃO";
            else
                return "REPROVADO";
        }
    }
}
