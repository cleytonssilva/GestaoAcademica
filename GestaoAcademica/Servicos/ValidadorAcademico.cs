using System;
using System.Text.RegularExpressions;

namespace GestaoAcademica.Servicos
{
   
    public class ValidadorAcademico
    {
        // Constantes de regras de negócio
        public const int IDADE_MINIMA_ALUNO = 18;
        public const double NOTA_MINIMA = 0.0;
        public const double NOTA_MAXIMA = 10.0;
        public const double NOTA_MINIMA_APROVACAO = 7.0;
        public const decimal SALARIO_MINIMO = 1000M;

        
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio.");

            if (nome.Length < 3)
                throw new ArgumentException("Nome deve conter no mínimo 3 caracteres.");

            if (Regex.IsMatch(nome, @"\d"))
                throw new ArgumentException("Nome não pode conter números.");
        }

        
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

            
        }

        
        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio.");

            string padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, padraoEmail))
                throw new ArgumentException("Email inválido. Use o formato: usuario@dominio.com");
        }

        
        public static void ValidarDataNascimento(DateTime dataNascimento)
        {
            if (dataNascimento > DateTime.UtcNow)
                throw new ArgumentException("Data de nascimento não pode ser futura.");

            int idade = DateTime.UtcNow.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.UtcNow.AddYears(-idade))
                idade--;

            
        }

        
        public static void ValidarMatricula(int matricula)
        {
            if (matricula <= 0)
                throw new ArgumentException("Matrícula deve ser um número positivo.");

            if (matricula.ToString().Length > 10)
                throw new ArgumentException("Matrícula não pode ter mais de 10 dígitos.");
        }

       
        public static void ValidarSalario(decimal salario)
        {
            if (salario < SALARIO_MINIMO)
                throw new ArgumentException($"Salário não pode ser menor que {SALARIO_MINIMO:C}.");

            if (salario > 100000M)
                throw new ArgumentException("Salário não pode exceder R$ 100.000,00.");
        }

       
        public static void ValidarNota(double nota)
        {
            if (nota < NOTA_MINIMA || nota > NOTA_MAXIMA)
                throw new ArgumentException($"Nota deve estar entre {NOTA_MINIMA} e {NOTA_MAXIMA}.");
        }

       
        public static void ValidarCodigoDisciplina(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Código da disciplina não pode ser vazio.");

            
        }

       
        public static void ValidarNomeDisciplina(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da disciplina não pode ser vazio.");

            if (nome.Length < 3 || nome.Length > 100)
                throw new ArgumentException("Nome da disciplina deve conter entre 3 e 100 caracteres.");
        }

        
        public static bool EstaAprovado(double nota)
        {
            return nota >= NOTA_MINIMA_APROVACAO;
        }

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
